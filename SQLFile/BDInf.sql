

-- Ajouter des établissements
INSERT INTO Etablissement (nomEtab, delaiInscription, contact, nomProviseur, prenomProviseur, Region, dateRentre, province, ville, secteur)
VALUES
('Etablissement A', 30, 'contact@etablissementA.com', 'Paul', 'Martin', 'Region1', '2024-09-01', 'Province1', 'Ville1', 'Secteur A'),
('Etablissement B', 20, 'contact@etablissementB.com', 'Marie', 'Durand', 'Region2', '2024-09-01', 'Province2', 'Ville2', 'Secteur B');


-- Ajouter des classes
INSERT INTO Classe (nomClasse, nbEleves, idEtablissement)
VALUES
('Classe 1A', 25, 1),  -- Etablissement A
('Classe 1B', 30, 1),  -- Etablissement A
('Classe 2A', 28, 2),  -- Etablissement B
('Classe 2B', 32, 2);  -- Etablissement B

-- Utilisateurs avec des informations partagées avec certains élèves
EXEC ajoutUtilisateur 'Dupont', 'Jean', 'Administrateur', 'jean.dupont@example.com', '1990-05-12', 'admin123';
EXEC ajoutUtilisateur 'Martin', 'Paul', 'Proviseur', 'paul.martin@example.com', '1985-03-23', 'proviseur123';
EXEC ajoutUtilisateur 'Leclerc', 'Sophie', 'Utilisateur limité', 'sophie.leclerc@example.com', '2000-08-15', 'motdepasse123';
EXEC ajoutUtilisateur 'Benoit', 'Michel', 'Utilisateur limité', 'michel.benoit@example.com', '1995-11-25', 'motdepasse456';
EXEC ajoutUtilisateur 'Lemoine', 'Claire', 'Utilisateur limité', 'claire.lemoine@example.com', '1998-02-10', 'motdepasse789';

-- Ajouter des élèves (en lien avec des utilisateurs)
-- Jean Dupont et Luc Gauthier ont les mêmes informations (nom, prénom, date de naissance)
INSERT INTO Eleve (noPv, idEtablissement, idClasse, nom, prenom,date_naissance)
VALUES
(12345, 1, 1, 'Dupont', 'Jean', '1990-05-12'),
(12346, 1, 2, 'Dubois', 'Alice', '2005-06-25'),
(12347, 2, 3, 'Lemoine', 'Claire', '1998-02-10'),
(12348, 2, 4, 'Petit', 'Julien','2004-09-05');

-- Michel Benoit et Julien Petit ont les mêmes informations (nom, prénom, date de naissance)
INSERT INTO Eleve (noPv, idEtablissement, idClasse, nom, prenom,date_naissance)
VALUES
(12349, 1, 1, 'Benoit', 'Michel','1995-11-25'),
(12350, 2, 4, 'Petit', 'Julien','2004-09-05');



-- Ajouter des cours
INSERT INTO Cour (nomCour, nomProf, dureeSemaine, dureeJour, idClasse)
VALUES
('Mathématiques', 'Mme Lefevre', 2, 1, 1),  -- Classe 1A
('Français', 'M. Dubois', 3, 2, 2),          -- Classe 1B
('Physique', 'M. Girard', 1, 1, 3),          -- Classe 2A
('Histoire', 'Mme Durand', 2, 1, 4);         -- Classe 2B

-- Ajouter des messages
INSERT INTO Message (date, message, idUtilisateur, idEtablissement)
VALUES
('2024-12-01 08:00:00', 'Demande de renseignement pour l''inscription', 1, 1),  -- Jean Dupont (Admin) vers Etablissement A
('2024-12-02 11:00:00', 'Problème avec le mot de passe', 2, 2);                  -- Paul Martin (Proviseur) vers Etablissement B


-- Ajouter des photos d'utilisateur
INSERT INTO PhotoUtilisateur (sourcePhoto, idUtilisateur)
VALUES
('https://example.com/photo1.jpg', 1),  -- Photo pour Jean Dupont
('https://example.com/photo2.jpg', 2);  -- Photo pour Paul Martin
