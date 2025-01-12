CREATE PROCEDURE ValidateRememberMeToken
    @Token NVARCHAR(512),
    @Now DATETIME,
    @UserEmail NVARCHAR(256) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Récupérer l'email si le token est valide
    SELECT @UserEmail = UserEmail
    FROM RememberMeTokens
    WHERE Token = @Token AND ExpirationDate > @Now;

    -- Si l'email est trouvé, @UserEmail aura une valeur, sinon il sera NULL
    -- Il n'est pas nécessaire de faire un SELECT ici, car @UserEmail est déjà un paramètre OUTPUT
END;

