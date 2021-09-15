/**
 * @param {import('pg').Client} conn
 */
exports.up = async (conn) => {
    await conn.query(`

CREATE TABLE avatar  (
    -- fk
    user_id bigint NOT NULL,
    -- colors
    head_color_id int NOT NULL,
    torso_color_id int NOT NULL,
    left_arm_color_id int NOT NULL,
    right_arm_color_id int NOT NULL,
    left_leg_color_id int NOT NULL,
    right_leg_color_id int NOT NULL,
    -- scales
    height_scale decimal NOT NULL,
    width_scale decimal NOT NULL,
    head_scale decimal NOT NULL,
    depth_scale decimal NOT NULL,
    proportion_scale decimal NOT NULL,
    body_type_scale decimal NOT NULL,
    -- misc
    avatar_type int NOT NULL DEFAULT '1'::int,
    created_at timestamp DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
    updated_at timestamp DEFAULT(CURRENT_TIMESTAMP) NOT NULL
);

CREATE UNIQUE INDEX avatar_user_id ON avatar (user_id);

-- assets
CREATE TABLE avatar_asset (
    user_id bigint NOT NULL,
    asset_id bigint NOT NULL
);

CREATE INDEX avatar_asset_user_id ON avatar_asset (user_id);
`);
}

/**
 * @param {import('pg').Client} conn
 */
exports.down = async (conn) => {
    await conn.query(`DROP TABLE avatar`);
    await conn.query(`DROP TABLE avatar_asset`);
}
