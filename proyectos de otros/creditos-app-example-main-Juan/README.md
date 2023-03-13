Proyecto que simula el funcionamiento de una entidad de crédito

# Getting Started
1.	Clonar el repositorio
2.	Cambiar los strings de conexion en el appsettings que se encuentra en la raíz del proyecto app service

# Build and Test
> Windows PS creditos-app-example>
> ``` powershell
> dotnet run --project .\Creditos\src\Applications\Creditos.AppServices\
> ```

> Linux / Macos creditos-app-example$
>``` bash
>dotnet run --project ./Creditos/src/Applications/Creditos.AppServices/
>```

Para probar los métodos a través de un cliente de nest clone el repositorio con su respectivo submodulo

``` bash
git clone --recurse-submodules https://git.sofka.com.co/juan.arce/creditos-app-example.git

o

git clone --recurse-submodules git@git.sofka.com.co:juan.arce/creditos-app-example.git

```
