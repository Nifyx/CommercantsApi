CREATE TRIGGER CREATED_AT_SHOP ON [dbo].[Shop] AFTER INSERT
AS 
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Shop] Set Created_at = GetDate() where Id in (SELECT Id FROM Inserted)
END