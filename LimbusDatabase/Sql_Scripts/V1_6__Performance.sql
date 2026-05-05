--Il est pertinent de rajouter un Non-Clustered Index sur la colonne PersonnageId de Personnage, car il y à de nombreux personnages dans cette table, et la page Index utilise le id d'un
--Personnage pour les charger.

CREATE NONCLUSTERED INDEX IX_Personnage_PersonnageID ON Personnages.Personnage(PersonnageID)

--la table Bataille est extensive et tout comme Personnage, est chargé en utilisant les Id des batailles

CREATE NONCLUSTERED INDEX IX_Bataille_BatailleId ON Anormalites.Bataille(BatailleId)

