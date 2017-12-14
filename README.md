## MyAuthSamples
### Resumen

Este repositorio contiene ejemplos de autorización y autenticación usando Indentity Server 4, junto con las especificaciones OAuth2 y OPenIdConnect (en breve llegará)

En la solución encontramos los siguientes proyectos:

**0_Overview**

Se corresponde con el el siguiente tutorial, donde encontraremos los primeros pasos par atrabajar con Identity Server:
http://docs.identityserver.io/en/release/quickstarts/0_overview.html

  Nos indica como preparar un proyecto visual studio core para "hostear" a identify server.  

**1_client_credentials**

   Authorization server (con Identity Server 4) que permite los siguientes grants de OAuth 2:
   
   * Client credentials
   * Resource owner password
   
   Para ello dispone de dos clientes, con sus correspondientes pares clientId/secret, más un par de usuarios (necesarios para el grant     resource owner password)
   
   Clientes: 
   
   <table>
    <tr><td>Cliente</td><td>ClientId</td><td>Secret</td></tr>
        <tr><td>Client credentials</td><td>client</td><td>secret</td></tr>
        <tr><td>Resource owner password</td><td>clientPwd</td><td>pwdsecret</td></tr>
    </table>

Usuarios:

   <table>
    <tr><td>Usuario</td><td>Password</td></tr>
    <tr><td>efrain</td><td>password</td></tr>
        <tr><td>tiberio</td><td>password</td></tr>        
    </table>

**2_core_client**

  Cliente .NET core que usa los dos grants anteriores para acceder a un API securizado (ver más abajo el proyecto 99_Api)
  
**3_js_client**
  
  Conjunto de clientes Javascript, que usando la librería simple-oauth2 (https://github.com/lelylan/simple-oauth2) acceden al API protegida y a GitHub usando diferentes grants y variantes de los mismos:
  
  * client_credentials.js
  
    Accede al API protegido usando el grant Client Credentials (en la tabla anterior de clientes vemos los clientId y secretos a usar)
  
  * client_password.js
  
  Accede al API protegido usando el grant Resource Owner Password (en la tabla anterior están el cliente y secreto a usar). Además se utiliza un de los usuarios disponibles
  
  * client_authorizationCodeApi.js
  
  Accede al API protegido usando el grant Authorization Code. Apunto a un Authorization Server que debe tener UI, ya que en el flujo somos redirigidos alli para introducir el par user/pwd. El proyecto 5_UI_server (más abajo) es el Authorization Server que da soporte a este caso
  
  * client_authorizationCodeApiRefresh.js
  
  Accede al API protegido usando el grant Authorization Code con soporte a refresh tokens. Apunto a un Authorization Server que debe tener UI, ya que en el flujo somos redirigidos alli para introducir el par user/pwd. El proyecto 5_UI_server (más abajo) es el Authorization Server que da soporte a este caso
  
  * client_authorizationCodeGithub.js
  
  Accede al API de github usando el grant Authorization Code. 
  
  * client_authorizationCodeGithubRefresh.js
  
  Accede al API de github usando el grant Authorization Code y tratando de de obtener refresh tokens, algo quer de momento no funciona en Github
  
Nota: el cliente y secreto de GitHub no es válido... ahorraos el tiempo y no intenteis acceder a mi cuenta :-P

**5_UI_server**

Authorization server que dispone de UI (con Identity Server 4) para habilitar el grant Authorization Code. Dispone de los siguientes clientes:
   
   * Client credentials
   * Resource owner password
   
   Para ello dispone de dos clientes, con sus correspondientes pares clientId/secret, más un par de usuarios (necesarios para el grant     resource owner password)
   
   Clientes: 
   
   <table>
    <tr><td>Grant</td><td>ClientId</td><td>Secret</td></tr>
        <tr><td>Authorization code</td><td>codeClient</td><td>secret</td></tr>
        <tr><td>Authorization code con soporte a refresh tokens</td><td>codeClientRefresh</td><td>secret</td></tr>
    <tr><td>Authorization code sin client secret y con extension PKCE</td><td>codeClientSPA</td><td>secret</td></tr>
    </table>

Usuarios:

   <table>
    <tr><td>Usuario</td><td>Password</td></tr>
    <tr><td>efrain</td><td>password</td></tr>
        <tr><td>tiberio</td><td>password</td></tr>        
    </table>

**99_Api**

Expone en http://localhost:5001 los siguientes recursos, todos para ser consumidos con GET:

Recursos NO securizados:

* /api/samurai  => Devuelve un listado de Samurais.

Recursos securizados:

Para acceder a ellos es necesario que el token disponga del scope api1

* /api/identity => Devuelve un listado de claims del usuario. 

* /api/identity/ReadData => Devuelve un string. Este recurso requiere que el token tenga un scope adicional: read

* /api/identity/ReadDataEnhanced => Devuelve un string. Este recurso requiere que el token tenga un scope adicional:  readEnhanced
