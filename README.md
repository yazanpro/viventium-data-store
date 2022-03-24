# viventium-data-store

When you first run the app, if you encounter this error: `Could not find a part of the path ... bin\roslyn\csc.exe`, please clean the solution and rebuild all then try again.

I just noticed a weird bug when you use the DataStore post API for the very first time. What happens is that when you call this API, the initial migration runs (including database creation). For some reason, the initial seed of the CompanyId column (in Companies table) is starting at 0, which means records will have the IDs (0, 1, 2...). This only happens when you first call this API (or any other API that tries to access the database). All subsequent calls of the DataStore API will fix the issue since it clears old data and re-imports new data. This is obviously a bug that can be fixed. But for the time being, please call the API twice the first time, so IDs get the correct values (1, 2, 3,...)

Swagger support has been added for your convenience (`/swagger`)

If you use Swagger When testing the `DataStore` post API, please make sure to add `=` before your CSV content and pick `application/x-www-form-urlencoded` as the parameter content type.

Here is the connection string in use:

`Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|ViventiumDataStore.mdf;Integrated Security=True;`
Please feel free to change it if it's not compatible with your system.
