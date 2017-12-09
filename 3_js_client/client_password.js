'use strict';

var requestify = require('requestify'); 
const express = require('express');
const app = express();

const oauth2 = require('simple-oauth2').create({
    client: {
      id: 'clientPwd',
      secret: 'pwdsecret',      
    },
    auth: {    
      tokenHost: 'http://localhost:5000',
      tokenPath: '/connect/token',
    },
  });

// Get the access token object for the client
const tokenConfig = {
    username: 'tiberio',
    password: 'password' 
  };
  
var miaccessToken;
oauth2.ownerPassword.getToken(tokenConfig)
    .then((result) => {
        console.log('Done! We have a token ');
        miaccessToken = oauth2.accessToken.create(result);
    })
    .catch((error) => {
    console.log('Access Token error', error.message);
    });

//Express routes
app.get('/samurai', (req, res) => {    
    requestify.get('http://localhost:5001/api/samurai')
        .then(function(response) {            
            res.send('The awesome samurais are: </BR>'+response.body);
        })
        .fail(function(response) {
            res.send('Failed with code = ' +response.getCode());
        });
  });

  app.get('/identity', (req, res) => {

    requestify.request('http://localhost:5001/api/identity', {
        method: 'GET',
        headers: {                 
            'Authorization': 'Bearer ' + miaccessToken.token.access_token
        }
    })
    .then(function(response) {        
        res.send('Identity information retrieved:</BR>'+response.body);    
    })
    .fail(function(response) {
        res.send('Failed with code = '+response.body+' ' +response.getCode());
    });
});
  
app.listen(3000, () => {
console.log('Express server started on port 3000'); 
});