/**
 * @param {import('pg').Client} conn
 */
exports.up = async (conn) => {
    await conn.query(`CREATE TABLE account_session  (
    id varchar(1024) NOT NULL constraint account_session_id_key unique, -- cookie/session id (no prefix)
	user_id bigint NOT NULL,
    created_at timestamp DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
    updated_at timestamp DEFAULT(CURRENT_TIMESTAMP) NOT NULL -- When the session was last used
);

create index account_session_user_id on account_information (user_id); -- required for pages where we need to get all sessions for the userId (e.g. to log out of all)


    `);
}

/**
 * @param {import('pg').Client} conn
 */
exports.down = async (conn) => {
    await conn.query(`DROP TABLE account_session`);
}
