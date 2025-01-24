.NET Web API, Angular 19, ETL (Extract, Transform, Load) from csv file, SQL Server

ETL process of 17000 players from 'players_22.csv'. Source: https://www.kaggle.com/datasets/bedooralmareni/players-22csv
Selected columns:

-PlayerID
-ShortName
-LongName
-PlayerPositions
-Overall
-Age
-BirthDate
-HeightCm
-WeightKg
-ClubName
-LeagueName
-NationalityName
-PreferredFoot
-WeakFoot
-SkillMoves
-Pace
-Shooting
-Passing
-Dribbling
-Defending
-Physic
-PlayerFaceUrl
-ClubLogoUrl
-NationFlagUrl

First execute 'DBQuery.sql' file to set up the database in SQL Server.
Then open the 'FootballPlayersAPI.sln' solution with Visual Studio and run it. 
Finally, execute the angular App with Visual Studio Code with the 'npm start' command.
The first time you run the app, the database will be empty. The ETL process will automatially start and you can refresh or run it again.
