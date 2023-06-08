docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YCMkiuX2CPog98l" -p 1433:1433 --name sql1 --hostname sql1 -d mcr.microsoft.com/mssql/server:2022-latest

docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=12345678' -p 1433:1433 -v projectracking/data:/var/opt/mssql/data -v projectracking/log:/var/opt/mssql/log -v projectracking/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2017-latest

docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=YCMkiuX2CPog98l' -p 1433:1433 --name sql1 --hostname sql1 -v data:/var/opt/mssql/data -d mcr.microsoft.com/mssql/server:2017-latest

Microsoft.Data.SqlClient.SqlException: 'Login failed for user 'sql1\Guest'.'