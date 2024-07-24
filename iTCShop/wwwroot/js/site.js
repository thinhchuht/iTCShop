document.addEventListener('DOMContentLoaded', function () {
    const loadProductTypes = async () => {
        try {
            const response = await fetch('/ProductType/ProductTypePartial');
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const html = await response.text();
            document.querySelector('#partialProductType').innerHTML = html;
        } catch (error) {
            console.error('There has been a problem with your fetch operation:', error);
        }
    };
    loadProductTypes();

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
    var cartItems = 0;
    var addCart = (btn) => {
        var cartHome = document.getElementById("cartHome");
        cartItems++;
        cartHome.innerHTML = `(${cartItems})`;
    }
});