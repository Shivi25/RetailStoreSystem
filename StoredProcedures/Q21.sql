select ((count(distinct t.PERSONCODE )*100)/(select count(*) from PERSON )) as percentage
 from
TRANSACTIONS as t
inner join PERSON p on p.PERSONCODE = t.PERSONCODE
inner join ADDRESS as p_a on p_a.ADDRESSCODE = p.ADDRESSCODE
inner join STORE s on s.STORECODE = t.STORECODE
inner join ADDRESS s_a on s_a.ADDRESSCODE = s.ADDRESSCODE
WHERE p_a.CITY != s_a.CITY