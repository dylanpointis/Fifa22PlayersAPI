CREATE DATABASE FootballPlayersDB
GO
USE FootballPlayersDB
GO

CREATE TABLE Players
(
	PlayerID int PRIMARY KEY IDENTITY(1,1),
	ShortName nvarchar(25),
	LongName nvarchar(50),
	PlayerPositions nvarchar(25),
	Overall int,
	Age int,
	BirthDate nvarchar(11),
	HeightCm int,
	WeightKg int,
	ClubName nvarchar(50),
	LeagueName nvarchar(50),
	NationalityName nvarchar(50),
	PreferredFoot nvarchar(5),
	WeakFoot int,
	SkillMoves int,
	Pace int,
	Shooting int,
	Passing int,
	Dribbling int,
	Defending int,
	Physic int,
	PlayerFaceUrl nvarchar(50),
	ClubLogoUrl nvarchar(50),
	NationFlagUrl nvarchar(50)
)
GO
/*CI: Case Insensitive. AI: Accent Insensitive */
ALTER TABLE Players ALTER COLUMN LongName NVARCHAR(255) COLLATE SQL_Latin1_General_CP1_CI_AI;

GO
CREATE Procedure InsertPlayer
	@ShortName nvarchar(25),
	@LongName nvarchar(50),
	@PlayerPositions nvarchar(25),
	@Overall int,
	@Age int,
	@BirthDate nvarchar(11),
	@HeightCm int,
	@WeightKg int,
	@ClubName nvarchar(50),
	@LeagueName nvarchar(50),
	@NationalityName nvarchar(50),
	@PreferredFoot nvarchar(5),
	@WeakFoot int,
	@SkillMoves int,
	@Pace int,
	@Shooting int,
	@Passing int,
	@Dribbling int,
	@Defending int,
	@Physic int,
	@PlayerFaceUrl nvarchar(50),
	@ClubLogoUrl nvarchar(50),
	@NationFlagUrl nvarchar(50)
AS
BEGIN
	INSERT INTO Players VALUES (@ShortName, @LongName, @PlayerPositions, @Overall, @Age, @BirthDate, 
	@HeightCm, @WeightKg, @ClubName, @LeagueName, @NationalityName, @PreferredFoot, @WeakFoot, @SkillMoves, 
	@Pace, @Shooting, @Passing, @Dribbling, @Defending, @Physic, @PlayerFaceUrl, @ClubLogoUrl, @NationFlagUrl)
END
GO


CREATE PROCEDURE GetAllPlayers
AS
BEGIN
	SELECT * FROM Players ORDER BY Overall DESC
END
GO

CREATE PROCEDURE GetPlayersByPage
@PageNumber int
AS
BEGIN
	SELECT * FROM Players
	WHERE PlayerID >= (@PageNumber - 1) * 100 AND  PlayerID <= @PageNumber * 100
	ORDER BY Overall DESC
END
GO

CREATE PROCEDURE GetPlayer
@PlayerID int
AS
BEGIN
	SELECT * FROM Players WHERE PlayerID = @PlayerID
END
GO


CREATE PROCEDURE ResetIdentity
AS
BEGIN
	DBCC CHECKIDENT ('Players', RESEED, 0);
END
GO


CREATE PROCEDURE FilterByOverall
@Overall int
AS
BEGIN
	SELECT * FROM Players WHERE Overall = @Overall
END
GO

CREATE PROCEDURE FilterByName
	@Name nvarchar(50)
AS
BEGIN
	SELECT * FROM Players WHERE LongName LIKE '%' + (@Name) + '%'
END
GO

CREATE PROCEDURE FilterByNationality
	@Nationality nvarchar(50)
AS
BEGIN
	SELECT * FROM Players WHERE NationalityName LIKE (@Nationality) + '%'
END
GO

CREATE PROCEDURE FilterByClub
	@Club nvarchar(50)
AS
BEGIN
	SELECT * FROM Players WHERE ClubName LIKE '%' + (@Club) + '%'
END
GO