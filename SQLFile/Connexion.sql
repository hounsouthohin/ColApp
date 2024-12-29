CREATE PROCEDURE connexion(@courriel CHAR(65), -- Taille du courriel à 65 caractères
                            @motDePasse NVARCHAR(64), 
                            @valeurConnexion INT OUTPUT)
AS
BEGIN
    SET NOCOUNT ON;

    -- Déclaration de la variable pour l'ID de l'utilisateur
    DECLARE @NO INT;

    -- Vérifier si le courriel et le mot de passe sont valides
    IF NOT EXISTS (SELECT 1 FROM Utilisateur 
                   WHERE courriel = @courriel 
                   AND motDePasse = HASHBYTES('SHA2_512', @motDePasse + CAST(sel AS NVARCHAR(36))))
    BEGIN
        -- Si la combinaison est incorrecte, retournez -1
        SET @valeurConnexion = -1;
        RETURN;
    END
    ELSE
    BEGIN
        -- Si la combinaison est correcte, récupérez l'ID utilisateur
        SET @NO = (SELECT idUtilisateur 
                   FROM Utilisateur 
                   WHERE courriel = @courriel 
                   AND motDePasse = HASHBYTES('SHA2_512', @motDePasse + CAST(sel AS NVARCHAR(36))));

        -- Si un utilisateur valide est trouvé, retournez son ID
        IF (@NO IS NOT NULL)
        BEGIN
            SET @valeurConnexion = @NO;
        END
        ELSE
        BEGIN
            SET @valeurConnexion = -1; -- En cas d'échec
        END
    END
END;
