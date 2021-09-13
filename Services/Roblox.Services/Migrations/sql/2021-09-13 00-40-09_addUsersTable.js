/**
 * @param {import('pg').Client} conn
 */
exports.up = async (conn) => {
    await conn.query(`

CREATE TABLE account  (
    id bigserial PRIMARY KEY NOT NULL,
    username varchar(50) NOT NULL,
    display_name varchar(50) DEFAULT NULL,
    account_status int NOT NULL DEFAULT 1,
    created_at timestamp DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
    updated_at timestamp DEFAULT(CURRENT_TIMESTAMP) NOT NULL
);

CREATE INDEX username_lower_idx ON account (lower(username)); -- don't bother with unique - we'd need to use an extension for case insensitive unique, so just handle it in the db or service layer...

-- passwords

    CREATE TABLE account_password
    (
        user_id         bigint                                NOT NULL,
        password        varchar(255)                          NOT NULL,
        password_status int                                   NOT NULL DEFAULT 1,
        created_at      timestamp DEFAULT (CURRENT_TIMESTAMP) NOT NULL,
        updated_at      timestamp DEFAULT (CURRENT_TIMESTAMP) NOT NULL
    );

CREATE INDEX account_password_userid_idx ON account_password (user_id);
`);
}

/**
 * @param {import('pg').Client} conn
 */
exports.down = async (conn) => {
    await conn.query(`DROP TABLE account`);
    await conn.query(`DROP TABLE account_password`);
}
