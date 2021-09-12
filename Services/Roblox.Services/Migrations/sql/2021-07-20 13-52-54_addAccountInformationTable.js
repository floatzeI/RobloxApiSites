/**
 * @param {import('pg').Client} conn
 */
exports.up = async (conn) => {
    await conn.query(`CREATE TABLE account_information  (
	user_id bigint PRIMARY KEY,
    birthdate date,
    gender smallint,
    description VARCHAR(4096),
    created_at timestamp DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
    updated_at timestamp DEFAULT(CURRENT_TIMESTAMP) NOT NULL
);

    `);
}

/**
 * @param {import('pg').Client} conn
 */
exports.down = async (conn) => {
    await conn.query(`DROP TABLE account_information`);
}
