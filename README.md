# viventium-data-store

When you first run the app, if you encounter this error: `Could not find a part of the path ... bin\roslyn\csc.exe`, please clean the solution and rebuild all then try again.

Swagger support has been added for your convenience (`/swagger`)

If you use Swagger When testing the `DataStore` post API, please make sure to add `=` before your CSV content and pick `application/x-www-form-urlencoded` as the parameter content type.

Here is the connection string in use:

`Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|ViventiumDataStore.mdf;Integrated Security=True;`
Please feel free to change it if it's not compatible with your system.
