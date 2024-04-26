$(document).ready(function () {
    var swiper = new Swiper('.swiper-auto', {
        slidesPerView: 5, // Show 5 slides per view
        spaceBetween: 10, // Space between slides
        loop: true, // Loop the slides
        pagination: {
            el: '.swiper-pagination', // Pagination container
            clickable: true, // Allow clicking on pagination bullets to navigate
        },
        autoplay: {
            delay: 1000, // Adjust the delay as needed (in milliseconds)
        },
    });
});