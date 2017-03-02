CREATE PROCEDURE Q17
 @itemcode int
AS

BEGIN
 SET NOCOUNT ON;
 select top 1 itemcode, count(TRANSACTIONCODE) count from INCLUDES
 where TRANSACTIONCODE in (select TRANSACTIONCODE from INCLUDES where itemcode=@itemcode) 
 and ITEMCODE != @itemcode  
 group by  itemcode
 order by count desc

 end