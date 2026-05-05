CREATE PROCEDURE Personnages.usp_MortDeFactionDistrict
(
@FactionId int,
@District char
)
AS
BEGIN
	
	UPDATE Personnages.Personnage
	SET EnVie=0
	WHERE FactionId=@FactionId AND District=@District

	UPDATE Anormalites.Equipement
	SET PersonnageId=null
	WHERE PersonnageId IN(SELECT PersonnageId FROM Personnages.Personnage WHERE FactionId=@FactionId AND District=@District)
END
--Puisque cette procédure ne fait pas usage de IQueryable puisque sans retour, je vais à la place ajouter un nonclustered index aux factions afin
--d'augmenter la vitesse d'exécution
CREATE NONCLUSTERED INDEX IX_Bataille_BatailleId ON Anormalites.Bataille(BatailleId)
