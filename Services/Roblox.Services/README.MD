# What is this?

This is a general re-creation of Roblox's micro services. Since so little is known about them (aside from the URLs and a handful of methods), we only have one "big service" which essentially acts as each small micro service shoved into a big one, so that in the event that more information is known about the actual services, it won't be too hard to split this off into a million tiny services.

## Design Notes

- Controllers directly access the database for ease of use. This would be horrible in a real world scenario (since testing would be difficult or impossible), however, this is just a fun little project that will never see real use, so I don't care enough to split up everything.

- Do not use tables not specific to the controller. For instance, you cannot inner join with the users table in the AccountInformation controller.