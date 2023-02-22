$(function () {
    'use strict';

    $('.js-menu-toggle').click(function (e) {
        var $this = $(this);
        if (!$('body').hasClass('show-sidebar')) {
            $('body').addClass('show-sidebar');
            $this.addClass('active');
        } else {
            $('body').removeClass('show-sidebar');
            $this.removeClass('active');
        }
        e.preventDefault();
    });

    // click outisde offcanvas
    $(document).mouseup(function (e) {
        var container = $(".sidebar");
        var menu = $(".js-menu-toggle")
        if (!container.is(e.target) && container.has(e.target).length === 0 
                && !menu.is(e.target) && menu.has(e.target).length === 0) {
            if ($('body').hasClass('show-sidebar')) {
                closeMainSidebar()
            }
        }
    });

    //FLOATING MENU 
    $(document).on("scroll", () => {
        if(window.pageYOffset >= 80){
            $(".scroll-top-menu").css("display", "block")        
        } else {
            $(".scroll-top-menu").css("display", "none")
        }
    })
    $(".btn-scroll-top").click(() => { 
        window.scrollTo(0,0);
    })
    
});

function closeMainSidebar() {
    $('body').removeClass('show-sidebar');
    $('body').find('.js-menu-toggle').removeClass('active');
}