CREATE TRIGGER UPDATED_AT_SHOP ON [Shop] AFTER UPDATE
AS 
BEGIN
    SET NOCOUNT ON;
    UPDATE [Shop] Set Updated_At = GetDate() where Id in (SELECT Id FROM Inserted)
END