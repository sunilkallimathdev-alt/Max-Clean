jQuery(document).ready(function($) {
	
	'use strict';

    //===== Wow Animation Setting =====//
    var wow = new WOW({
        boxClass:     'wow',      // default
        animateClass: 'animated', // default
        offset:       0,          // default
        mobile:       true,       // default
        live:         true        // default
    }); 

    wow.init();

    $('.hdr-srch-btn,.hdr-srch-btn2').on('click',function(){
        $('.hdr-srch').addClass('active');
        return false;
    });
    $('.srch-cls').on('click',function(){
        $('.hdr-srch').removeClass('active');
        return false;
    });

    //===== Cart Drop Down =====//
    $('.crt-btn').on('click',function(){
        $(this).parent().toggleClass('active');
        return false;
    });

    $('.prd-rmv').on('click',function(){
        $(this).parent().parent().slideUp();
        return false;
    });

    //===== Header Side Panel =====//
    $('.sd-pnl-btn').on('click',function(){
        $('.hdr-sd-pnl').addClass('slidein');
        return false;
    });
    $('.hdr-sd-cls').on('click',function(){
        $('.hdr-sd-pnl').removeClass('slidein');
        return false;
    });

    //===== Header Login =====//
    $('.lgn').on('click',function(){
        $(this).parent().toggleClass('active');
        return false;
    });

    //===== Toggle =====//
    $('#tgl .tgl-cnt').hide();
    $('#tgl h5:first').addClass('active').next().slideDown(500).parent().addClass("activate");
    $('#tgl h5').on("click", function () {
        if ($(this).next().is(':hidden')) {
            $('#tgl h5').removeClass('active').next().slideUp(500).parent().removeClass("activate");
            $(this).toggleClass('active').next().slideDown(500).parent().toggleClass("activate");
            return false;
        }
    });

    //===== Responsive Header =====//
    $('.rspn-mnu-btn').on('click', function () {
        $('.rsnp-mnu').addClass('active');
        return false;
    });
    $('.rspn-mnu-cls').on('click', function () {
        $('.rsnp-mnu').removeClass('active');
        return false;
    });
    $('.rsnp-mnu li.menu-item-has-children > a').on('click', function () {
        $(this).parent().siblings().children('ul').slideUp();
        $(this).parent().siblings().removeClass('active');
        $(this).parent().children('ul').slideToggle();
        $(this).parent().toggleClass('active');
        return false;
    });

    //===== Responsive Contact =====//
    $('.rspn-cnt li').each(function () {
        $(".rspn-cnt li").on('click', function () {
            $(".rspn-cnt li").removeClass("active");
            $(this).addClass("active");
        });
    });

    //===== Sticky Header =====//
    var menu_height = $('header').height();
    $(window).scroll(function () {
        var scroll = $(window).scrollTop();
        if (scroll >= menu_height) {
            $('.fr-stky').addClass('sticky');
        } else {
            $('.fr-stky').removeClass('sticky');
        }
    });
    if ($('header').hasClass('fr-stky')) {
     //  $('.pg-lyut').css({'padding-top': menu_height});
    }

    //===== Scroll Bar =====//
    if ($.isFunction($.fn.perfectScrollbar)) {
        $('.rsnp-mnu > ul').perfectScrollbar();
    }

    //===== ToolTip =====//
    if ($.isFunction($.fn.tooltip)) {
        $('[data-toggle="tooltip"]').tooltip();
    }

    //===== Counter Up =====//
    if ($.isFunction($.fn.counterUp)) {
        $('.cntr').counterUp({
            delay: 10,
            time: 1000
        });
    }

    //===== Progress Bar =====//
    if ($.isFunction($.fn.loading)) {
        $(".prg-br").loading();
    }

    //===== Touch Spin =====//
    if ($.isFunction($.fn.TouchSpin)) {
        $('.prd-qty > input').TouchSpin({});
    }

    //===== LightBox =====//
    if ($.isFunction($.fn.poptrox)) {
        var foo = $('.lightbox');
        foo.poptrox({usePopupCaption: true, usePopupNav: true});
    }

    //===== Scroll Up Bar =====//
    if ($.isFunction($.fn.scrollupbar)) {
        $('header').scrollupbar();
    }

    //===== Mini Select =====//
    if ($.isFunction($.fn.minimalect)) {
        $('select').minimalect({});
    }

    //===== Count Down =====//
    if ($.isFunction($.fn.downCount)) {
        $('.countdown').downCount({
            date: '12/3/2019 12:00:00',
            offset: +5
        });
    }

    //===== Owl Carousel =====//
    if ($.isFunction($.fn.owlCarousel)) {
        //=== Testimonials Carousel ===//
        $('.tst-car').owlCarousel({
            autoplay: true,
            smartSpeed: 900,
            loop: true,
            items: 1,
            dots: true,
            slideSpeed: 2000,
            nav: false,
            margin: 0,
            animateIn:'fadeIn',
            animateOut:'fadeOut'
        });

        //=== Testimonials 2 Carousel ===//
        $('.tst-car2').owlCarousel({
            autoplay: true,
            smartSpeed: 700,
            loop: true,
            items: 2,
            dots: false,
            slideSpeed: 2000,
            nav: false,
            margin: 50,
            responsive: {
                0: {items: 1},
                480: {items: 1},
                768: {items: 1},
                1200: {items: 2}
            }
        });

        //=== Sponsor Carousel ===//
        $('.spncr-crsl').owlCarousel({
            autoplay: true,
            smartSpeed: 600,
            loop: true,
            items: 5,
            dots: false,
            slideSpeed: 2000,
            nav: false,
            margin: 30,
            responsive: {
                0: {items: 2},
                480: {items: 3},
                768: {items: 4},
                1200: {items: 5}
            }
        });

        //=== Work Profit Carousel ===//
        $('.wrk-prft-crsl').owlCarousel({
            autoplay: true,
            smartSpeed: 800,
            loop: true,
            items: 1,
            dots: true,
            slideSpeed: 2000,
            nav: false,
            margin: 0,
            animateIn:'fadeIn',
            animateOut:'fadeOut'
        });

        //=== Related Posts Carousel ===//
        $('.rltd-pst-crsl').owlCarousel({
            autoplay: true,
            smartSpeed: 800,
            loop: true,
            items: 3,
            dots: false,
            slideSpeed: 2000,
            nav: true,
            margin: 20,
            responsive: {
                0: {items: 1},
                480: {items: 2},
                768: {items: 2},
                1200: {items: 3}
            },
            navText: [
                "<i class='fa fa-angle-left'></i>",
                "<i class='fa fa-angle-right'></i>"
            ]
        });
    }
});

$(window).on('load',function(){
    'use strict';

    $("").append("<div class='purchase-btn'><a href='cleaning.html' title='' itemprop='url' target='_blank'>Book</a></div>");

    $('#loading').fadeOut('slow');
});
