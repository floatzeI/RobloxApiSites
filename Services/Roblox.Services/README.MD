# What is this?

This is a general re-creation of Roblox's micro services. Since so little is known about them (aside from the URLs and a handful of methods), we only have one "big service" which essentially acts as each small micro service shoved into a big one, so that in the event that more information is known about the actual services, it won't be too hard to split this off into a million tiny services.

## Design Notes

- Do not use tables not specific to the controller. For instance, you cannot inner join with the users table in the AccountInformation controller/service, since this would make de-coupling impossible in the future (and is not realistic).