/**
 * @param {import('pg').Client} conn
 */
exports.up = async (conn) => {
    await conn.query(`

CREATE TABLE asset_genre (
    asset_id bigint NOT NULL, -- the id of the asset
    genre_id smallint NOT NULL -- the genre id
);

create index asset_genre_asset_id on asset_genre(asset_id);
`);
}

/**
 * @param {import('pg').Client} conn
 */
exports.down = async (conn) => {
    await conn.query(`DROP TABLE asset_genre`);
}
