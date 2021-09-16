// Simple migration system inspired by knexjs, but more forgiving, and no weird abstractions on types
const help = `
Usage:
node Migrations/index.js latest                - Run up to the latest migration
node Migrations/index.js up <migration name>   - Go up a migration
node Migrations/index.js down <migration name> - Go down a migration
node Migrations/index.js make <migration name> - Create a migration with name
`;
const migrationTableName = 'rbx_migrations'; // Don't use special characters in this, it's not escaped when querying
const fs = require('fs');
const path = require('path');
const config = JSON.parse(fs.readFileSync(path.join(__dirname, '../appsettings.json')).toString());
const migrationFilesPath = path.join(__dirname, './sql/');

const _postgresSplit = config.Postgres.split('; ');
const readPostgresVal = (keyToRead) => {
    for (const item of _postgresSplit) {
        const [key, value] = item.split('=');
        if (key.toLowerCase() == keyToRead) {
            return value;
        }
    }
}

const { Client } = require('pg')
const client = new Client({
    host: readPostgresVal('host'),
    database: readPostgresVal('database'),
    user: readPostgresVal('username'),
    password: readPostgresVal('password'),
});
/**
 * @type {import('pg').Client}
 */
let mode = process.argv[2];
if (typeof mode !== 'string') {
    console.error('No arguments supplied. Displaying Help:\n' + help);
    process.exit(1);
}
mode = mode.toLowerCase();

const runMigration = async (item, mode) => {
    console.log('[info] Running Migration', item);
    await client.query('BEGIN');
    try {
        const module = require(migrationFilesPath + '/' + item.slice(0, -3));
        if (mode === 'up') {
            await module.up(client);
            await client.query('INSERT INTO ' + migrationTableName + ' (migration_name, created_at, is_db) VALUES ($1, $2, $3)', [
                item,
                new Date(),
                true,
            ]);
        } else {
            await module.down(client);
            await client.query('DELETE FROM ' + migrationTableName + ' WHERE migration_name = $1', [
                item,
            ]);
        }
        await client.query('COMMIT');
        console.log('[info] Migration', item, 'Completed');
    } catch (e) {
        console.log('[info] Migration', item, 'Failed');
        await client.query('ROLLBACK');
        throw e;
    }
}

const main = async () => {
    await client.connect();
    // first, check if we need to make the migration table
    const exists = await client.query(`SELECT * FROM information_schema.tables WHERE table_name = $1`, [migrationTableName]);
    if (exists.rowCount === 0) {
        await client.query(`CREATE TABLE ${migrationTableName} (
    id serial PRIMARY KEY,
    migration_name VARCHAR (255) NOT NULL,
    created_at TIMESTAMP NOT NULL,
    is_db BOOLEAN NOT NULL DEFAULT(FALSE)
)`);
        console.log('[info] created', migrationTableName);
    }
    if (mode === 'latest') {
        // now get all our migrations and see if anything has changed
        const ranMigrations = await client.query('SELECT * FROM ' + migrationTableName);
        let hm = {};
        for (const item of ranMigrations.rows) {
            hm[item.migration_name] = true;
        }
        let didRun = false;
        for (const item of fs.readdirSync(migrationFilesPath)) {
            if (hm[item] !== true) {
                didRun = true;
                await runMigration(item, 'up');
            }
        }
        if (!didRun) {
            console.log('[info] No Pending Migrations')
        }

    } else if (mode === 'up') {
        const name = process.argv[3];
        const ranMigrations = await client.query('SELECT * FROM ' + migrationTableName);
        for (const item of ranMigrations.rows) {
            if (item.migration_name === name) {
                console.error('[error] Migration', item.migration_name, 'Already ran');
                process.exit(1);
            }
        }
        await runMigration(name, 'up');
    } else if (mode === 'down') {
        // TODO
        const name = process.argv[3];
        const ranMigrations = await client.query('SELECT * FROM ' + migrationTableName);
        for (const item of ranMigrations.rows) {
            if (item.migration_name === name) {
                await runMigration(name, 'down');
                process.exit(0);
            }
        }
        console.error('[error] Migration', name, 'Could not be found');
        process.exit(1);
    } else if (mode === 'make') {
        const name = process.argv[3];
        const fullFileName = new Date().toISOString().replace(/T/, ' ').replace(/\..+/, '').replace(/:/g, '-') + '_' + name + '.js';
        const templateFile = `/**
 * @param {import('pg').Client} conn
 */
exports.up = async (conn) => {

}

/**
 * @param {import('pg').Client} conn
 */
exports.down = async (conn) => {

}
`;
        fs.writeFileSync(migrationFilesPath + '/' + fullFileName, templateFile);
    }

    await client.end()

}
main();
process.on('unhandledRejection', e => { console.error(e); process.exit(1) })