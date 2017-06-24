. Installation steps:
	. Attach the datase mdf file (UI/App_Data) to an existing sql server
	. Create a new user or use an existing sql user with enough privilage to read, insert and execute procedures
	. Update the connection string in web.config (UI project) and app.config (Tests project) with the new credentials

. Usage steps
	. Run the UI project
	. Click on (Register) menu item and create a new user
	. Login with the new user email

. Application features
	. Register new users with username and email
	. Login using email
	. Browse books and their auther
	. Sort by book title or auther name
	. Filter with (all books, available books, taken by current user books)
	. Ability to borrow available books
	. Ability to return books taken by current user
	. View book history (who took it, returned it and when)
	. Logout to login with different user

. Shortcomings due to time limitation
	. Paging algorithm is not the best
	. I made sample unit tests, couldn't test all classes
	. I made simple exception handler that logs to a text file
	. Email feature doesn't work
	. There are a lot of optimization and better exception handling needed
	. Didn't check for email format

. Tehnical info
	. I used MVC for Client
	. Used Autofac for dependency injection
	. Used MStest for unit tests
	. Used Moq framework for mocking