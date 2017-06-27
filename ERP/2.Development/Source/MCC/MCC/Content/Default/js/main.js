(function($){
	$(document).ready(function(){	
	    t.init()
	}),
	$(window).resize(function(){
		t.setHeightSlideshow(),
		t.textCustomHeader()
	}),
	$(window).load(function(){	
		$(this).scroll(function(){
			$(this).scrollTop() > 200 ? $('#back-top').fadeIn() : $('#back-top').fadeOut();
			$(this).scrollTop() > 400 ? $('.nav-fixed-left').addClass('visible') : $('.nav-fixed-left').removeClass('visible');
		})
		
	}),	
	$(document).on('click touchstart',function(i){
		var o = $('#offset-menu-s1'),
			l = $('#LeftPush'),
			p = $('#offset-menu-s2'),
			r = $('#CartPush'),
			s = $('.btn-dropdown'),
			g = $('.user-link')
			h = $('.top-linklists-mobile'),
			k = $('.top-header .custom-text');
		o.is(i.target) || 0 !== o.has(i.target).length || l.is(i.target) || 0 !== l.has(i.target).length || p.is(i.target) || 0 !== p.has(i.target).length || r.is(i.target) || 0 !== r.has(i.target).length || s.is(i.target) || 0 !== s.has(i.target).length || g.is(i.target) || 0 !== g.has(i.target).length || h.is(i.target) || 0 !== h.has(i.target).length || k.is(i.target) || 0 !== k.has(i.target).length || (t.closeLeftPush(),t.closeCartRight(),t.closeDropDown(g),t.closeDropDown(h),t.closeDropDown(k))
	});
	var t = {
		init: function() {
			this.initSlideshow(),
			this.initMobileMenu(),
			this.initButtonDropDown(),
			this.initLeftPush(),			
			this.setHeightSlideshow(),
			this.textCustomHeader(),
			this.ImageLazyLoad(),
			this.initQuickView(),
            this.initDropDownCart(),
            this.initQuickviewAddToCart(),
            this.initAddToCart(),
            this.initProductAddToCart(),
            this.initModal(),
            this.initOwlThumbsImage(),            
            this.initRelatedProduct(),
			this.initScrollTop(),
			this.initNavCollection(),
				this.initPrintCollection()
		},
		initSlideshow: function(){
			$('.slideshow').owlCarousel({
				singleItem: true,
				autoPlay: 8000,
				pagination: true,
				navigation: false,
				transitionStyle : "fade"				
			});            
		},
		initMobileMenu: function(){
			$('.mobile-menu').each(function(){
				$(this).accordion({
			      accordion: false,
			      speed: 300,
			      closedSign: '<i class="fa fa-angle-down"></i>',
			      openedSign: '<i class="fa fa-angle-up"></i>'
			    })
		    })
		},
		
		initButtonDropDown: function(){
			$('.btn-dropdown').each(function(){
		    	$(this).on('click', function(){
		    		var target_drop = $(this).attr('data-target');
		    		if ($(target_drop).is(':hidden')){
		    			$(this).addClass('active');
		    			$(target_drop).stop(true,true).slideDown(300);
		    		} else {
		    			$(target_drop).stop(true,true).slideUp(300);
		    			$(this).removeClass('active');
		    		}
		    	})
	    	})
		},
		closeDropDown: function(e){
			$(e).stop(true,true).slideUp(300);
		},
		initLeftPush: function(){
			var menuLeft = $('#offset-menu-s1'),
		      	showLeftPush = $( '#LeftPush' ),
		      	body = $('body');
		    showLeftPush.on('click',function(){
		        $(this).toggleClass('active');
		        body.toggleClass('offset-push-right');
		        menuLeft.toggleClass('offset-menu-left-open');
		    })
		},
		closeLeftPush: function(){
			$( '#LeftPush' ).removeClass('active');
			$('#offset-menu-s1').removeClass('offset-menu-left-open');
			$('body').removeClass('offset-push-right');
		},
		
		closeCartRight: function(){
			$('#CartPush').removeClass('active');
			$('body').removeClass('offset-push-left');
			$('#offset-menu-s2').children('.modal-container').remove();
			$('#offset-menu-s2').removeClass('offset-menu-right-open');
		},
		setHeightSlideshow: function(){
			var ww = $(window).width();
			if (ww >= 992){
				var height = $('.main-category').height();
				$('.slideshow').css('height',height);
			} else {
				$('.slideshow').css('height', 'auto');
			}
		},
		textCustomHeader: function(){
			var ww = $(window).width();
			if (ww >= 992){
				$('.top-header .custom-text').removeAttr('style');
			} else {
				$('.top-header .custom-text').css('display','none');
			}
		},
		ImageLazyLoad: function(){
			if (enable_lazy_loading == true){ 
				$('img.lazy').lazyload({
					effect: 'fadeIn',
					threshold: 200
				})
			} else {
				return false;
			}
		},
		initDropDownCart: function() {              
            t.checkItemsInDropdownCart();
            $("#mini-cart-modal .remove").click(function(n) {
                n.preventDefault();
                var r = $(this).parents(".item").attr("id");
                r = r.match(/\d+/g);
                Bizweb.removeItem(r, function(e) {
                    t.doUpdateDropdownCart(e)
                })
            });
        },
		initPrintCollection: function(){            
			switch (change_image){
				case true:
					var r = '<div class="col-xs-12 col-sm-6 col-md-6 col-lg-4"><div class="product-grid product-ajax-cart" id="product-{ID}"><div class="level-pro-sale" style="display:{DISPLAY_SALE}"><span>sale</span></div><div class="product-img"><a href="/products/{ALIAS}" title="{NAME}"><img src="{IMAGE}" alt="{NAME}" class="img-fix primary-img"><img src="{IMAGE2}" alt="{NAME}" class="img-fix secondary-img"></a></div><h3><a href="/products/{ALIAS}" title="{NAME}">{NAME}</a></h3><div class="price"><del>{COMPARE_PRICE}</del><span>{PRICE}</span></div><form class="actions clearfix" method="post" action="/cart/add" id="product-actions-{ID}">';                    
					break;
				case false:
					var r = '<div class="col-xs-12 col-sm-6 col-md-6 col-lg-4"><div class="product-grid hover-zoom product-ajax-cart" id="product-{ID}"><div class="level-pro-sale" style="display:{DISPLAY_SALE}"><span>sale</span></div><div class="product-img"><a href="/products/{ALIAS}" title="{NAME}"><img src="{IMAGE}" alt="{NAME}" class="img-fix primary-img"></a></div><h3><a href="/products/{ALIAS}" title="{NAME}">{NAME}</a></h3><div class="price"><del>{COMPARE_PRICE}</del><span>{PRICE}</span></div><form class="actions clearfix" method="post" action="/cart/add" id="product-actions-{ID}">'; 
			}           
			$('.tab-index li a').on('click', function(){
				var n = $(this).attr('data-collection'),
					h = $(this).attr('href'),
				    product_display = $(this).parents('.tab-index').attr('data-display');
					if ( product_display === ''){						
						product_display = 5;
					};					    
					if ($(h).find('.product-grid').length === 0){	
						$.ajax({
							dataType: "json",
							url:"/collections/" + n + "/products.json",
							data: {},
							beforeSend: function() {
								$(h).find(".wrap_loading").show()
							},
							success: function(n){
								$(h).find(".wrap_loading").remove();
								var length_data_return = n['products'].length;						    
								var count_product = 0;
								while(count_product < length_data_return){
									if(count_product > product_display)
										break;


									var q = n.products[count_product],
										idz = q.id,
										a = q.name,                                 
										g = Bizweb.resizeImage(q.image.src, "large"),
										u = q.alias,
										p = q.variants[0].price,
										s = r;
									if (q.variants.length > 2){                                 
											s = s + '<a href="/products/{ALIAS}" class="btn-transparent pull-left">Chọn sản phẩm</a><button class="btn-transparent pull-right quickview-btn" id="{ALIAS}">Xem nhanh</button></form></div></div>';       

									} else {
										switch (enable_ajax_cart){
											case true:
											s = s + '<input type="hidden" name="variantId" value="' + q.variants[0].id + '"><button class="btn-transparent pull-left add-ajax-cart">Cho vào giỏ</button><button class="btn-transparent pull-right quickview-btn" id="{ALIAS}">Xem nhanh</button></form></div></div>';
											break;  
											case false:
											s = s + '<input type="hidden" name="variantId" value="' + q.variants[0].id + '"><button class="btn-transparent pull-left">Cho vào giỏ</button><button class="btn-transparent pull-right quickview-btn" id="{ALIAS}">Xem nhanh</button></form></div></div>';                                     
										} 
									};
									if (q.images.length > 2){
										var g2 = Bizweb.resizeImage(q.images[1].src, "large");
									} else {
										var g2 = Bizweb.resizeImage(q.image.src, "large");
									};

									s = s.replace(/\{ID\}/g, idz);
									s = s.replace(/\{ALIAS\}/g, u);
									s = s.replace(/\{NAME\}/g, a);
									s = s.replace(/\{IMAGE\}/g, g);
									s = s.replace(/\{IMAGE2\}/g, g2);
									s = s.replace(/\{PRICE\}/g, Bizweb.formatMoney(p, window.money_format));
									if (q.variants[0].compare_at_price > q.variants[0].price){
										s = s.replace(/\{COMPARE_PRICE\}/g, Bizweb.formatMoney(q.variants[0].compare_at_price, window.money_format));
										s = s.replace(/\{DISPLAY_SALE\}/g,'block')
									} else {
										s = s.replace(/\{COMPARE_PRICE\}/g, '');
										s = s.replace(/\{DISPLAY_SALE\}/g,'none')
									}                               
									$(h).append(s);	
									count_product++;   
								}
								t.initQuickView();
								t.initAjaxAddToCart();
							}
						});
					  /* Bizweb.getCollection(n,function(n){                            
							
						    
						    if (enable_ajax_cart) {
							  t.initAddToCart();
						    }
						});     */                                                                              
					}
					
				              
			});			
			
		},
        initQuickView: function() {
            $(".quickview-btn").click(function() {                
                var n = $(this).attr("id");
                Bizweb.getProduct(n, function(n) {					
                    var r = $("#quickview-template").html();
                    $(".quick-view").html(r);
                    var i = $(".quick-view");
                    var s = n.content.replace(/(<([^>]+)>)/ig, "");
                    s = s.split(" ").splice(0, 35).join(" ") + "...";					
					s = s.replace(/&nbsp;/ig,' ');
					
                    i.find(".product-title a").text(n.name);
                    i.find(".product-title a").attr("href", n.url);
                    i.find(".product-description").text(s);
                    i.find(".price").html(Bizweb.formatMoney(n.price, window.money_format));
                    i.find(".product-item").attr("id", "product-" + n.id);
                    i.find(".vendor span").html(n.vendor);
                    i.find(".variants").attr("id", "product-actions-" + n.id);
                    i.find(".variants select").attr("id", "product-select-" + n.id);
                    i.find(".variants input.variant").attr("value",n.id);
                    if (n.compare_at_price > n.price) {
                        i.find(".compare-price").html(Bizweb.formatMoney(n.compare_at_price_max, window.money_format)).show();
                       
                    } else {
                        i.find(".compare-price").html("");                            
                    }
                    if (!n.available) {
                        i.find("select, input, label").remove();
                        
                        i.find(".add-to-cart-btn").text("Unavailable").addClass("disabled").attr("disabled", "disabled");
                    } else {
                        i.find(".total-price span").html(Bizweb.formatMoney(n.price, window.money_format));
                        t.createQuickViewVariants(n, i);

                    }
                    t.loadQuickViewSlider(n, i);
                    t.initQuickviewAddToCart();
                    $(".quick-view").fadeIn(500);

                });
                return false
            });
            $(".quick-view .overlay, .quick-view .close-window").live("click", function() {
                $(".quick-view").fadeOut(500);
                return false;
            })
        },
        showModal: function(n) {
            $(n).fadeIn(500);
            t.timeout = setTimeout(function() {
                $(n).fadeOut(500)
            }, 4e3)
        },
        initModal: function(){
            $('.close-modal, .overlay').click(function(){
                clearTimeout(t.timeout);
                $('.ajax-success-modal').fadeOut(300);
            })
        },
		
		initScrollTop: function() {
            $("#back-top").click(function() {				
                return $("body,html").animate({
                    scrollTop: 0
                }, 400), !1
				return false
            })
        },
        initOwlThumbsImage: function() {
            $(".thumbnail-images").owlCarousel({
                navigation: !0,
                navigationText:['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>'],
                items: 5,
                itemsDesktop: [1199, 4],
                itemsDesktopSmall: [979, 4],
                itemsTablet: [768, 4],
                itemsTabletSmall: [540, 3],
                itemsMobile: [360, 3]
            });
        },
        
         initRelatedProduct: function(){
            $(".owl-related-product").owlCarousel({
                navigation: !0,
                navigationText:['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>'],
                items: 4,
                itemsDesktop: [1199, 3],
                itemsDesktopSmall: [979, 2],
                itemsTablet: [768, 2],
                itemsTabletSmall: [540, 2],
                itemsMobile: [360, 1]
            });
        },
		initNavCollection: function(){
			$('body').scrollspy({target: "#scrollspy", offset: 70}); 			
			$('.nav-fixed-left a').click(function() {				
				var location = $( $.attr(this, 'href') ).offset().top - 10;
				$('html, body').animate({
					scrollTop: ($( $.attr(this, 'href') ).offset().top - 10)
				}, 500);
				return false;
			});
		},
        loadQuickViewSlider: function(n, r) {
            var s = Bizweb.resizeImage(n.featured_image, "grande");
            r.find(".quickview-featured-image").append('<a href="' + n.url + '"><img src="' + s + '" title="' + n.name + '"/></a>');
            if (n.images.length > 0) {
                var o = r.find(".more-view-wrapper ul");
                for (i in n.images) {
                    var u = Bizweb.resizeImage(n.images[i], "grande");
                    var a = Bizweb.resizeImage(n.images[i], "compact");
                    var l = Bizweb.resizeImage(n.images[i], "original");
                    var f = '<li><a href="javascript:void(0)"  data-image="' + u + '"><img src="' + a + '" class="img-responsive"  /></a></li>';
                    o.append(f)
                }
                o.find("a").click(function() {                    
                    var t = r.find(".quickview-featured-image img");
                    if (t.attr("src") != $(this).attr("data-image")) {
                        t.attr("src", $(this).attr("data-image"));
                        t.load(function(t) {
                            $(this).unbind("load");
                        })
                    }
                });
                t.initQuickViewCarousel(o)

            }
        },
        initQuickViewCarousel: function(e) {
            if (e) {
                e.owlCarousel({
                    item: 5,
					itemsDesktop: [1199, 5],
                	itemsDesktopSmall: [979, 5],
                	itemsTablet: [768, 5],
                	itemsTabletSmall: [540, 5],
                	itemsMobile: [360, 3],
                    pagination: false,
                    navigation: true,                       
                    navigationText: ['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>'] 
                });
            }
        },
        initQuickviewAddToCart: function() {
            if ($(".quick-view .quickview-add-cart-btn").length > 0) {
                $(".quick-view .quickview-add-cart-btn").click(function() {
                    var n = $(".quick-view select[name=id]").val();
                    if (!n) {
                        n = $(".quick-view input[name=id]").val()
                    }
                    var r = $(".quick-view input[name=quantity]").val();
                    if (!r) {
                        r = 1
                    }
                    var i = $(".quick-view .product-title a").text();
                    var s = $(".quick-view .quickview-featured-image img").attr("src");
                    t.doAjaxAddToCart(n, r, i, s);
                    $(".quick-view").fadeOut(300)
                })
            }
        },
        initProductAddToCart: function(){
            if ($('.product-ajax-add-cart').length > 0){
                $('.product-ajax-add-cart').click(function(n){
                    n.preventDefault();
                    if($(this).attr('disabled') != 'disabled'){
                        
                        var r = $(".product-form select[name=variantId]").val();
                        if (!r){
                            r = $(".product-form input[name=variantId]").val();                                
                        }
                        var i = $(".product-form input[name=quantity]").val();
                        if (!i) {
                            i = 1
                        }
                        var s = $('.product-single h1').text();                          
                        var o = $('#product-featured-image').attr('src');
                        t.doAjaxAddToCart(r,i,s,o)
                    }
                })
            }
        },
		initAjaxAddToCart: function(){
            if ($('.add-ajax-cart').length > 0){
                $('.add-ajax-cart').click(function(n){
                    n.preventDefault();
                    if($(this).attr('disabled') != 'disabled'){
                        var r = $(this).parents('.product-ajax-cart');
                        var i = $(r).attr('id');
                        i = i.match(/\d+/g);                        
                        
                        var s = $('#product-actions-' + i + ' input[name=variantId]').val();                               
                        
                        var o = $('#product-actions-' + i + ' input[name=quantity]').val();
                        if (!o) {
                            o = 1
                        }                        
                        var u = $(r).find('h3').text();                        
                        var a = $(r).find('.product-img .primary-img').attr('src');                        
                        t.doAjaxAddToCart(s,o,u,a)
                    }
                })
            }
        },
        initAddToCart: function(){
            if ($('.add-cart').length > 0){
                $('.add-cart').click(function(n){
                    n.preventDefault();
                    if($(this).attr('disabled') != 'disabled'){
                        var r = $(this).parents('.product-ajax-cart');
                        var i = $(r).attr('id');
                        i = i.match(/\d+/g);                        
                        
                        var s = $('#product-actions-' + i + ' input[name=variantId]').val();                               
                        
                        var o = $('#product-actions-' + i + ' input[name=quantity]').val();
                        if (!o) {
                            o = 1
                        }                        
                        var u = $(r).find('h3').text();                        
                        var a = $(r).find('.product-img .primary-img').attr('src');                        
                        t.doAjaxAddToCart(s,o,u,a)
                    }
                })
            }
        },
        doAjaxAddToCart: function(n, r, i, s) {
            $.ajax({
                type: "POST",
                url: "/cart/add.js",
                data: "quantity=" + r + "&variantId=" + n,
                dataType: "json",
                success: function(n) {
                    $(".ajax-success-modal").find(".ajax-product-title").text(i);
                    $(".ajax-success-modal").find(".ajax-product-image").attr("src", s);                        
                    $(".ajax-success-modal").find(".ajax-product-quantity").html('Số lượng:' + r)
                    t.showModal(".ajax-success-modal");
                    t.updateDropdownCart()
                },
                error: function(n, r) {                     
                    $('.ajax-error-message').text($.parseJSON(n.responseText).description);
                    t.showModal('.ajax-error-modal');
                    
                }
            })
        },
        updateDropdownCart: function() {
            Bizweb.getCart(function(e) {
                t.doUpdateDropdownCart(e)
            })
        },
        doUpdateDropdownCart: function(n) {
             var r = '<div class="item clearfix" id="item-{ID}"><a href="{URL}" class="item-img"><img src="{IMAGE}" class="img-fix" alt="{TITLE}"></a><div class="item-info"><h3><a href="{URL}">{TITLE}</a></h3><p class="item-price">{PRICE}</p><p class="item-quantity">Số lượng: <b>{QUANTITY}</b></p></div><a href="javascript:void(0);" class="remove" title="Xóa sản phẩm này"><i class="ico-remove sprite"></i></a></div>';
             $(".bottom-header .ico-cart").attr('data-count',n.item_count);
			 $(".mini-cart-count").html(n.item_count);
            $("#mini-cart-modal .wrapper-mini-cart").html("");
            if (n.item_count > 0) {
                for (var i = 0; i < n.items.length; i++) {
                    var s = r;
                    s = s.replace(/\{ID\}/g, n.items[i].variant_id);
                    s = s.replace(/\{URL\}/g, n.items[i].url);
                    s = s.replace(/\{TITLE\}/g, n.items[i].name);
                    s = s.replace(/\{VARIANT_TITLE\}/g, n.items[i].variant_title);
                    s = s.replace(/\{QUANTITY\}/g, n.items[i].quantity);
                    s = s.replace(/\{IMAGE\}/g, Bizweb.resizeImage(n.items[i].image, "small"));
                    s = s.replace(/\{PRICE\}/g, Bizweb.formatMoney(n.items[i].price, window.money_format));
                    $("#mini-cart-modal .wrapper-mini-cart").append(s)
                }
                $("#mini-cart-modal .wrapper-mini-cart .remove").click(function(n) {
                    n.preventDefault();
                    var r = $(this).parents(".item").attr("id");
                    r = r.match(/\d+/g);
                    Bizweb.removeItem(r, function(e) {
                        t.doUpdateDropdownCart(e)
                    })
                });

            }
            t.checkItemsInDropdownCart()
        },
        checkItemsInDropdownCart: function() {
            if ($("#mini-cart-modal .wrapper-mini-cart").children().length > 0) {                  
                $("#mini-cart-modal .no-item").hide();
                $("#mini-cart-modal .has-items").show();                
            } else {
                $("#mini-cart-modal .has-items").hide();
                $("#mini-cart-modal .no-item").show();                
            }
        },
        createQuickViewVariants: function(t, n) {
            if (t.variants.length > 1) {
                for (var r = 0; r < t.variants.length; r++) {
                    var i = t.variants[r];
                    var s = '<option value="' + i.id + '">' + i.title + "</option>";
                    n.find("form.variants > select").append(s)
                }
                new Bizweb.OptionSelectors("product-select-" + t.id, {
                    product: t,
                    onVariantSelected: selectCallbackQuickview
                });

                $(".quick-view .selectize-input input").attr("disabled", "disabled");
                if (t.options.length == 1) {
                    $(".selector-wrapper:eq(0)").prepend("<label>" + t.options[0].name + "</label>")
                }
                n.find("form.variants .selector-wrapper label").each(function(n, r) {
                    $(this).html(t.options[n].name)
                })
            } else {
                n.find("form.variants > select").remove();
                var o = '<input type="hidden" name="id" value="' + t.variants[0].id + '">';
                n.find("form.variants").append(o)
            }
        }
	}

	
})(jQuery);

 $(document).ready(function(){
         
         jQuery('.swatch :radio').change(function() {
           var optionIndex = jQuery(this).closest('.swatch').attr('data-option-index');
           var optionValue = jQuery(this).val();
           jQuery(this)
             .closest('form')
             .find('.single-option-selector')
             .eq(optionIndex)
             .val(optionValue)
             .trigger('change');
         });
             
         
         
         
         $('#address-box').on('show.bs.modal', function (e) {    
         $('#gmap').gMap({
         zoom: 17,
         scrollwheel: true,
         maptype: 'ROADMAP',
         markers:[
         {
           address: 'Số 2A, Đường 15, Khu Phố 1, Phường Linh Đông, Quận Thủ Đức, TP.Hồ Chí Minh, Việt Nam ',
           html: '_address'
         }
         ]
         }); 
         });
         
         
         $('[data-toggle="tooltip"]').tooltip();
           var lists_clone = '';
           var $top_linklists = $('.top-linklists>li');
           if ($top_linklists.length > 0){
             for (i = 0;i<$top_linklists.length;i++){
               lists_clone = $top_linklists[i].cloneNode(true);
               $('.top-linklists-mobile').append(lists_clone);
             }     
           }
           $('.top-linklists-mobile .sub-linklist ').css('display','none');
           $('.top-linklists-mobile a').children('.fa-angle-down').remove();
         
})

  function showRecoverPasswordForm() {
    document.getElementById('recover_password').style.display = 'block';
    document.getElementById('login').style.display='none';
  }

  function hideRecoverPasswordForm() {
    document.getElementById('recover_password').style.display = 'none';
    document.getElementById('login').style.display = 'block';
  }

  // Allow deep linking to the recover password form
  if (window.location.hash == '#recover') { showRecoverPasswordForm() }
