ALTER TABLE Personnages.Personnage ADD
Identifiant uniqueidentifier NOT NULL ROWGUIDCOL DEFAULT newid()
GO

ALTER TABLE Personnages.Personnage ADD CONSTRAINT UC_Personnage_Identifiant
UNIQUE(Identifiant);
GO




ALTER TABLE Personnages.Personnage ADD
Photo varbinary(max) FILESTREAM NULL;
GO
