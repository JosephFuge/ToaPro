# ToaPro

Note that many links are not updated yet.

## Project setup
1. Ensure Postgres is installed on your machine
2. Ensure you have a Postgres server with port 5432 running
3. Ensure that server has a user with username "postgres" and password "postgres"
4. Run the following two commands to setup the database:
	a. dotnet ef migrations add Initial
	b. dotnet ef database update


## User login
How to create a user of a certain type (bandaid fix until a view is created to assign roles) - start the email with the right word to get its corresponding role:
Coord - Coordinator
Prof - Professor
Stud - Student
TA - TA
Judge - Judge

i.e. studhaydencowart@gmail.com
^ creates a student user

Note: Unfortunately, creating student and judge users this way does not add a record to the Student or Judge tables yet