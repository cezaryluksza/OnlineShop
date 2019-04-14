//function toggleSortingOptions() {
//    var menuBox = document.getElementById('sortingOptions');
//    if (menuBox.style.display == "none") {
//        menuBox.style.display = "block";
//    }
//    else {
//        menuBox.style.display = "none";
//    }
//}

$('.sorting-button').on('click', function () {
    $('.sorting').toggleClass('off')
})

$('.categories-button').on('click', function () {
    $('.categories').toggleClass('cat-off')
})