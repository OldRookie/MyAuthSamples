'use strict';

var requestify = require('requestify'); 
const express = require('express');
const app = express();
var miaccessToken;

const oauth2 = require('simple-oauth2').create({
    client: {
      id: 'codeClientRefresh',
      secret: 'secret',      
    },
    auth: {    
      tokenHost: 'http://localhost:5000',
      tokenPath: '/connect/token',
      authorizeHost: 'http://localhost:5000',
      authorizePath: '/connect/authorize',
      revokePath: '/connect/revocation',
    },
  });

// Authorization uri definition
const authorizationUri = oauth2.authorizationCode.authorizeURL({
    redirect_uri: 'http://localhost:3000/callback',
    scope: 'api1 offline_access',
    state: 'abbc',
  });
   
//Express routes
   
app.get('/', (req, res) => {
    res.send('Hello<br><a href="/auth">Log in id server</a><br><a href="/data">Get the protected data</a>');
});

app.get('/data', (req, res) => {

    requestify.request('http://localhost:5001/api/identity', {
        method: 'GET',
        headers: {                 
            'Authorization': 'Bearer ' + miaccessToken.token.access_token
        }
        })
        .then(function(response) {        
            res.send(response.body);
        })
        .fail(function(response) {
            res.send('Failed with code = '+response.body+' ' +response.getCode());
        })
    });

 // Initial page redirecting to Github
 app.get('/auth', (req, res) => {
    console.log(authorizationUri);
    res.redirect(authorizationUri);
  });

  // Callback service parsing the authorization token and asking for the access token
app.get('/callback', (req, res) => {
   
    console.log('llamaron a callback.Code:');
    
    const code = req.query.code;
    console.log(code);
    var options = {      
      code: code,
      redirect_uri: 'http://localhost:3000/callback'
    }

    // Get the access token object for the client  

    oauth2.authorizationCode.getToken(options)
        .then((result) => {
            console.log('Done! We have a token ');
            miaccessToken = oauth2.accessToken.create(result);
            return res.status(200).json(miaccessToken);
        })
        .catch((error) => {
        console.log('Access Token error', error.message);
        });
    });

app.get('/refresh', (req, res) => {
    
    if (miaccessToken.expired()) {
        console.log('El token está expirado!');      
    }        
    else{
        console.log('El token NO está expirado!');     
            console.log('Refrescando token...');
            miaccessToken.refresh()
            .then((result) => {
                miaccessToken = result;
                res.send('refrescado');
            })
            .catch ( (error) => {
                console.log('Refresh  Token error', error.message);
                res.send('error al refrescar');
            });
    }
});
    
app.get('/revoke', (req, res) => {

    if (miaccessToken.expired()) {
        console.log('El token está expirado!');
    } else {
        console.log('El token NO está expirado!');

        miaccessToken.revoke('access_token', (error) => {
            // Session ended. But the refresh_token is still valid.

            // Revoke the refresh_token
            miaccessToken.revoke('refresh_token', (error) => {
                console.log('token revoked.');
                res.send('revocado');
            });
        });
    }
});

//Start express
app.listen(3000, () => {
    console.log('Express server started on port 3000'); 
});