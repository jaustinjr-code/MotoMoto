PROJECT DESCRIPTION:

Our programming files contain the implementation of our Service Layer, DataAccess Layer, Business Layer, and Application Layer for our Program.cs file to work properly. Within the Program.cs file you are able to create a user (meaning adding a username, password, and email), check if the user status is Enabled or Disabled, and Delete or Update the information of a user.
Our project goal was to test the program using XUnit, however we were not able to get this up and running in time for each of our members. So to test our program we created individual unit tests that are stored in the Test.cs file.
As we further the project we have a Bulk Method and Authourization/Authentication programs that we will continue to implement and unit test into our program as well as other items as the model we currently have is highly scalable.

HOW TO RUN:

1. Make sure you have MariaDB 10.6 installed
   - Mac users will install using homebrew `brew install mariadb`
   - Windows users will install using the online https://mariadb.com/downloads/
1. Make sure you have MySqlConnector installed using `dotnet add package mysqlconnector`
1. Locate the `.sql` files in the folder `./MotoMoto/SetUpEnvironment/MariaDB`
   - Connect to your MariaDB user and create the database for user accounts and logs using `create database <db_name>;` for each
   - Enter into each database and run their respective .sql files `MotoMotoLog.sql` for logs and `MotoMotoUserManagement.sql` for user accounts
   - Or run the dump files using `mariadb -u <user_name> -p <db_name> < <dump_file>`
1. Run the Program.cs file in ./MotoMoto/app and the project runs

ERRORS:
-BuildTempUser() only is working with hard coded values of username and password rather than user input. File: app/TheNewPanalists.DataAccessLayer/Implementations/ArchivingDataAccess.cs
-In the LoggingDataAccess layer the correct value of user id is not being retrieved within the EstablishMariaDb() function. File: app/TheNewPanalists.DataAccessLayer/Implementations/LoggingDataAccess.cs
