﻿let cartItems = 0;

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

function setProductId(id, check,name='',memory='',color='') {
    console.log(id);
    console.log(check);
    console.log(document.getElementById('imei'));
    console.log(document.getElementById('productId'));
    if (check === "prod") document.getElementById('imei').value = id;;
    if (check === "type") {
        document.getElementById('productId').value = id;
        document.getElementById('productName').value = name;
        document.getElementById('productMemory').value = memory;
        document.getElementById('productColor').value = color;
    }
    if (check === "order") document.getElementById('orderId').value = id;
    console.log(document.getElementById('imei'));
    
}

window.onpageshow = function (event) {
    if (event.persisted) {
        window.location.reload();
    }
};

