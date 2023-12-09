SISTEMA DE GESTION ADMINISTRATIVA 

PROGRAMADOR:IVAN CERVANTES CRUZ 

PASOS PARA EJECUCION 

1-TENER INSTALADOS LOS SIGUIENTES PROGRAMAS: 

SQL Server Management Studio 19.2.56.2
Microsoft Visual Studio Community 2022 (64 bits) 

2- Ejecutar el Scritp llamado ´´DISETOP-BD actualizada-VERSION FINAL´´ de la base de datos de DISETOP 
para que cree las tablas, las llaves primarias y foraneas junto con los procedimientos almacenados necesarios

3-Abrir el proyecto DISETOP.sln lo puedes encontrar en la carpeta PRACTICA-GDC-main

4-Ir hasta al final en el explorador de soluciones en Visual Studio 2022 y abir el archivo llamado ¨Web.config¨

5-Al final de ese archivo vas a encontrar el conection string 

<connectionStrings>
  <add name="DISETOPEntities" connectionString="metadata=res://*/Models.disetop.csdl|res://*/Models.disetop.ssdl|res://*/Models.disetop.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=DESKTOP-L0GVN2O\SQLEXPRESS;initial catalog=DISETOP;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
</connectionStrings>

6- Remplazar data source=DESKTOP-L0GVN2O\SQLEXPRESS; por el nombre del servidor de la computadora en la que se quiera ejecutar el programa. 

7-Luego ir al controlador llamado AccesoController.cs ubicado en la carpeta Controllers, la podemos ver en el explorador de soluciones en Visual Studio 2022

8- Ahi vamos a encontrar en la linea 19 el siguiente codigo:

 static string cadena = "Data Source=DESKTOP-L0GVN2O\\SQLEXPRESS;Initial Catalog=DISETOP;Integrated Security=true";

9- Cambiar el Data Source=DESKTOP-L0GVN2O\\SQLEXPRESS; por el nombre del servidor de la computadora en la que se quiera ejecutar el programa.

10- Actualizar el modelo disetop.edmx si se hizo algun cambio en la base de datos con el fin de sincronizar el modelo con la base de datos ejecutada en la computadora

11- Ejecutar el programa

12- En el formulario de Ingreso pide un pin para poder registrar Usuarios, el pin es 0160 y de esa forma pueden ingresar usuarios y acceder al menu principal del sistema. 