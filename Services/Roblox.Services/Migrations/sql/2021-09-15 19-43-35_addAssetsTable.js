/**
 * @param {import('pg').Client} conn
 */
exports.up = async (conn) => {
    await conn.query(`

CREATE TABLE asset  (
    id bigserial NOT NULL, -- asset id
    creator_id bigint NOT NULL,
    creator_type int NOT NULL, -- 1 = user, 2 = group
    name varchar(255) NOT NULL,
    description varchar(4096) DEFAULT NULL,
    type_id int NOT NULL, -- asset type id
    created_at timestamp DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
    updated_at timestamp DEFAULT(CURRENT_TIMESTAMP) NOT NULL
);


`);
}

/**
 * @param {import('pg').Client} conn
 */
exports.down = async (conn) => {
    await conn.query(`DROP TABLE asset`);
}
