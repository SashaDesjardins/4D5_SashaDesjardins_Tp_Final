CREATE VIEW Anormalites.Vw_PersonnagesBatailles
AS
SELECT CONCAT(P.Prenom,' ',P.Nom) AS[Nom],COUNT(BatailleID) AS [Nombre de participations] FROM Personnages.Personnage P  INNER JOIN Anormalites.PersonnageBataille PB ON P.PersonnageID=PB.PersonnageId
WHERE P.PersonnageId=PB.PersonnageId
GROUP BY P.Prenom,P.Nom
GO

CREATE VIEW Anormalites.Vw_AnormaliteBatailles
AS
SELECT A.Nom,COUNT(BatailleID) as [Nombre de participations] FROM Anormalites.Anormalite A INNER JOIN Anormalites.AnormaliteBataille AB ON A.AnormaliteID=AB.AnormaliteID
WHERE A.AnormaliteID=AB.AnormaliteID
GROUP BY A.Nom
GO

