CREATE PROCEDURE Q20
 @storecode int

AS

BEGIN
SET NOCOUNT ON;

select s.transaction_date as Bestdate 
from 
	(select top 1 (it.PRICE*i.QUANTITY) as totalprice, t.TRANSACTION_DATE  
	from transactions t
	inner join includes as i on t.TRANSACTIONCODE = i.TRANSACTIONCODE
	inner join item as it on it.ITEMCODE = i.ITEMCODE
	where t.STORECODE = @storecode
	order by totalprice desc 
	) as s 
end