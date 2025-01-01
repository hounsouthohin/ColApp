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


CREATE TABLE Utilisateur (
    idUtilisateur INT IDENTITY(1,1) PRIMARY KEY,  -- Nouvelle clé primaire
    nom VARCHAR(50) NOT NULL,    
    prenom VARCHAR(50) NOT NULL, 
    Role VARCHAR(50) NOT NULL,
    courriel VARCHAR(100)   NOT NULL, 
    motDePasse VARCHAR(100) NOT NULL,
    sel VARCHAR(100)  NOT NULL,
    date_naissance VARCHAR NOT NULL,
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

CREATE TABLE PhotoUtilisateur (
    noPhoto INT IDENTITY(1,1) PRIMARY KEY,         
    sourcePhoto NVARCHAR(255) NOT NULL,            
    idUtilisateur INT                  NOT NULL,                                      
    FOREIGN KEY (idUtilisateur) REFERENCES Utilisateur(idUtilisateur) ON DELETE NO ACTION
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