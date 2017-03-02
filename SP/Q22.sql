select sum(price*quantity) as totalprice from exists_in as e
inner join item i
on e.ITEMCODE = i.ITEMCODE