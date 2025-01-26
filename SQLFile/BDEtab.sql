CREATE DATABASE BDEtab;
go
USE BDEtab;

CREATE TABLE Etablissement (
    idEtablissement INT IDENTITY(1,1) PRIMARY KEY,
    nomEtab VARCHAR(100)      NOT NULL,                 
    delaiInscription INT      NOT NULL,                         
    contact VARCHAR(50)       NOT NULL,                           
    nomProviseur VARCHAR(100) NOT NULL,    
    prenomProviseur VARCHAR(100)         NOT NULL,                
    Region VARCHAR(50)        NOT NULL,                            
    dateRentre DATE           NOT NULL,                               
    province VARCHAR(50)      NOT NULL,                          
    ville VARCHAR(50)         NOT NULL,                             
    secteur VARCHAR(50)       NOT NULL                           
);


CREATE TABLE db_accessadmin.Utilisateur (
    idUtilisateur INT IDENTITY(1, 1) NOT NULL,
    nom VARCHAR(50) NOT NULL,
    prenom VARCHAR(50) NOT NULL,
    Role VARCHAR(50) NOT NULL,
    courriel VARCHAR(100) NOT NULL,
    motDePasse VARCHAR(100) NOT NULL,
    sel VARCHAR(100) NOT NULL,
    date_naissance VARCHAR(50) NULL,
    PasswordResetToken VARCHAR(100) NULL,
    ResetTokenExpires DATETIME NULL,
    IsEmailVerified BIT NOT NULL DEFAULT 0,
    VerifyEmailToken NVARCHAR(100) NULL,
    emailTokenExpiration DATETIME NULL,
    CONSTRAINT PK_Utilisateur PRIMARY KEY CLUSTERED (idUtilisateur ASC)
);


CREATE TABLE SeSouvenirTokens (
    Id INT PRIMARY KEY IDENTITY,
    UserEmail NVARCHAR(256) NOT NULL,
    Token NVARCHAR(512) NOT NULL,
    DateExpiration DATETIME NOT NULL
);
CREATE TABLE db_accessadmin.TentativesConnexion (
    Id INT IDENTITY(1, 1) NOT NULL,
    Courriel NVARCHAR(65) NOT NULL,
    Tentatives INT NOT NULL DEFAULT 0,
    DateDeblocage DATETIME NULL,
    CONSTRAINT PK_TentativesConnexion PRIMARY KEY CLUSTERED (Id ASC)
);

CREATE TABLE Classe (
    idClasse INT IDENTITY(1,1) PRIMARY KEY,        
    nomClasse NVARCHAR(100) NOT NULL,              
    nbEleves INT   NOT NULL,                                  
    idEtablissement INT    NOT NULL,                           
    FOREIGN KEY (idEtablissement) REFERENCES Etablissement(idEtablissement) ON DELETE NO ACTION
);

CREATE TABLE Eleve (
    idEleve INT IDENTITY(1,1) PRIMARY KEY,  -- Nouvelle clé primaire
    noPv    INT       NOT NULL,
    idEtablissement INT NOT NULL,
    idClasse        INT NOT NULL,
    nom VARCHAR(50) NOT NULL,    
    prenom VARCHAR(50) NOT NULL, 
    date_naissance DATE NOT NULL,
    FOREIGN KEY(idEtablissement) REFERENCES Etablissement(idEtablissement),
    FOREIGN KEY(idClasse) REFERENCES Classe(idClasse)

);



CREATE TABLE Cour (
    idCour INT IDENTITY(1,1) PRIMARY KEY,          
    nomCour NVARCHAR(100) NOT NULL,                
    nomProf NVARCHAR(100) NOT NULL,                         
    dureeSemaine INT      NOT NULL,                              
    dureeJour INT         NOT NULL,                                 
    idClasse INT          NOT NULL,                                  
    FOREIGN KEY (idClasse) REFERENCES Classe(idClasse) ON DELETE CASCADE
);
  
CREATE TABLE Notifications (
    idNotifications INT IDENTITY(1,1) PRIMARY KEY, 
    date DATETIME               NOT NULL,                            
    message NVARCHAR(MAX)       NOT NULL,                
    vue BIT DEFAULT 0           NOT NULL,                             
   
);

CREATE TABLE Message (
    idMessage INT IDENTITY(1,1) PRIMARY KEY, 
    date DATETIME                    NOT NULL,                            
    message NVARCHAR(MAX)            NOT NULL,                
    idUtilisateur                   INT       NOT NULL,
    idEtablissement        INT       NOT NULL,                          
    FOREIGN KEY (idUtilisateur) REFERENCES Utilisateur(idUtilisateur) ON DELETE NO ACTION,
    FOREIGN KEY (idEtablissement) REFERENCES Etablissement(idEtablissement) ON DELETE NO ACTION
);

CREATE TABLE PhotoEleve (
    noPhoto INT IDENTITY(1,1) PRIMARY KEY,         
    sourcePhoto NVARCHAR(255) NOT NULL,            
    idEleve INT                  NOT NULL,                                      
    FOREIGN KEY (idEleve) REFERENCES Eleve(idEleve) ON DELETE NO ACTION
);



CREATE TABLE Disponibilite (
    idDisponibilite INT IDENTITY(1, 1) NOT NULL,
    idUtilisateur INT NOT NULL,  -- L'ID de l'utilisateur ayant un rôle administrateur
    date DATE NOT NULL,
    heureDebut TIME NOT NULL,
    heureFin TIME NOT NULL,
    statut VARCHAR(20) NOT NULL DEFAULT 'Disponible', -- Statut: Disponible, Réservée, etc.
    CONSTRAINT PK_Disponibilite PRIMARY KEY CLUSTERED (idDisponibilite ASC),
    CONSTRAINT FK_Disponibilite_Utilisateur FOREIGN KEY (idUtilisateur) REFERENCES db_accessadmin.Utilisateur(idUtilisateur)  -- Lien avec l'utilisateur
);

CREATE TABLE RendezVous (
    idRendezVous INT IDENTITY(1, 1) NOT NULL,
    idEleve INT NOT NULL,  -- L'ID de l'élève ayant pris le rendez-vous
    idDisponibilite INT NOT NULL,  -- L'ID de la disponibilité réservée
    dateHeureReservation DATETIME NOT NULL,  -- La date et l'heure à laquelle le rendez-vous a été pris
    statut VARCHAR(20) NOT NULL DEFAULT 'Réservé',  -- Statut: Réservé, Annulé, Terminé, etc.
    CONSTRAINT PK_RendezVous PRIMARY KEY CLUSTERED (idRendezVous ASC),
    CONSTRAINT FK_RendezVous_Eleve FOREIGN KEY (idEleve) REFERENCES Eleve(idEleve),  -- Lien avec l'élève
    CONSTRAINT FK_RendezVous_Disponibilite FOREIGN KEY (idDisponibilite) REFERENCES db_accessadmin.Disponibilite(idDisponibilite)  -- Lien avec la disponibilité
);

-- Table pour stocker les informations de profil de l'utilisateur
CREATE TABLE ProfileUtilisateur (
    idUtilisateur INT NOT NULL,  -- Clé étrangère qui fait référence à l'utilisateur
    courriel VARCHAR(100) NOT NULL,  -- Email de l'utilisateur
    motDePasse VARCHAR(100) NOT NULL,  -- Mot de passe de l'utilisateur
    dateModification DATETIME NOT NULL,  -- Date de la dernière modification
    CONSTRAINT PK_ProfileUtilisateur PRIMARY KEY CLUSTERED (idUtilisateur ASC),
    CONSTRAINT FK_ProfileUtilisateur_Utilisateur FOREIGN KEY (idUtilisateur) REFERENCES Utilisateur(idUtilisateur)  -- Lien avec l'utilisateur
);
-- Table pour stocker les informations de profil de l'élève
CREATE TABLE ProfileEleve (
    idEleve INT NOT NULL,  -- Clé étrangère qui fait référence à l'élève
    noPhoto INT NOT NULL,  -- L'identifient de l'utilisateur
    courriel VARCHAR(255) NULL,  -- Adresse de l'élève
    dateModification DATETIME NOT NULL DEFAULT GETDATE(),  -- Date de la dernière modification
    CONSTRAINT PK_ProfileEleve PRIMARY KEY CLUSTERED (idEleve ASC),
    CONSTRAINT FK_ProfileEleve_Eleve FOREIGN KEY (idEleve) REFERENCES Eleve(idEleve),  -- Lien avec l'élève
    CONSTRAINT FK_Photo_Eleve FOREIGN KEY (noPhoto) REFERENCES PhotoEleve(noPhoto)  -- Lien avec l'élève 
);
-- Table Classe
ALTER TABLE Classe
ADD 
    CONSTRAINT FK_Classe_Etablissement FOREIGN KEY (idEtablissement) 
        REFERENCES Etablissement(idEtablissement) ON DELETE NO ACTION;

-- Table Eleve
ALTER TABLE Eleve
ADD 
    CONSTRAINT FK_Eleve_Etablissement FOREIGN KEY (idEtablissement) 
        REFERENCES Etablissement(idEtablissement) ON DELETE CASCADE,
    CONSTRAINT FK_Eleve_Classe FOREIGN KEY (idClasse) 
        REFERENCES Classe(idClasse) ON DELETE CASCADE;

-- Table Cour
ALTER TABLE Cour
ADD 
    CONSTRAINT FK_Cour_Classe FOREIGN KEY (idClasse),
        REFERENCES Classe(idClasse) ON DELETE NO ACTION;



-- Table Message
ALTER TABLE Message
ADD 
    CONSTRAINT FK_Message_Utilisateur FOREIGN KEY (idUtilisateur)
        REFERENCES Utilisateur(idUtilisateur) ON DELETE NO ACTION,
    CONSTRAINT FK_Message_Etablissement FOREIGN KEY (idEtablissement)
        REFERENCES Etablissement(idEtablissement) ON DELETE NO ACTION;

-- Table PhotoUtilisateur
ALTER TABLE PhotoUtilisateur
ADD 
    CONSTRAINT FK_PhotoUtilisateur_Utilisateur FOREIGN KEY (idUtilisateur)
        REFERENCES Utilisateur(idUtilisateur) ON DELETE NO ACTION;