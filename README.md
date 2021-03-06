## MyAuthSamples
### Resumen

Este repositorio contiene ejemplos de autorización y autenticación usando Indentity Server 4, junto con las especificaciones OAuth2 y OpenIdConnect (en breve llegará)

Se complementará con articulos en mi blog... que están en camino.

### Proyectos en la solución

**0_Overview**

Se corresponde con el el siguiente tutorial, donde encontraremos los primeros pasos para trabajar con Identity Server:
http://docs.identityserver.io/en/release/quickstarts/0_overview.html

  Nos indica como preparar un proyecto visual studio core para "hostear" a identify server.  

**1_client_credentials**

   Authorization server (con Identity Server 4) que permite los siguientes grants de OAuth 2:
   
   * Client credentials
   * Resource owner password
   
   Para ello dispone de dos clientes, con sus correspondientes pares clientId/secret, más un par de usuarios (necesarios para el grant     resource owner password)
   
   Clientes: 
   
   <table>
    <tr><td>Tipo de grant</td><td>ClientId</td><td>Secret</td></tr>
        <tr><td>Client credentials</td><td>client</td><td>secret</td></tr>
        <tr><td>Resource owner password</td><td>clientPwd</td><td>pwdsecret</td></tr>
    </table>

Adicionalmente, se han configurado custom claims, tanto para uno de los clientes como para uno de los usuarios, de modo que podamos ver como el token puede llevar claims que no son las standard.

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

Authorization server que dispone de UI (con Identity Server 4) para habilitar el grant Authorization Code. 
   
   Para ello dispone de dos clientes, con sus correspondientes pares clientId/secret, más un par de usuarios (necesarios para el grant     resource owner password)
   
   Clientes: 
   
   <table>
    <tr><td>Grant</td><td>ClientId</td><td>Secret</td></tr>
        <tr><td>Authorization code</td><td>codeClient</td><td>secret</td></tr>
        <tr><td>Authorization code con soporte a refresh tokens (caducidad al los 15 segundos)</td><td>codeClientRefresh</td><td>secret</td></tr>
    <tr><td>Authorization code sin client secret y con extension PKCE</td><td>codeClientSPA</td><td>secret</td></tr>
    </table>

Usuarios:

   <table>
    <tr><td>Usuario</td><td>Password</td></tr>
    <tr><td>efrain</td><td>password</td></tr>
        <tr><td>tiberio</td><td>password</td></tr>        
    </table>


**6_UI_server_oidc**

Authorization server que dispone de UI (con Identity Server 4) para habilitar el flow Implicit de OpenIdConnect. 
   
   Para ello dispone de un cliente que podemos ver a continuación:
   

   
   <table>
    <tr><td>Flow</td><td>ClientId</td><td>Secret</td></tr>
        <tr><td>Implicit</td><td>iodcImplicitClient</td><td>no es necesario</td></tr>
    </table>

Es importante destacar que se ha habilitado un scope custom, que añade claims custom a las standard. El cliente anterior está configurado para soportar los siguientes scopes:

* openid
* profile
* sergioScope, que añade soporte a las claims "sergioClaim1" y "sergioClaim2"

Usuarios:

   <table>
    <tr><td>Usuario</td><td>Password</td><td>Custom claims</td></tr>
    <tr><td>efrain</td><td>password</td><td>no tiene</td></tr>
    <tr><td>tiberio</td><td>password</td><td>"sergioClaim1" =>"sergioClaim1 value"  y  "sergioClaim2" => "sergioClaim2 value"  </td></tr>        
    </table>

Esta aplicación se aloja en el puerto 5500

**7_UI_core_client_oidc**

Cliente .net core que dispone de UI (es MVC) y que dispone de una página segura (/home/about). 
Esta configurado como cliente OpenIdConnect con el flujo implicit contra el Identity Server del puerto 5500 (apartado 6)

También dispone de un endpoint /Home/Logout que dispara el proceso de logout contra el Identity Server

Esta aplicación se aloja en el puerto 5502

**8_UI_core_client_oidc_hybrid**

Cliente .net core que dispone de UI (es MVC) y que dispone de una página segura (/home/about). 
Esta configurado como cliente OpenIdConnect con el flujo hybrid contra el Identity Server del puerto 5500 (apartado 6)

También dispone de una página (/home/api) que hace una llamada al API securizado (Api2, ver más abajo) usando el token que se obtubo del flujo Hybrid

Finalmente dispone de otra página (/home/UserInfo) que devuelve el listado de claims de identidad del usuario, ya que por defecto en este flujo no vienen...

Esta aplicación se aloja en el puerto 5503

**9_js_client_implicit**

Cliente js que sigue el fujo implicit. Su index nos guia en el proceso, que permite hacer login y logout, además de llamar al API
Esta aplicación se aloja en el puerto 5504

**9_js_client_implicit**

Cliente js que sigue el fujo hybrid. Su index nos guia en el proceso, que permite hacer login y logout, además de llamar al API
Esta aplicación se aloja en el puerto 5504

Se ha intentado implementar sin exito funcionalidad para revocar el access_token. Desafortunadamente la libreria oidc-client-js que usamos en el lado JS no esta funcionando en este caso de uso ... :-(

**99_Api**

Expone en http://localhost:5001 los siguientes recursos, todos para ser consumidos con GET:

Recursos NO securizados:

* /api/samurai  => Devuelve un listado de Samurais.

Recursos securizados:

Para acceder a ellos es necesario que el token disponga del scope api1

* /api/identity => Devuelve un listado de claims del usuario. 

* /api/identity/ReadData => Devuelve un string. Este recurso requiere que el token tenga un scope adicional: read

* /api/identity/ReadDataEnhanced => Devuelve un string. Este recurso requiere que el token tenga un scope adicional:  readEnhanced

Su "autoridad" es el identity server que da soporte a OAuth2 (puerto 5000)

**99_Api2**

Expone en http://localhost:5501 los siguientes recursos securizados, todos para ser consumidos con GET:

* /api/values  => Devuelve un listado de valores, que son strings

Para acceder a ellos es necesario que el token disponga del scope api2

Su "autoridad" es el identity server que da soporte a OpenIdConnect (puerto 5500)
