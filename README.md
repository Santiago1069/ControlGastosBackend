Cada vez que uno vaya a crear un modelo para la creacion de una tabla tiene que ejecutar en la conosla de lops paquetes nuget el siguiente comandos:

"dotnet ef migrations add Inicial" -> Initial es el nombre que se le da a la migracion, este puede cambiar dependiendo de la tabla que se este creando.
"dotnet ef database update" -> Este comando es para actualizar la base de datos con la migracion que se acaba de crear.
