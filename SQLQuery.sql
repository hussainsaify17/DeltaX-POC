CREATE TABLE Persons (
    PersonsID int IDENTITY(1,1) PRIMARY KEY,
    Name varchar(50) NOT NULL,
    Sex varchar(6) NOT NULL CHECK (Sex IN ('MALE', 'FEMALE')),
    DOB date NOT NULL,
    Bio varchar(255),
	Designation varchar(10) NOT NULL CHECK (Designation IN ('ACTOR', 'PRODUCER'))
);

Create TABLE Movies(
	MoviesID int IDENTITY(1,1) PRIMARY KEY,
	Name varchar(100) NOT NULL,
	YEAROFRELEASE varchar(4) NOT NULL,
	PLOT varchar(255),
	PosterImage Varchar(255),	
	Producer int NOT NULL FOREIGN KEY REFERENCES Persons(PersonsID)	
);

Create TABLE MoviesActors(
	
	MoviesActorsID int PRIMARY KEY IDENTITY(1,1) ,
	MovieID int NOT NULL,
	PersonID int NOT NULL
);