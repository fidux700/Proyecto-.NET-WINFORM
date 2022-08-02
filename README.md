# Proyecto-.NET-WINFORM-

Proyecto que simula un catalogo de articulos que se podria utilizar para cualquier tipo de industria.

Tecnologias usadas: C#, .NET, Winform.NetFramework

PARA REALIZAR LA CONEXION CON LA BASE DE DATOS:
*Aclaracion importante: Para poder ejecutar el programa se debe tener el SQL Server instalado.

1- Primero debe arrastrar el archivo sql que se encuentra en la carpeta "ScriptSQL" hacia el SQL Server, ejecutar el mismo script con F5 dentro del comando de SQL.

2- Una vez ejecutado, deben dirigirse a la ruta de acceso y hacer click derecho - Refresh (para poder actualizar los datos y se muestre en pantalla la base recientemente agregada)

![image](https://user-images.githubusercontent.com/71730230/181391957-f53692fd-9e8a-44e2-9b5e-5970cf3cb4cc.png)

3- Luego abrir la app con Visual Studio
![image](https://user-images.githubusercontent.com/71730230/181392090-bce2a3a5-781e-48a6-a0cc-041e4ba1bcf1.png)


4- Dirigirse a la clase "ConexionDB".cs y abrir el metodo "ConexionRaiz()"
![image](https://user-images.githubusercontent.com/71730230/181392335-5610d2cc-85d1-4d62-9ca2-bddf41468f39.png)


5- Por ultimo para que funcione verifique que los datos que se encuentran en esa linea de codigo sean correspondidos con los suyos.
Deberá modificar si o si la credencial de usuario, ya que los establecidos son propios de mi login.
![image](https://user-images.githubusercontent.com/71730230/181392482-df443de9-781d-4409-80f2-1a6fa020b131.png)


Espero que haya podido tener exito con la conexion, de igual manera a continuacion le comparto documentacion original del connectionstring.
https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=dotnet-plat-ext-6.0

Gracias!
