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
	
END