# Election Data Project
This Election Data Project is designed to scrape election poll data from various websites, clean and store it in a database, and provide a RESTful API to retrieve the data. The project is composed of several components, including a data scraper, a data API, and (soon) a web interface to display the poll data.

View the live application at: https://election.jtismon.dev

## Components
1. **Data Scraper**: A console application that scrapes election poll data, processes it, and stores it in a PostgreSQL database.
2. **Data API**: An Azure Function App that provides RESTful endpoints to access the poll data stored in the database.
3. **Web Interface**: A front-end web application built using React to visualize the poll data.

## Technology Used
1. **C# / .NET Core**: For developing the data scraper and API
2. **Entity Framework Core**: For ORM and database migrations.
3. **PostgreSQL**: For storing poll data
4. **Azure Functions**: For hosting the API
5. **React**: For the front-end web application
6. **Tailwind CSS**: For styling the front-end web application

## Configuration

### Configuration Files
There are three example configuration files:
1. **ElectionData.API/local.settings.json.example**: Add your connection string
2. **ElectionData.Scraper/local.appSettings.json.example**: Add your connection string
3. **ElectionData.Web/.env.local.example**: Set your Azure functions testing URL string

After you have added your connection strings in the three files, remove the .example file extension. 

### Deployment
You will need to make sure the environment variables are set for the respective projects according to the configuration files above.

### Database Migrations
To create migrations run:

```SHELL
dotnet ef migrations add InitialMigration --project ElectionData.Data --startup-project ElectionData.Scraper
```

To update the DB with the migrations, run:
```SHELL
dotnet ef database update --project ElectionData.Data --startup-project ElectionData.Scraper
```