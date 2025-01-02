ALTER PROCEDURE ajoutUtilisateur
    @nom NVARCHAR(50),
    @prenom NVARCHAR(50),
    @role NVARCHAR(50) = 'Utilisateur',  -- Rôle par défaut
    @courriel NVARCHAR(100),
    @date_naissance NVARCHAR(50),
    @mdp NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Vérifier si un utilisateur avec le même courriel existe déjà
    IF NOT EXISTS (SELECT * FROM Utilisateur WHERE courriel = @courriel)
    BEGIN
        -- Générer un sel unique pour le mot de passe
        DECLARE @sel UNIQUEIDENTIFIER = NEWID();

        -- Vérifier le rôle basé sur le mot de passe
        IF CHARINDEX('admin', @mdp) > 0
        BEGIN
            SET @role = 'Administrateur';
        END
        ELSE IF CHARINDEX('proviseur', @mdp) > 0
        BEGIN
            SET @role = 'Proviseur';
        END
        ELSE
        BEGIN
            SET @role = 'Utilisateur';
        END

        -- Validation des rôles autorisés
        IF @role NOT IN ('Administrateur', 'Proviseur', 'Utilisateur')
        BEGIN
            PRINT 'Rôle non valide. Le rôle sera défini sur "Utilisateur".';
            SET @role = 'Utilisateur';  -- Rôle par défaut si l'assignation est invalide
        END

        -- Vérifier si un administrateur existe déjà pour cet établissement
        IF @role = 'Administrateur'
        BEGIN
            IF EXISTS (SELECT 1 FROM Utilisateur WHERE Role = 'Administrateur')
            BEGIN
                PRINT 'Un administrateur existe déjà pour cet établissement. Le rôle sera attribué en tant qu''utilisateur limité.';
                SET @role = 'Utilisateur';
            END
        END

        -- Vérifier si un proviseur existe déjà pour cet établissement
        IF @role = 'Proviseur'
        BEGIN
            IF EXISTS (SELECT 1 FROM Utilisateur WHERE Role = 'Proviseur')
            BEGIN
                PRINT 'Un proviseur existe déjà pour cet établissement. Le rôle sera attribué en tant qu''utilisateur limité.';
                SET @role = 'Utilisateur';
            END
        END

        BEGIN TRY
            -- Insérer un nouvel utilisateur avec le mot de passe crypté et le sel
            INSERT INTO Utilisateur (nom, prenom, Role, courriel, motDePasse, sel, date_naissance)
            VALUES 
            (@nom, 
             @prenom, 
             @role, 
             @courriel, 
             CONVERT(VARBINARY(100), HASHBYTES('SHA2_512', @mdp + CAST(@sel AS NVARCHAR(36)))), 
             CAST(@sel AS NVARCHAR(36)),  -- Nous stockons le sel sous forme de texte (NVARCHAR)
             @date_naissance);
        END TRY
        BEGIN CATCH
            -- Gestion des erreurs : Ajouter du code pour gérer les erreurs si nécessaire
            PRINT 'Une erreur est survenue lors de l''ajout de l''utilisateur.';
        END CATCH
    END
    ELSE
    BEGIN
        -- Retourner -1 si l'utilisateur existe déjà
        RETURN -1;
    END
END;
GO



EXEC ajoutUtilisateur 
    @nom = 'sfsd', 
    @prenom = 'fdsds', 
    @date_naissance = '2003-12-24', 
    @courriel = 'orhelbambara@gmail.com', 
    @mdp = 'mevoici';
