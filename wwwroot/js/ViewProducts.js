// productScript.js
$(document).ready(function () {
    var hub = new signalR.HubConnectionBuilder().withUrl("/ProductHub").build();
    hub.start().then(function () {
        console.log("Connection Success");
    });

    hub.on("NewProductAdded", function (obj) {
        console.log(obj);
        $(".swiper-slide").append(
            `<div class="product-card position-relative product">
                <div class="image-holder">
                    <img src="~/Images/Product/${obj.ImageUrl}" alt="product image" class="img-fluid">
                </div>
                <div class="cart-concern position-absolute">
                    <div class="cart-button d-flex">
                        <a>Add to Cart <svg class="cart-outline"><use xlink:href="#cart-outline"></use></svg></a>
                    </div>
                </div>
                <div class="card-detail d-flex justify-content-between align-items-baseline pt-3">
                    <h3 class="card-title text-uppercase">
                        <span class="item-price text-primary">${obj.name}</span>
                    </h3>
                    <span class="item-price text-primary">${obj.Price}</span>
                </div>
            </div>`
        );
    });
});