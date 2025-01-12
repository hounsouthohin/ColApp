ALTER PROCEDURE InsertRememberMeToken
    @UserEmail NVARCHAR(256),
    @Token NVARCHAR(512),
    @ExpirationDate NVARCHAR(60)
AS
BEGIN
    -- Activer les options de transaction sécurisée
    SET NOCOUNT ON;

    -- Insérer les données dans la table
    INSERT INTO SeSouvenirTokens (UserEmail, Token, DateExpiration)
    VALUES (@UserEmail, @Token, @ExpirationDate);
END;
