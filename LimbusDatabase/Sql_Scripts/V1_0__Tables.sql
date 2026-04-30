--Création des tables--



CREATE TABLE Personnages.Faction(
		FactionID int IDENTITY (1,1) NOT NULL,
		Nom nvarchar(50) NOT NULL,
		Type nvarchar(15) NOT NULL,
		Description nvarchar(250),
		CONSTRAINT PK_Faction_FactionID PRIMARY KEY (FactionID)
);
GO

CREATE TABLE Personnages.Personnage(
		PersonnageID int IDENTITY (1,1) NOT NULL,
		Nom nvarchar(50),
		Prenom nvarchar(50) NOT NULL,
		FactionID int NOT NULL,
		District char(1) NOT NULL,
		EnVie bit NOT NULL,
		CONSTRAINT PK_Personnage_PersonnageID PRIMARY KEY (PersonnageID)
);
GO

CREATE TABLE Personnages.Identite(
		IdentiteID int IDENTITY (1,1) NOT NULL,
		Titre nvarchar(50) NOT NULL,
		DateTrouve DATE NOT NULL,
		Rarete int NOT NULL,
		PersonnageID int NOT NULL
);
GO



CREATE TABLE Anormalites.Anormalite(
		AnormaliteID int IDENTITY (1,1) NOT NULL,
		Nom nvarchar(100) NOT NULL,
		Classification nvarchar(5) NOT NULL,
		Origine nvarchar(20) NOT NULL,
		Apparence nvarchar(500) NOT NULL,
		CONSTRAINT PK_Anormalite_AnormaliteID PRIMARY KEY (AnormaliteID)
);
GO

CREATE TABLE Anormalites.Equipement(
		EquipementID int IDENTITY (1,1) NOT NULL,
		Nom nvarchar(25) NOT NULL,
		Type nvarchar(10) NOT NULL,
		Effets nvarchar(250) NOT NULL,
		Classification nvarchar(5) NOT NULL,
		AnormaliteID int NOT NULL,
		PersonnageID int,
		CONSTRAINT PK_Equipement_EquipementID PRIMARY KEY (EquipementID)
);
GO

CREATE TABLE Anormalites.Bataille(
		BatailleID int IDENTITY (1,1) NOT NULL,
		Date datetime NOT NULL,
		But nvarchar(500) NOT NULL,
		CONSTRAINT PK_Bataille_BatailleID PRIMARY KEY (BatailleID)

);
GO

CREATE TABLE Anormalites.PersonnageBataille(
		BatailleID int NOT NULL,
		PersonnageID int NOT NULL,
		CONSTRAINT PK_PersonnageBataille_BatailleID_PersonnageID PRIMARY KEY (BatailleID,PersonnageID)	
);
GO

CREATE TABLE Anormalites.AnormaliteBataille(
		BatailleID int NOT NULL,
		AnormaliteID int NOT NULL,
		CONSTRAINT PK_AnormaliteBataille_BatailleID_AnormaliteID PRIMARY KEY(BatailleID,AnormaliteID)
);
GO

--Ajout des restrictions--
ALTER TABLE Personnages.Faction 
ADD CONSTRAINT CK_Faction_Type CHECK (Type in ('Bureau','Association','Aile','Syndicat','Autre'))
GO

ALTER TABLE Personnages.Personnage
ADD CONSTRAINT FK_Personnage_FactionID FOREIGN KEY (FactionID) REFERENCES Personnages.Faction(FactionID)
GO

ALTER TABLE Personnages.Personnage
ADD CONSTRAINT DF_Personnage_Nom DEFAULT '' FOR Nom
GO

ALTER TABLE Personnages.Identite 
ADD CONSTRAINT FK_Identite_PersonnageID FOREIGN KEY (PersonnageID) REFERENCES Personnages.Personnage(PersonnageID)
GO

ALTER TABLE Personnages.Identite
ADD CONSTRAINT CK_Identite_Rarete CHECK (Rarete BETWEEN 1 AND 3)
GO

ALTER TABLE Anormalites.Anormalite
ADD CONSTRAINT CK_Anormalite_Classification CHECK (Classification in ('ZAYIN','TETH','HE','WAW','ALEPH'))
GO

ALTER TABLE Anormalites.Anormalite
ADD CONSTRAINT UC_Anormalite_Nom UNIQUE (Nom)
GO

ALTER TABLE Anormalites.Equipement
ADD CONSTRAINT FK_Equipement_AnormaliteID FOREIGN KEY (AnormaliteID) REFERENCES Anormalites.Anormalite(AnormaliteID)
GO

ALTER TABLE Anormalites.Equipement
ADD CONSTRAINT FK_Equipement_PersonnageID FOREIGN KEY (PersonnageID) REFERENCES Personnages.Personnage(PersonnageID)
GO

ALTER TABLE Anormalites.Equipement
ADD CONSTRAINT CK_Equipement_Type CHECK (Type in ('Arme','Armure'))
GO

ALTER TABLE Anormalites.PersonnageBataille
ADD CONSTRAINT FK_PersonnageBataille_BatailleID FOREIGN KEY (BatailleID) REFERENCES Anormalites.Bataille(BatailleID)
GO

ALTER TABLE Anormalites.PersonnageBataille
ADD CONSTRAINT FK_PersonnageBataille_PersonnageID FOREIGN KEY (PersonnageID) REFERENCES Personnages.Personnage(PersonnageID)
GO

ALTER TABLE Anormalites.AnormaliteBataille
ADD CONSTRAINT FK_AnormaliteBataille_BatailleID FOREIGN KEY (BatailleID) REFERENCES Anormalites.Bataille(BatailleID)
GO

ALTER TABLE Anormalites.AnormaliteBataille
ADD CONSTRAINT FK_AnormaliteBataille_AnormaliteID FOREIGN KEY(AnormaliteID) REFERENCES Anormalites.Anormalite(AnormaliteID)
GO