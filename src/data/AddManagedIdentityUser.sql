CREATE USER TechExcelAzurePaaS FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER TechExcelAzurePaaS;
ALTER ROLE db_datawriter ADD MEMBER TechExcelAzurePaaS;

