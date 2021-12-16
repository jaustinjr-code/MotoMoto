## MotoMoto

CECS 491A Semester Project

Milestone 3

Professor Vatanak Vong

Blake Del Rey\
Isabel Guzman\
Jacob Sunia\
James Austin Jr\
Naeun Yu

Team Leader: James Austin Jr

How To Run:

1. Make sure you have MariaDB 10.6 installed
   - Mac users will install using homebrew `brew install mariadb`
   - Windows users will install using the online https://mariadb.com/downloads/
1. Make sure you have MySqlConnector installed using `dotnet add package mysqlconnector`
1. Locate the `.sql` files in the folder `./MotoMoto/SetUpEnvironment/MariaDB`
   - Connect to your MariaDB user and create the database for user accounts and logs using `create database <db_name>;` for each
   - Enter into each database and run their respective .sql files `MotoMotoLog.sql` for logs and `MotoMotoUserManagement.sql` for user accounts
   - Or run the dump files using `mariadb -u <user_name> -p <db_name> < <dump_file>`
1. Run the Program.cs file in ./MotoMoto/app and the project runs
