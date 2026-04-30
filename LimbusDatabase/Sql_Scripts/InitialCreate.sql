--Création de la bd--
---------------------
CREATE DATABASE LimbusDatabase
GO
USE LimbusDatabase
GO

--Filestream--

EXEC sp_configure filestream_access_level, 2 RECONFIGURE
ALTER DATABASE LimbusDatabase
ADD FILEGROUP FG_Images CONTAINS FILESTREAM;
GO
ALTER DATABASE LimbusDatabase
ADD FILE(
	NAME=GF_Images,
	FILENAME='C:\EspaceLabo\FG_Images_2368398'
)
TO FILEGROUP FG_Images
GO
