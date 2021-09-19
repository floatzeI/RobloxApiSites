/**
 * @param {import('pg').Client} conn
 */
exports.up = async (conn) => {
    await conn.query(`

CREATE TABLE user_asset (
    id bigserial NOT NULL,
    asset_id bigint NOT NULL,
    user_id bigint NOT NULL,
    price int,
    serial_number int DEFAULT(null),
    expires_at timestamp DEFAULT(null), -- For rental items - this is when the item can no longer be used by the user until they renew it
    created_at timestamp default(now()),
    updated_at timestamp default(now())
);

create index user_asset_user_id on user_asset(user_id);
create index user_asset_asset_id_id on user_asset(asset_id, id); -- required for limited unique purchasing and ownership endpoint

`);
}

/**
 * @param {import('pg').Client} conn
 */
exports.down = async (conn) => {
    await conn.query(`DROP TABLE user_asset`);
}
