CREATE DATABASE Zakazivanje
GO
USE Zakazivanje
GO
CREATE SCHEMA Zakazivanje
GO
CREATE SCHEMA Prodaja
GO

CREATE TABLE Zakazivanje.Korisnici
(
	KorisnikID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Ime nvarchar(30) NOT NULL,
	Prezime nvarchar(30) NULL,
	Pol int NOT NULL,
	DatumRodjenja date NULL,
	Telefon nvarchar(30) NULL,
	Email nvarchar(50) NULL,
	DatumOtvaranjaDosijea datetime NOT NULL,
	DatumPoslednjeIntervencije datetime NULL,
	PoznatiAlergeni nvarchar(500) NULL,
	Zabeleske nvarchar(500) NULL
)

CREATE TABLE Zakazivanje.Usluge
(
	UslugaID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	SifraUsluge int NOT NULL,
	Naziv nvarchar(100) NOT NULL,
	Tip nvarchar(30) NULL,
	Trajanje int NOT NULL,
	Opis nvarchar(300) NULL,
	Cena money NOT NULL,
	Primedbe nvarchar(500) NULL
)

CREATE TABLE Zakazivanje.Radnici
(
	RadnikID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Ime nvarchar(30) NOT NULL,
	Prezime nvarchar(30) NOT NULL,
	Pozicija int NOT NULL,
	Telefon nvarchar(30) NULL,
	Email nvarchar(50) NULL,
	DatumZaposlenja date NULL
)

CREATE TABLE Zakazivanje.Termini
(
	TerminID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	DatumVreme datetime NOT NULL,
	KorisnikID int FOREIGN KEY REFERENCES Zakazivanje.Korisnici(KorisnikID) NOT NULL,
	RadnikID int FOREIGN KEY REFERENCES Zakazivanje.Radnici(RadnikID) NOT NULL,
	AdministratorID int FOREIGN KEY REFERENCES Zakazivanje.Radnici(RadnikID) NULL,
	UslugaID int FOREIGN KEY REFERENCES Zakazivanje.Usluge(UslugaID) NOT NULL
)

CREATE TABLE Prodaja.Proizvodi
(
	ProizvodID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	SifraProizvoda int NOT NULL,
	Naziv nvarchar(50) NOT NULL,
	Proizvodjac nvarchar(50) NULL,
	Cena money NOT NULL,
	Kategorija nvarchar(30) NULL,
	Stanje int NOT NULL
)

CREATE TABLE Prodaja.Porudzbine
(
	PorudzbinaID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	DatumPorudzbine datetime NOT NULL,
	Total money NOT NULL,
	KorisnikID int FOREIGN KEY REFERENCES Zakazivanje.Korisnici(KorisnikID) NULL
)

CREATE TABLE Prodaja.DetaljiPorudzbine
(
	PorudzbinaID int FOREIGN KEY REFERENCES Prodaja.Porudzbine(PorudzbinaID) NOT NULL,
	ProizvodID int FOREIGN KEY REFERENCES Prodaja.Proizvodi(ProizvodID) NOT NULL,
	Kolicina int NOT NULL
	CONSTRAINT PK_DeteljiPorudzbine PRIMARY KEY (PorudzbinaID, ProizvodID)
)
GO

INSERT INTO Zakazivanje.Radnici VALUES('Bojana','Vasovic','1','011555555','boki@gmail.com','1.1.2010')
INSERT INTO Zakazivanje.Radnici VALUES('Jovana','Jovanovic','1','011444444','joca@gmail.com','1.2.2012')
INSERT INTO Zakazivanje.Radnici VALUES('Ivana','Ivanovic','1','011333333','ika@gmail.com','2.5.2014')
INSERT INTO Zakazivanje.Radnici VALUES('N.','N.','2','/','/','1.1.2010')

INSERT INTO Zakazivanje.Korisnici VALUES('Test', 'Korisnik', '1', '1.1.1995', '011123456', 'test@gmail.com', '1.1.2010', '1.1.2010', 'Nema poznatih alergena', 'Zabeleske')
INSERT INTO Zakazivanje.Usluge VALUES('1111', 'Test', 'usluga', '3', 'Test usluga, trajanje: 1:30', '1500', 'Nema primedbi')
INSERT INTO Zakazivanje.Proizvodi VALUES('2222', 'Test', 'proizvod', '800', 'Samponi', '15')

