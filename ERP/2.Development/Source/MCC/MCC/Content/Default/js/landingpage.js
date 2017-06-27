$(window).on("load",function(){
	tabsmenu("#whoweare");
});
function animateTabContent(currentElement,timeRunAnimate){
	setTimeout(function(){
		currentElement.removeAttr("style").addClass('animated zoomIn');
	},timeRunAnimate);
	setTimeout(function(){
		currentElement.removeClass('animated zoomIn');
	},timeRunAnimate + 1000);
}
function tabsmenu(id){

	var tabContent=$(id+" .tabs-content .tabs-content-item");
	var tabMenu=$(id+" .tabs-menu .tabs-menu-item");

	tabMenu.on("click",function(){

		var dataTab = $(this).attr("data-tab");

		//remove all class active
		tabMenu.removeClass("active");
		tabContent.removeClass("active");

		//active current tabs-menu
		$(this).addClass("active");

		//active current tabs-content
		var tabCurrentActive = $(id+" .tabs-content .tabs-content-item[data-tab='"+dataTab+"']");
		tabCurrentActive.addClass("active");

		//show item animate
		var timeRunAnimate=0;
		for(var i=0;i<tabCurrentActive.children().length;i++){
			animateTabContent(tabCurrentActive.children().eq(i),timeRunAnimate);
			timeRunAnimate +=100;
		}
	}) 
}
