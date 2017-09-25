xcopy migrate.exe .\bin\Debug /F /R /Y

cd .\bin\Debug

migrate.exe Restaurants.Corp.Dal.dll /startupConfigurationFile="..\..\App.config"