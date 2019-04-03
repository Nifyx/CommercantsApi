CREATE TRIGGER CREATED_AT_ROLE ON [dbo].[Role] AFTER INSERT
AS 
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Role] Set Created_at = GetDate() where Id in (SELECT Id FROM Inserted)
END