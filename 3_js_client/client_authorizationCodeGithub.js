'use strict';

var requestify = require('requestify'); 
const express = require('express');
const app = express();
var miaccessToken;

const oauth2 = require('simple-oauth2').create({
    client: {
        id: 'fdfc2be6b8b40646ea03',
        secret: '72f5ace862c834bdeffa896122cd3986e8359da0'
    },
    auth: {    
      tokenHost: 'https://github.com',
      tokenPath: '/login/oauth/access_token',
      authorizePath: '/login/oauth/authorize'
    }
  });

// Authorization uri definition
const authorizationUri = oauth2.authorizationCode.authorizeURL({
    redirect_uri: 'http://localhost:3000/callback',
    scope: '',
    state: 'abbc'
  });
   
//Express routes
   
app.get('/', (req, res) => {
    res.send('Hello<br><a href="/auth">Log in with Github</a><br><a href="/repos">Get the repos</a>');
    });

app.get('/repos', (req, res) => {

    requestify.request('https://api.github.com/user/repos', {
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
        })
        .catch((error) => {
        console.log('Access Token error', error.message);
        });

        return res.status(200).json(miaccessToken);
    });


//Start express
app.listen(3000, () => {
    console.log('Express server started on port 3000'); 
});