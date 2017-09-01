So kinda cheated because I didnt start working on the project till today. I've been super busy over the last few days so bear with me.
1. Didnt implement a SQL database. Wanted you guys to be able to run the code without installing SQL server so went for 
   SQLite as my single user database. The syntax is slightly different from T-SQL but that wasnt the problem, the problem was I tried to use EF6 to link with the database 
   but it was taking too long to set everything up.
2. Instead of using a database, I cheated and stored the users in a session variable which is totally against everything RESTful 
   but I was just trying to get something out quickly, Look at Stash.DataSource.UserSessionDataSourceLogic for the Session data source logic.
3. I just now realized I forgot to hash the password but I'm about to go to sleep right now and I wanna submit this right now so the password isnt hashed before being saved.
4. The logic to get the account key works by making a request to the api and if it fails, it waits for like 10 seconds and tries again, it tries 10 times before giving up and throwing an exception,
   right now that exception isnt being handled in any way.
5. The account key service logic is handled with a background thread so the user isnt locked out if the external api takes too long or doesnt respond.
6. I created an action to help you load a bunch of random users into the system. 
   Alls you'd need to do is make a POST request to v1/users/CreateRandomUsers?numUsers=100 and that creates 100 random users, you can leave the "numUsers" part blank and it creates 1000 records and returns them.
7. To conclude, every feature is implemented bar:  SQL database and hashed password and that's just due to me trying to get this to you guys right now.
8. I used POSTMAN to test the web api. 

