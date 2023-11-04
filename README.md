# LiftBuddy

## Trello

Here the link to the trello project [link](https://trello.com/invite/b/ZM5SPJhw/ATTIca4272877b978a4851d1e85ef47f0b8332746D9E/liftbuddy)

### Useful commands
#### Database migrations
- dotnet ef migrations add "migration-name" --project Lift.Buddy.Core --context LiftBuddyContext
- dotnet ef database update --project Lift.Buddy.Core --context LiftBuddyContext
#### Deploy UI
Inside clientapp folder
- docker compose up --build
#### Deploy API
- launch project with visual studio usind docker compose option