--Cette vue permet de voir le nombre d'identités pour les 12 personnages de base du jeu
CREATE VIEW Personnages.Vw_IdentitesPersonnages
AS
SELECT CONCAT(Prenom,' ',Nom) AS [Nom],COUNT(Prenom) AS[Nombre d'identite] FROM Personnages.Personnage
WHERE Prenom IN('Gregor','Heathcliff','Rodion','Sinclair','Hong','Outis','Faust','Meursault','Ishmael','Yi','Ryoshu','Don')
GROUP BY Prenom, Nom
GO

