/**
 * @param {import('pg').Client} conn
 */
exports.up = async (conn) => {
    await conn.query(`

CREATE TABLE marketplace (
    id bigserial NOT NULL,
    asset_id bigint NOT NULL,
    is_for_sale bool NOT NULL,
    price_robux int,
    price_tickets int,
    is_limited bool NOT NULL default(false),
    is_limited_unique bool NOT NULL default(false),
    stock_count int default(null),
    off_sale_deadline timestamp default(null),
    minimum_membership_level smallint default(null),
    content_rating_id smallint default(null)
);
-- note: there is no unique index on asset_id - roblox has some assets with multiple products
create index marketplace_asset_id on marketplace(asset_id);
`);
}

/**
 * @param {import('pg').Client} conn
 */
exports.down = async (conn) => {
    await conn.query(`DROP TABLE marketplace`);
}
