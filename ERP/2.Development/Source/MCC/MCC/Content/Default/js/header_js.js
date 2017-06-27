var curent = 0;
new WOW().init();
$(document).ready(function(){
	
	
	
	if($('.navi-type-3').length > 0){
		$('.navi-type-3 .menu-des li > a').hover(function(){
			ActiveNavigation3Hover($(this));
		}, function(){
			ActiveNavigation3();
		});
		
	}
	
	// menu mobile
		$( "#nav-toggle" ).click(function(event){
			event.stopPropagation();
			var show_item;
			var hide_item;
			if($('.menu-mb').hasClass('active-mb')){
				clearTimeout(hide_item);
				hide_item = setTimeout(function(){
					$('.menu-mb li').fadeOut();
				},500);	
			}
			$(this).toggleClass( "active-menu" );
			$('.menu-mb').toggleClass('active-mb');
			clearTimeout(show_item);
			if($('.menu-mb').hasClass('active-mb')){
				$('.menu-mb li').each(function(i,val){
					show_item = setTimeout(function(){
						$('.menu-mb li').eq(i).fadeIn(400);
					}, 80*i);
				});
			}
		});// end click
	// close menu mobile on click body
		$('body').click(function(){
			if($( "#nav-toggle" ).hasClass('active-menu')){
				$( "#nav-toggle" ).click();
			}
		});// end body click

	
	// slide introduce
	if($('#header-3-slid').length>0){
		$('#header-3-slid .responsiveGallery-wrapper').responsiveGallery({
			animatDuration: 600,
			flagrotate: 1,
			$btn_prev: $('#header-3-slid .responsiveGallery-btn_prev'),
			$btn_next: $('#header-3-slid .responsiveGallery-btn_next')
		});
	}
    
	
	
	// active menu DES&M0B option 1
	MenuOption($('.navi-type-1 .menu-des li'), $('.navi-type-1 .menu-mb li'));
	
	// active menu DES&M0B option 2 
	MenuOption($('.navi-type-2 .menu-des li'), $('.navi-type-2 .menu-mb li'));
	
	// active menu DES&M0B option 3
	MenuOption($('.navi-type-3 .menu-des li'), $('.navi-type-3 .menu-mb li'));
	
	// active menu DES&M0B option 4
	MenuOption($('.navi-type-4 .menu-des li'), $('.navi-type-4 .menu-mb li'));
	
	// active menu DES&M0B option 5
	MenuOption($('.navi-type-5 .menu-des li'), $('.navi-type-5 .menu-mb li'));
	
	// video 4
	Video4();
	
	// loading page 4
	setTimeout(function(){
		$('#container-loading').css({'opacity':'1'});
	},100);
	setTimeout(function(){
		$('.bg-loadding').fadeOut(300);
	},800);
	
	//active navigation type 3
	ActiveNavigation3();
	MobileClickNavigation();
	
	// call slider header 8
	if($('.owl-carousel-h8').length>0){
		$('.owl-carousel-h8').owlCarousel({
			 items : 1,
			itemsCustom : false,
			itemsDesktop : [1199,1],
			itemsDesktopSmall : [980,1],
			itemsTablet: [768,1],
			itemsTabletSmall: false,
			itemsMobile : [479,1],
			pagination:true,
			autoPlay:false,
			singleItem:true,
			 paginationSpeed : 400
		});
	}
	
	// header 4 video
	if($('.play-vides').length>0){
		$('.play-vides').click(function(){
			if($(this).hasClass('play--vide')){
				$(this).removeClass('play--vide');
				$('.bg-h-4-des').animate({
					'opacity': 1
				},300);
			}else{
				$(this).addClass('play--vide');
				$('.bg-h-4-des').animate({
					'opacity': 0
				},400);
			}
		});
	}
	
	// call slider header 9
	if($('.owl-carousel-h9').length>0){
		$('.owl-carousel-h9').owlCarousel({
			items : 1,
			itemsCustom : false,
			itemsDesktop : [1199,1],
			itemsDesktopSmall : [980,1],
			itemsTablet: [768,1],
			itemsTabletSmall: false,
			itemsMobile : [479,1],
			pagination:true,
			autoPlay:false,
			singleItem:true
		});
	}
	
	// call slider header 10
	if($('#dg-container').length>0){
		$('#dg-container').gallery({autoplay	:	true});
		var w_parent = $('.slider-h-10-right').width();
	}
	
	
	//fontEndPixSliderMobile();
	//AutoRunfontEndPixSliderMobile();
	
	
	ScrollMenu();
	$(window).on('load scroll', function(e){
		if($(window).scrollTop() > 50){
			$('.tagert-menu').addClass('blk-menu-fix');
			if($('.tagert-menu').hasClass('template-12-flag')){
				$('.tagert-menu').addClass('clor-menu-fix-12');
			}
		}else{
			$('.tagert-menu').removeClass('blk-menu-fix');
			$('.tagert-menu').addClass('clor-menu-fix-12');
		}
	});
	
});// end ready

	function ScrollMenu(){
	var list_ele = $('.commont-menu .js-dynamic-menu-des a');
	list_ele.click(function(){
		var target_ele = $(this).attr('href');
		var height_ele = $(target_ele).offset().top;
			$('html, body').stop().animate({
			scrollTop: $(target_ele).offset().top
			}, (height_ele <= 300) ? 900 : (height_ele/500) * 250);
		
	});
}// end function 

// check exist element
jQuery.fn.exists = function(){return this.length>0;}

// slider-h10-pix-font-end
function fontEndPixSliderMobile(){
	var slider = $('.pix-fontend-nc');
	var item = $('.pix-fontend-nc .item-');
	var item_left = $('.pix-fontend-nc .item-.left-sli');
	var item_right = $('.pix-fontend-nc .item-.right-sli');
	
	// event onload and resize
	$(window).on('resize', function(){
		slider = $('.pix-fontend-nc');
		// item_left 
		item_left.css({
			'width': (slider.width()/2 - 60) + 'px'
		});
		// item_right 
		item_right.css({
			'width': (slider.width()/2 - 60) + 'px'
		});
	});
	
	// window onload 
		// item_left 
	item_left.css({
		'width': (slider.width()/2 - 60) + 'px'
	});
		// item_right 
	item_right.css({
		'width': (slider.width()/2 - 60) + 'px'
	});
	
} //end function

function AutoRunfontEndPixSliderMobile(){
	var auto_run = null;
	var items = $('.pix-fontend-nc .item-');
	var item = $('.pix-fontend-nc .item-');
	
	auto_run =  setInterval(function(){
		var item_right = $('.pix-fontend-nc .item-.right-sli');
		var item_left = $('.pix-fontend-nc .item-.left-sli');
		var item_center = $('.item-.center-sli');
		
		if(item_right.next().length > 0){
			
		}else{
			item_left.removeClass('left-sli').fadeOut(400);
			item.eq(0).addClass('right-sli').fadeIn(100);
			item_center.removeClass('center-sli').addClass('left-sli');
			item_right.removeClass('.right-sli').addClass('center-sli');
			
		}
		// item_left.removeClass('left-sli').fadeOut(200);
		// item_center.removeClass('center-sli').addClass('left-sli');
		// item_right.removeClass('.right-sli').addClass('center-sli');
		// item_right.next().addClass('right-sli').fadeIn(100);
		fontEndPixSliderMobile();
	},2000);
	
	//clearInterval(auto_run);
}

// Active navigation 3
function ActiveNavigation3(){
	var ele_span = $('.navi-type-3 .menu-des li:last-child span');
	var ele_active = $('.menu-des li .menu__item--current');
	var pos_left = 0;
	var status = '';
	// check elemet exist
	if ($(ele_active).exists()) {
		pos_left = ele_active.position().left + (ele_active.width()/2) + 10;
	}

	if(pos_left > curent){
		status = 'right';
		curent = pos_left;
	}else{
		status = 'left';
		curent = pos_left;
	}
	if(status == 'left'){
		ele_span.eq(0).stop().animate({
		'left': (pos_left - 10) + 'px'
		},700);
		ele_span.eq(1).stop().animate({
			'left': pos_left + 'px'
		},750);
		ele_span.eq(2).stop().animate({
			'left': (pos_left + 10) + 'px'
		},900);
	}else{
		ele_span.eq(0).stop().animate({
		'left': (pos_left - 10) + 'px'
		},900);
		ele_span.eq(1).stop().animate({
			'left': pos_left + 'px'
		},750);
		ele_span.eq(2).stop().animate({
			'left': (pos_left + 10) + 'px'
		},700);
	}
	
	

}// end function

// Active navigation 3
function ActiveNavigation3Hover(parathis){
	var ele_span = $('.navi-type-3 .menu-des li:last-child span');
	var ele_active = parathis;
	var pos_left = 0;
	var status = '';
	// check elemet exist
	if ($(ele_active).exists()) {
		pos_left = ele_active.position().left + (ele_active.width()/2) + 10;
	}

	if(pos_left > curent){
		status = 'right';
		curent = pos_left;
	}else{
		status = 'left';
		curent = pos_left;
	}
	if(status == 'left'){
		ele_span.eq(0).stop().animate({
		'left': (pos_left - 10) + 'px'
		},700);
		ele_span.eq(1).stop().animate({
			'left': pos_left + 'px'
		},750);
		ele_span.eq(2).stop().animate({
			'left': (pos_left + 10) + 'px'
		},900);
	}else{
		ele_span.eq(0).stop().animate({
		'left': (pos_left - 10) + 'px'
		},900);
		ele_span.eq(1).stop().animate({
			'left': pos_left + 'px'
		},750);
		ele_span.eq(2).stop().animate({
			'left': (pos_left + 10) + 'px'
		},700);
	}
	
	

}// end function

// Click menu mobile Navigation 3
function MobileClickNavigation(){
	$('.navi-type-3 .menu-mb li').click(function(){
		$('.navi-type-3 .menu-des li').eq($(this).index()).removeClass('menu__item--current');
		$('.navi-type-3 .menu-des li').find('a').removeClass('menu__item--current');
		$('.navi-type-3 .menu-des li').eq($(this).index()).find('a').addClass('menu__item--current');
		
		$('.navi-type-3 .menu-des li:last-child').css({'display':'none'});
		$('.navi-type-3 .menu-des li a').click(function(){
			$('.navi-type-3 .menu-des li:last-child').css({'display':'block'});
			$('.navi-type-3 .menu-des li:last-child span').css({'bottom':'20px'});
		});
	});
}


// header 4 control video
function Video4(){
		$(window).on("resize load", function(){
					var h_video = $('#bgvid').height() + 82;
					var h_video_o = $('#bgvid').outerHeight();
					var parent = $('.h-option-4');
					if($(window).width() >= 768){
						parent.css({
							'height': h_video_o +'px'
						});
					}else{
						parent.css({
							'height': 'auto'
						});
					}
					
					
				});
				var button_play = document.getElementById("bgvid");
				$('.play-vides').click(function(){
				  if (button_play.paused) {
					button_play.play();
				  } else {
					button_play.pause();
				  }
				});
}// end function video 4




// active menu destop and mobile
function MenuOption(par_des, par_mb){
			var item_menu = par_des;
			var item_menu_mb = par_mb;
			
			
			item_menu.click(function(){
				item_menu.removeClass('menu__item--current');
				$(this).addClass('menu__item--current');
				item_menu.find('a').removeClass('menu__item--current effect-zoom');
				$(this).find('a').addClass('menu__item--current');
				
				$('.navi-type-2 .menu-des li').eq($(this).index()).find('a').addClass('effect-zoom');
				
				item_menu_mb.find('a').removeClass('menu__item--current');
				item_menu_mb.removeClass('menu__item--current');
				item_menu_mb.eq($(this).index()).find('a').addClass('menu__item--current');
				
				ActiveNavigation3();
				
				
			});// end click destop
			
			item_menu_mb.click(function(){
				item_menu_mb.find('a').removeClass('menu__item--current effect-zoom');
				$(this).find('a').addClass('menu__item--current');
				item_menu_mb.removeClass('menu__item--current');
				
				$('.navi-type-2 .menu-mb li').eq($(this).index()).find('a').addClass('effect-zoom');
				item_menu.find('a').removeClass('menu__item--current');
				item_menu.removeClass('menu__item--current');
				item_menu.eq($(this).index()).find('a').addClass('menu__item--current');
				item_menu.eq($(this).index()).addClass('menu__item--current');
				
				event.stopPropagation();
			});// end click mobile
		}// end function
		
// active memu destop
(function() {
			[].slice.call(document.querySelectorAll('.menu')).forEach(function(menu) {
				var menuItems = menu.querySelectorAll('.menu__link'),
					setCurrent = function(ev) {
						ev.preventDefault();

						var item = ev.target.parentNode; // li

						// return if already current
						if (classie.has(item, 'menu__item--current')) {
							return false;
						}
						// remove current
						classie.remove(menu.querySelector('.menu__item--current'), 'menu__item--current');
						// set current
						classie.add(item, 'menu__item--current');
					};

				[].slice.call(menuItems).forEach(function(el) {
					el.addEventListener('click', setCurrent);
				});
			});

			[].slice.call(document.querySelectorAll('.link-copy')).forEach(function(link) {
				link.setAttribute('data-clipboard-text', location.protocol + '//' + location.host + location.pathname + '#' + link.parentNode.id);
				new Clipboard(link);
				link.addEventListener('click', function() {
					classie.add(link, 'link-copy--animate');
					setTimeout(function() {
						classie.remove(link, 'link-copy--animate');
					}, 300);
				});
			});
		})(window);

if($('#container-loading').length > 0){
		
(function() {
		var container = document.getElementById('container-loading');
		var drop = document.getElementById('drop');
		var drop2 = document.getElementById('drop2');
		var outline = document.getElementById('outline');

		TweenMax.set(['svg'], {
			position: 'absolute',
			top: '50%',
			left: '50%',
			xPercent: -50,
			yPercent: -50
		})

		TweenMax.set([container], {
			position: 'absolute',
			top: '50%',
			left: '50%',
			xPercent: -50,
			yPercent: -50
		})

		TweenMax.set(drop, {
			transformOrigin: '50% 50%'
		})

		var tl = new TimelineMax({
			repeat: -1,
			paused: false,
			repeatDelay: 0,
			immediateRender: false
		});

		tl.timeScale(3);

		tl.to(drop, 4, {
			attr: {
				cx: 250,
				rx: '+=10',
				ry: '+=10'
			},
			ease: Back.easeInOut.config(3)
		})
		.to(drop2, 4, {
			attr: {
				cx: 250
			},
			ease: Power1.easeInOut
		}, '-=4')
		.to(drop, 4, {
			attr: {
				cx: 125,
				rx: '-=10',
				ry: '-=10'
			},
			ease: Back.easeInOut.config(3)
		})
		.to(drop2, 4, {
			attr: {
				cx: 125,
				rx: '-=10',
				ry: '-=10'
			},
			ease: Power1.easeInOut
		}, '-=4')
	})();
}