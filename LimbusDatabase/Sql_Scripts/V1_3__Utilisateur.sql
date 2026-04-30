CREATE TABLE Utilisateurs.Utilisateur(
UtilisateurID int IDENTITY(1,1),
Pseudonyme nvarchar(50) NOT NULL,
MotDePasseHache varbinary(32) NOT NULL,
MdpSel varbinary(16) NOT NULL,
Email nvarchar(256) NOT NULL,
CONSTRAINT PK_Utilisateur_UtilisateurID PRIMARY KEY(UtilisateurID)
);
GO

CREATE MASTER KEY ENCRYPTION BY PASSWORD='kynmE5-catfyd-nohtam';
GO

CREATE CERTIFICATE MonCertificat WITH SUBJECT ='ChiffrementMDP'
GO

CREATE SYMMETRIC KEY MaSuperCle WITH ALGORITHM = AES_256 ENCRYPTION BY CERTIFICATE MonCertificat;
GO
CREATE PROCEDURE Utilisateurs.USP_CreerUtilisateur
	@Pseudonyme nvarchar(50),
	@MotDePasse nvarchar(100),
	@Email nvarchar(256)
AS
BEGIN
	DECLARE @MdpSel varbinary(16)= CRYPT_GEN_RANDOM(16);

	DECLARE @MdpEtSel nvarchar(116)=CONCAT(@MotDePasse,@MdpSel);

	DECLARE @MdpHachage varbinary(32)=HASHBYTES('SHA2_256',@MdpEtSel);

	INSERT INTO Utilisateurs.Utilisateur(Pseudonyme,MotDePasseHache,MdpSel,Email)
	VALUES(@Pseudonyme,@MdpHachage,@MdpSel,@Email)
END
GO

CREATE PROCEDURE Utilisateurs.AuthUtilisateur
	@Pseudo nvarchar(50),
	@MotDePasse nvarchar(50)
AS
BEGIN
		
		DECLARE @MdpSel varbinary(16)
		DECLARE @MdpHache varbinary(32);

		SELECT  @MdpSel=MdpSel,@MdpHache=MotDePasseHache
		FROM Utilisateurs.Utilisateur
		WHERE @Pseudo=Pseudonyme;

		IF HASHBYTES('SHA2_256',CONCAT(@MotDePasse,@MdpSel))=@MdpHache
		BEGIN
			SELECT * FROM Utilisateurs.Utilisateur WHERE Pseudonyme=@Pseudo
		END
		ELSE
		BEGIN
			SELECT TOP 0 * FROM Utilisateurs.Utilisateur
		END
END
GO


