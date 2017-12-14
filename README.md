## MyAuthSamples
### Resumen

Este repositorio contiene ejemplos de autorización y autenticación usando Indentity Server 4, junto con las especificaciones OAuth2 y OPenIdConnect (en breve llegará)


- 0_Overview

    Se corresponde con el el siguiente tutorial, donde encontraremos los primeros pasos par atrabajar con Identity Server:

    http://docs.identityserver.io/en/release/quickstarts/0_overview.html

  Nos indica como preparar un proyecto visual studio core para "hostear" a identify server.  

- 1_client_credentials

   Authorization server (con Identity Server 4) que permite los siguientes grants:
   Cliwnt credentials
   Resource owner password
   
   Para ello dispone de dos clientes, con sus correspondinetes pares clientId/secret, más un par de usuarios (necesartios para el grant     resource owner password)
   
   Clientes: 
   
   <table>
    <tr><td>Cliente</td><td>ClientId</td><td>Secret</td></tr>
        <tr><td>Client credentials</td><td>client</td><td>secret</td></tr>
        <tr><td>Resource owner password</td><td>ClientId</td><td>pwdsecret</td></tr>    
</table>

Usuarios:

   <table>
    <tr><td>Usuario</td><td>Password</td></tr>
    <tr><td>efrain</td><td>password</td></tr>
        <tr><td>tiberio</td><td>password</td></tr>        
</table>


- 2_core_client

  Cliente .NET core que usa los dos grants anteriores para cceder a un API securizado (ver más abajo el proyecto 99_Api)
  
- 3_js_client
  
  Conjunto de clientes Javascript, que usando la librería simple-oauth2 (https://github.com/lelylan/simple-oauth2) acceden al API protegida y a GitHub usando diferentes grants y variantes de los mismos:
  
  * client_credentials.js
  
    Accede al API protegido usando el grant Client Credentials (en la tabla anterior de clientes vemos los clientId y secretos a usar)
  
  * client_password.js
  
  Accede al API protegido usando el grant Resource Owner Password (en la tabla anterior están el cliente y secreto a usar). Además se utilza un de los usuarios disponibles
  
  * client_authorizationCodeApi.js
  * client_authorizationCodeApiRefresh.js
  * client_authorizationCodeGithub.js
  * client_authorizationCodeGithubRefresh.js
  

- 5_UI_server
- 99_Api
