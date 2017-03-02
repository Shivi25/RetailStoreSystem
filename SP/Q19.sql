CREATE PROCEDURE Q19
 @storecode int
AS

BEGIN
SET NOCOUNT ON;

select  sum(w.WEEKLY_HOURS*p.HOURLY_SALARY) as weeklysalary  
from  WORKS_IN as w 
inner join POSITION as p on p.job = w.JOB
where w.storecode = @storecode

end