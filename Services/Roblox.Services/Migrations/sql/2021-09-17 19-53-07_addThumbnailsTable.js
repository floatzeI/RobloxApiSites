/**
 * @param {import('pg').Client} conn
 */
exports.up = async (conn) => {
    await conn.query(`

CREATE TABLE thumbnail (
    id varchar(64) NOT NULL, -- thumbnail ID
    file_id varchar(128) NOT NULL, -- ID in Roblox.Files.Service
    reference_id bigint NOT NULL, -- ID depending on the type
    thumbnail_type int NOT NULL, -- Thumbnail type enum
    resolution_x smallint NOT NULL,
    resolution_y smallint NOT NULL,
    created_at timestamp DEFAULT(CURRENT_TIMESTAMP) NOT NULL
);

create index thumbnail_id on thumbnail(id);
create index thumbnail_reference_id_type on thumbnail(reference_id, thumbnail_type);
`);
}

/**
 * @param {import('pg').Client} conn
 */
exports.down = async (conn) => {
    await conn.query(`DROP TABLE thumbnail`);
}
