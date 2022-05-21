var passwordInput = document.querySelector("#Password");
var CB = document.querySelector("#Hidden");

CB.addEventListener('change', (event) => {
    if (event.currentTarget.checked) {
       passwordInput.type = "text";
    }
    else {
        passwordInput.type = "password";
    }
})

