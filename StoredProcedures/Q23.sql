CREATE PROCEDURE Q23
	@X1 varchar (4),
	@Y1 varchar (4),
	@X2 varchar (4),
	@Y2 varchar (4),
	@X3 varchar (4),
	@Y3 varchar (4),
	@X4 varchar (4),
	@Y4 varchar (4)
	
AS

BEGIN
	SET NOCOUNT ON;
DECLARE @g geometry;

SET @g = geometry::STGeomFromText('POLYGON(('+@X1+' '+@Y1+', '+@X2+' '+@Y2+', '+@X3+' '+@Y3+' , '+@X4+' '+@Y4+', '+@X1+' '+@Y1+'))', 0);

select S.STORECODE, A.X, A.Y 
from ADDRESS A
INNER JOIN STORE S ON S.ADDRESSCODE = A.ADDRESSCODE
WHERE A.LOCATION.STIntersects(@g) = 1
ORDER BY S.STORECODE

END