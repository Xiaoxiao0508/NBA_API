select name 
from sys.databases
use [nba-db-main]
select *
from Player
SELECT table_catalog[database],table_schema [schema],table_name name,table_type type
FROM INFORMATION_SCHEMA.TABLES
GO

select * 
from Player
select *
from allPlayers

select *
from PlayerSelection
EXEC getPlayersFromTeam @teamName='Miami Heat',@SortingCol='FIRSTNAME',@SortType='DESC'

