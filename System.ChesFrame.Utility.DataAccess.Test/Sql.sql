
--Create Table
use UserManager
create table Person
(
	ID int identity(1,1),
	Name nvarchar(20),
	Age int
)

--Insert Data
insert into Person
(Name,Age)
values('张三',500)

insert into Person
(Name,Age)
values('李四',600)


--Select All
select * from Person

--Paging Select
SELECT @TotalCount = COUNT(1)
FROM Person

SELECT *
FROM 
(
	SELECT * ,
		ROW_NUMBER() OVER (ORDER BY @OrderBy) AS [SN]
	FROM 
	Person
)T
WHERE [SN] BETWEEN @RowFrom AND @RowTo

--Create Proc
GO;
CREATE PROC PQueryPeron
AS
BEGIN
	SELECT * FROM Person
END