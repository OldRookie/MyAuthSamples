function log() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
}

Oidc.Log.logger = console;
Oidc.Log.level=Oidc.Log.DEBUG;

document.getElementById("login").addEventListener("click", login, false);
document.getElementById("api").addEventListener("click", api, false);
document.getElementById("logout").addEventListener("click", logout, false);
document.getElementById("revoke").addEventListener("click", revoke, false);

var config = {
    authority: "http://localhost:5500",
    client_id: "jsHybridClient",
    redirect_uri: "http://localhost:5504/callback.html",
    response_type: "id_token token code",
    scope:"openid profile api2 offline_access",
    post_logout_redirect_uri: "http://localhost:5504/index.html"
};
var mgr = new Oidc.UserManager(config);

mgr.getUser().then(function (user) {
    if (user) {
        //log("User logged in", user.profile);        
        log("User:", user);        
    }
    else {
        log("User not logged in");
    }
});

function login() {
    mgr.signinRedirect();
}

function revoke() {
    mgr.revokeAccessToken().then(function(a) {
        console.debug("Access token revocation finished. ");
    });    
}

function api() {
    mgr.getUser().then(function (user) {
        var url = "http://localhost:5501/api/values";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

function logout() {
    mgr.signoutRedirect();
}