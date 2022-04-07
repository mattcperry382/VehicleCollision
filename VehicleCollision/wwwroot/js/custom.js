$(window).load(function () {
    // init Isotope
    var $projects = $('.projects').isotope({
        itemSelector: '.project',
        layoutMode: 'fitRows'
    });
    $(".filter-btn").click(function () {
        var data_filter = $(this).attr("data-filter");
        $projects.isotope({
            filter: data_filter
        });
        $(".filter-btn").removeClass("active");
        $(".filter-btn").removeClass("shadow");
        $(this).addClass("active");
        $(this).addClass("shadow");
        return false;
    });
});

$('.alert').alert();


