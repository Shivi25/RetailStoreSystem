CREATE PROCEDURE Q12
	@X varchar (4),
	@Y varchar (4)
AS	
BEGIN
	SET NOCOUNT ON;
DECLARE @g geometry;
 SET @g = geometry::STGeomFromText('POINT('+@X+' '+@Y+')', 0);

select top 5 S.STORECODE, LOCATION.STDistance(@g) as distance, A.X, A.Y 
from ADDRESS A
INNER JOIN STORE S ON S.ADDRESSCODE = A.ADDRESSCODE
order by distance asc, S.STORECODE asc
END