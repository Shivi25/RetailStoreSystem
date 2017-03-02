CREATE PROCEDURE Q16
 @EMPLOYEECODE int
AS

BEGIN
 SET NOCOUNT ON;

delete from WORKS_IN 
where  personcode = @EMPLOYEECODE


end