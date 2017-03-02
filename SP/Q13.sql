CREATE PROCEDURE Q13
	@X varchar (4),
	@Y varchar (4),
	@range float
AS	
BEGIN
	SET NOCOUNT ON;
DECLARE @g geometry;
 SET @g = geometry::STGeomFromText('POINT('+@X+' '+@Y+'  )', 0);

select S.STORECODE, LOCATION.STDistance(@g) as distance, A.X, A.Y 
from ADDRESS A
INNER JOIN STORE S ON S.ADDRESSCODE = A.ADDRESSCODE
where LOCATION.STDistance(@g) < @range

END