var images = [
    "assets/img/slider/1-2.jpg",
    "assets/img/slider/1-1.jpg"
];

var currentIndex = 0;

function changeImage() {
    var imageElement = document.getElementById("image");
    imageElement.src = images[currentIndex];

    currentIndex++;
    if (currentIndex >= images.length) {
        currentIndex = 0;
    }
}

setInterval(changeImage, 1000); // Thực hiện chuyển đổi hình ảnh sau mỗi 1 giây