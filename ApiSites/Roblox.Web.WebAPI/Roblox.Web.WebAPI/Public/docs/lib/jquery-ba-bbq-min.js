jQuery.fn.wiggle = function (e) { var a = { speed: 50, wiggles: 3, travel: 5, callback: null }, e = jQuery.extend(a, e); return this.each(function () { var a = this, l = (jQuery(this).wrap('<div class="wiggle-wrap"></div>').css("position", "relative"), 0); for (i = 1; i <= e.wiggles; i++)jQuery(this).animate({ left: "-=" + e.travel }, e.speed).animate({ left: "+=" + 2 * e.travel }, 2 * e.speed).animate({ left: "-=" + e.travel }, e.speed, function () { l++, jQuery(a).parent().hasClass("wiggle-wrap") && jQuery(a).parent().replaceWith(a), l == e.wiggles && jQuery.isFunction(e.callback) && e.callback() }) }) };