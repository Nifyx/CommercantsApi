CREATE TRIGGER UPDATED_AT_ROLE ON [Role] AFTER UPDATE
AS 
BEGIN
    SET NOCOUNT ON;
    UPDATE [Role] Set Updated_At = GetDate() where Id in (SELECT Id FROM Inserted)
END