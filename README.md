# Awsome Library Adventure
is designed with n-tier application working with raw SQL and Stored Procudure at the data layer. WebAPI designed as service layer. If you like to run this project you first need to configure it properly in steps below:

## Running WebAPI

 1. Restore the database backup file [AwsomeLibraryDb](https://github.com/Ahmetcanb/AwsomeLibraryAdvanture/blob/master/AwsomeLibraryDb.bak)
 2. Configure your database server link at [WebAPI > appsettings.json](https://github.com/Ahmetcanb/AwsomeLibraryAdvanture/blob/master/AwsomeLibraryAdvanture.WebAPI/appsettings.json)
 3. Go to the WebAPI folder and run `dotnet run` at the terminal.
 4. `https://localhost:5001` go to the URL in browser, it will redirect to you swagger page. You can see what you can do with API

## Running AwsomeUI

 1. Go to the AwsomeUI folder and run `dotnet run` at the terminal
 2. If you did not change any configuration, you will able to see web site at the URL `https://localhost:3001`
 3. If your AwsomeWebAPI not running on `https://localhost:5001`, you need to configure the URLs in file [site.js](https://github.com/Ahmetcanb/AwsomeLibraryAdvanture/tree/master/AwsomeLibraryAdvanture.AwsomeUI/wwwroot/js/site.js)

> Hooray!
