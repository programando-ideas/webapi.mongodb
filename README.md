# ASP.NET Core WebApi + MongoDb

Proyecto con operaciones CRUD sobre **MongoDb** expuestas en una WebApi hecha con **ASP.NET Core 3.1***

## Videos Youtube (𝖠𝖲𝖯.𝖭𝖤𝖳 𝖢𝗈𝗋𝖾 𝖶𝖾𝖻𝖠𝗉𝗂 + 𝖬𝗈𝗇𝗀𝗈𝖣𝖡 🚀🍃🎞)
- [🎥 Parte I](https://youtu.be/mI64TjWxVgI)
- [🎥 Parte II](https://youtube.com/programandoideas)

### Herramientas necesarias para este video

- [x] [MongoDb Server Community](https://www.mongodb.com/download-center/community)
- [x] [MongoDb Compass](https://www.mongodb.com/download-center/compass)
- [x] [Visual Studio 2019 Community](https://visualstudio.microsoft.com/es/vs/community/)
- [x] [.net Core](https://dotnet.microsoft.com/download)
- [x] [Postman free](https://www.postman.com/downloads/)
- [x] [SoapUI Open source](https://www.soapui.org/downloads/soapui/)

## Startup.cs
```csharp
services.Configure<ClientesStoreDatabaseSettings>(
                 Configuration.GetSection(nameof(ClientesStoreDatabaseSettings)));

services.AddSingleton<IClientesStoreDatabaseSettings>(sp =>
                 sp.GetRequiredService<IOptions<ClientesStoreDatabaseSettings>>().Value);

services.AddSingleton<ClientesDb>();
services.AddSingleton<ClientesDbAsync>();
services.AddSingleton<ClientesDbQueryable>();

// http://mongodb.github.io/mongo-csharp-driver/2.0/reference/driver/connecting/#re-use
services.AddSingleton<IClientSettingsService, ClientSettingsServiceMongoDB>();
```

## Elemplo de URLs para utilizar con Postman
#### GET
- http://localhost:5000/api/clientes/5eac64158a01245a2c89fb0c
- http://localhost:5000/api/clientesiq/getbydir/123
- http://localhost:5000/api/clientesasync

#### POST
- http://localhost:5000/api/clientes
```json
{
  "nombre": "Cliente 1",
  "apellido": "Apellido 1",
  "edad": 37,
  "telefonos": [
    {
      "id": 1,
      "tel": "+555555555555"
    },
    {
      "id": 2,
      "tel": "+111111111111"
    },
    {
      "id": 3,
      "tel": "+222222222222"
    },
    {
      "id": 4,
      "tel": "+999999999999"
    }
  ],
  "direccionCliente": {
    "id": 0,
    "calle": "Calle 123",
    "numero": "133",
    "depto": "3a"
  }
}
```
#### PUT
```json
{
  "id": "{Id Creado}",
  "nombre": "Cliente 1 12345",
  "apellido": "Apellido 1",
  "edad": 37,
  "telefonos": [
    {
      "id": 1,
      "tel": "+555555555555"
    },
    {
      "id": 2,
      "tel": "+111111111111"
    },
    {
      "id": 3,
      "tel": "+222222222222"
    },
    {
      "id": 4,
      "tel": "+999999999999"
    }
  ],
  "direccionCliente": {
    "calle": "Calle 123",
    "numero": "133",
    "depto": "3a"
  }
}
```
#### DEL
- http://localhost:5000/api/clientes/5eac64158a01245a2c89fb0c

## Directorio JsonTest
#### Json con ejemplos para crear y actualizar un cliente
- CREATE_POST.json
- UPDATE_PUT.json
#### Proyecto de SoapUI para ejecutar el "Load Test"
- LoadTest_MongoDB_API.xml

------------
#### Programando Ideas 2020
<p>
  <a href="https://paypal.me/lp8126" target="_blank">
    <img src="https://www.paypalobjects.com/es_XC/MX/i/btn/btn_donateCC_LG.gif" border="0" alt="Donar con PayPal" />
  </a>
</p>
