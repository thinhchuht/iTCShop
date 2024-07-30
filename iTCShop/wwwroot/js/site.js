let cartItems = 0;

const addCart = async (el) => {
    try {
        var productTypeId = el.getAttribute('product-type-id');
        const response = await fetch('/Cart/AddCart', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(productTypeId)
        });
        if (response.redirected) {
            window.location.href = response.url;
        } else {
            const result = await response.json();
            if (result.code === 1) {
                alert(result.message);
                const cartHome = document.getElementById("cartHome");
                cartItems++;
                cartHome.innerHTML = `(${cartItems})`;
            } else {
                alert(result.message);
            }
        }
    } catch (error) {
        console.error('Error:', error);
        alert('Failed to add product to cart.');
    }
};

function setProductId(id,check) {
    console.log(id);
    console.log(check);
    console.log(document.getElementById('imei'));
    console.log(document.getElementById('productId'));
    if (check === "prod") document.getElementById('imei').value = id;;
    if (check === "type") document.getElementById('productId').value = id;
    if (check === "order") document.getElementById('orderId').value = id;
    console.log(document.getElementById('imei'));
    
}

document.addEventListener('DOMContentLoaded', function () {
    const attachAddCartEvent = () => {
        const icons = document.querySelectorAll('.product .fa-plus');
        icons.forEach(icon => {
            icon.onclick = () => addCart(icon);
        });
    };

    attachAddCartEvent();

    const login = document.getElementById("log");
    login.onclick = () => {
        console.log(login.innerText);
        if (login.innerText.trim() === "Login") {
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
