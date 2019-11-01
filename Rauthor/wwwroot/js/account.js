function switchForm() {
    var loginView = document.getElementById("login-form");
    var registerView = document.getElementById("register-form");
    var authButton = document.getElementById("acc-reg-button");
    var registerButton = document.getElementById("acc-auth-button");
    var regCheers = document.getElementById("reg-cheers");
    var authCheers = document.getElementById("auth-cheers");
    if (loginView.style.display != "none") {
        registerView.style.display = "block";
        regCheers.style.display = "block";
        loginView.style.display = "none";
        authCheers.style.display = "none";
        authButton.style.boxShadow = "0px 2px 0px 0px rgba(255, 82, 25, 1)";
        authButton.style.color = "rgb(255, 82, 25)";
        registerButton.style.boxShadow = "none";
        registerButton.style.color = "rgb(132, 132, 132)";
    }
    else {
        loginView.style.display = "block";
        registerView.style.display = "none";
        regCheers.style.display = "none";
        authCheers.style.display = "block";
        registerButton.style.boxShadow = "0px 2px 0px 0px rgba(255, 82, 25, 1)";
        registerButton.style.color = "rgb(255, 82, 25)";
        authButton.style.boxShadow = "none";
        authButton.style.color = "rgb(132, 132, 132)";
    }
}
function successReg() {
    var authContainer = document.getElementById("auth");
    var sucView = document.getElementById("successReg");
    var authCheersView = document.getElementById("auth-cheers");
    var regCheersView = document.getElementById("reg-cheers");
    var authFormContainerView = document.getElementById("auth-form-container");
    var regView = document.getElementById("register-form");
    authContainer.style.gridTemplateColumns = "0.33fr";
    authContainer.style.alignContent = "center";
    authContainer.style.justifyContent = "center";
    regView.style.display = "none";
    authCheersView.style.display = "none";
    regCheersView.style.display = "none";
    authFormContainerView.style.display = "none";
    sucView.style.display = "flex";
}
//# sourceMappingURL=account.js.map