'use strict';

var requestify = require('requestify'); 
const express = require('express');
const app = express();
var miaccessToken;

const oauth2 = require('simple-oauth2').create({
    client: {
        id: 'codeClient',
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
    scope: 'api1',
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
        code,
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


//Start express
app.listen(3000, () => {
    console.log('Express server started on port 3000'); 
});