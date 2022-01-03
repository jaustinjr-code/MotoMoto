PROJECT DESCRIPTION:

  Our programming files contain the implementation of our Service Layer, DataAccess Layer, Business Layer, and Application Layer for our Program.cs file to work properly. Within the Program.cs file you are able to create a user (meaning adding a username, password, and email), check if the user status is Enabled or Disabled, and Delete or Update the information of a user. 
   Our project goal was to test the program using XUnit, however we were not able to get this up and running in time for each of our members. So to test our program we created individual unit tests that are stored in the Test.cs file. 
   As we further the project we have a Bulk Method and Authourization/Authentication programs that we will continue to implement and unit test into our program as well as other items as the model we currently have is highly scalable.

HOW TO RUN:



ERRORS:
-BuildTempUser() only is working with hard coded values of username and password rather than user input. File: app/TheNewPanalists.DataAccessLayer/Implementations/ArchivingDataAccess.cs
-In the LoggingDataAccess layer the correct value of user id is not being retrieved within the EstablishMariaDb() function. File: app/TheNewPanalists.DataAccessLayer/Implementations/LoggingDataAccess.cs 