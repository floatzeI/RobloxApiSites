/**
 * @param {import('pg').Client} conn
 */
exports.up = async (conn) => {
    await conn.query(`

CREATE TABLE file (
    id varchar(128) NOT NULL, -- ID, aka file hash
    mime varchar(64) DEFAULT NULL, -- File mime type (e.g. "Image/PNG"), if known
    size_bytes bigint NOT NULL, -- File size in bytes
    created_at timestamp DEFAULT(CURRENT_TIMESTAMP) NOT NULL
);

create unique index file_id on file(id);


`);
}

/**
 * @param {import('pg').Client} conn
 */
exports.down = async (conn) => {
    await conn.query(`DROP TABLE file`);
}
