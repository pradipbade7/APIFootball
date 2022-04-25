$(document).ready(function () {
    ellipse();
});

function ellipse() {
    let coverText = document.querySelector('.cover .caption-content .detail');
    let description = Array.from(document.querySelectorAll('.description p.detail'));
    if (coverText) {
        let coverInnerText = coverText.innerHTML;
        if (coverInnerText.length > 200) {
            coverInnerText = coverInnerText.substr(0, 200) + '...';
            coverText.innerHTML = coverInnerText;
        }
        if (coverInnerText.length > 200) {
            let coverInnerText = coverText.innerHTML;
            coverInnerText = coverInnerText.substr(0, 200) + '...';
            coverText.innerHTML = coverInnerText;
        }
    }
    if (description) {
        description.forEach(i => {
            let descriptionInnerText = i.innerHTML;
            if (descriptionInnerText.length > 140) {
                descriptionInnerText = descriptionInnerText.substr(0, 140) + '...';
                i.innerHTML = descriptionInnerText;
            }
        });
    }
}
$('.search-icon').click(function () {
    let search = document.querySelector('.search');
    search.classList.toggle("d-lg-block");
});
$('.table_slide .trigger').click(function () {
    let previousTitle, nextTitle, previousTable, nextTable;
    let activeTitle = document.querySelector('.slide-title.active');
    let activeTable = document.querySelector(`${activeTitle.getAttribute('data-target')}`);
    if (this.getAttribute('data-select') === 'next') {
        nextTitle = activeTitle.nextElementSibling;
        nextTable = activeTable.nextElementSibling;
        if (nextTitle !== null) {
            $(activeTitle).fadeOut("fast", () => {
                activeTitle.classList.remove("active");
                $(nextTitle).fadeIn("fast", () => {
                    nextTitle.classList.add("active");
                });
            });
            $(activeTable).fadeOut("fast", () => {
                activeTable.classList.remove("active");
                $(nextTable).fadeIn("fast", () => {
                    nextTable.classList.add("active");
                });
            });
        }
    } else if (this.getAttribute('data-select') === 'previous') {
        previousTitle = activeTitle.previousElementSibling;
        previousTable = activeTable.previousElementSibling;
        if (previousTitle !== null) {
            $(activeTitle).fadeOut("fast", () => {
                activeTitle.classList.remove("active");
                $(previousTitle).fadeIn("fast", () => {
                    previousTitle.classList.add("active");
                });
            });
            $(activeTable).fadeOut("fast", () => {
                activeTable.classList.remove("active");
                $(previousTable).fadeIn("fast", () => {
                    previousTable.classList.add("active");
                });
            });
        }
    }
});