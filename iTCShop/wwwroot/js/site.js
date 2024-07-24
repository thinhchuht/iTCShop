document.addEventListener('DOMContentLoaded', function () {
    var login = document.getElementById("log");
    login.onclick = () => {
        console.log(login.innerText);
        if (login.innerText.trim() == "Login") {
            window.location.href = "/Login/Login";
        } else {
            fetch('/Login/LogOut', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                }
            }).then(response => {
                if (response.ok) {
                    console.log(response);
                    window.location.href = '/Login/Login';
                } else {
                    alert('Logout failed.');
                }
            }).catch(error => {
                console.error('Error:', error);
                alert('Logout failed.');
            });
        }
    }
});