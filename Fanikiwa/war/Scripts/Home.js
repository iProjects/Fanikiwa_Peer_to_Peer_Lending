function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Home/Help.html" style="cursor: pointer;">Help</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Contact/ContactUs.html" style="cursor: pointer;">Contact Us</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Home/About.html" style="cursor: pointer;">About</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
