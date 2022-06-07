use DirectumTask

with firstQuery (FullName,SumQuantity) as
(
SELECT [Surname] +' ' + sls.[Name] as [Fullname], Sum(Quantity) as [SumQuantity]
FROM [Sales] AS s
JOIN [Sellers] as sls on s.[IDSel]=sls.[ID]
WHERE CONVERT(DATETIME, s.[Date], 101)>= CONVERT(DATETIME, '20131001', 101) OR CONVERT(DATETIME, s.[Date], 101)>= CONVERT(DATETIME, '20141007', 101)
GROUP BY [Surname] +' ' + sls.[Name]
)

select
FQ.FullName,p.[Name], (100. * sum(s.Quantity)) / sum(sum(s.Quantity)) over (partition by p.[Name]) as [%]
from
 Sales s join
 Products p on p.ID = s.IDProd JOIN
 Sellers r on r.ID = s.IDSel JOIN
 firstQuery FQ on FQ.FullName=(r.Surname+' '+r.Name)
where
 s.Date between '20131001' and '20141007'
group by
FQ.FullName,p.[Name];
