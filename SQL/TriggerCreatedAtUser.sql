CREATE TRIGGER CREATED_AT_USER ON [dbo].[User] AFTER INSERT
AS 
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[User] Set Created_at = GetDate() where Id in (SELECT Id FROM Inserted)
END