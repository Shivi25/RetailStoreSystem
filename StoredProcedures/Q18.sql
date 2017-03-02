CREATE PROCEDURE Q18
 @storecode int
AS

BEGIN
SET NOCOUNT ON;

select  sum(it.PRICE*i.QUANTITY) as totalprice   
from transactions t
inner join includes as i on t.TRANSACTIONCODE = i.TRANSACTIONCODE
inner join item as it on it.ITEMCODE = i.ITEMCODE
where t.STORECODE = @storecode
end