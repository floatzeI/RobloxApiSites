/**
 * @param {import('pg').Client} conn
 */
exports.up = async (conn) => {
    await conn.query(`

CREATE TABLE asset_version (
    id bigserial NOT NULL, -- asset version id
    file_id varchar(128) NOT NULL, -- ID in Roblox.Files.Service
    asset_id bigint NOT NULL,
    user_id bigint NOT NULL, -- The userId that uploaded this version. For instance, in a group team create session, this would be the user who clicked "Publish"
    version_number int NOT NULL, -- The version number of the item
    created_at timestamp DEFAULT(CURRENT_TIMESTAMP) NOT NULL
);

-- For queries that want the latest version for a specific assetId (which will be common)
create index asset_version_asset_id_id_desc on asset_version(asset_id, id DESC);

`);
}

/**
 * @param {import('pg').Client} conn
 */
exports.down = async (conn) => {
    await conn.query(`DROP TABLE asset_version`);
}
