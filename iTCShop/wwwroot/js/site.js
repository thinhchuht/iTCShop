let cartItems = 0;

const addCart = async (el) => {
    try {
        var productType = el.getAttribute('data-product-type');
        const response = await fetch('/Cart/AddCart', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(productType) 
        });

        console.log(response);
        const result = await response.json(); 
        console.log(result);
        if (result.code === 1) {
            alert(result.message);
            const cartHome = document.getElementById("cartHome");
            cartItems++; 
            cartHome.innerHTML = `(${cartItems})`;
        } else {
            alert(result.message);
        }
    } catch (error) {
        console.error('Error:', error);
        alert('Failed to add product to cart.');
    }
};

document.addEventListener('DOMContentLoaded', function () {
    const loadProductTypes = async () => {
        try {
            const response = await fetch('/ProductType/ProductTypePartial');
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const html = await response.text();
            document.querySelector('#partialProductType').innerHTML = html;
            attachAddCartEvent(); 
        } catch (error) {
            console.error('There has been a problem with your fetch operation:', error);
        }
    };

    const attachAddCartEvent = () => {
        const icons = document.querySelectorAll('.product .fa-plus');
        icons.forEach(icon => {
            icon.onclick = () => addCart(icon);
        });
    };

    loadProductTypes();

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
    