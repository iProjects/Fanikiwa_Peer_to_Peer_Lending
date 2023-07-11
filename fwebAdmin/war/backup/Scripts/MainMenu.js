function CreateMainMenu() {
	var MainMenu = [];
	MainMenu.push('<div class="nav"><ul class="menu">');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Home/Index.html" style="cursor: pointer;" >Home</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Account/List.html" style="cursor: pointer;" >Accounts</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Setting/List.html" style="cursor: pointer;" >Settings</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/TransactionType/List.html" style="cursor: pointer;" >Transaction Types</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Offer/List.html" style="cursor: pointer;" >Offers</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Member/List.html" style="cursor: pointer;" >Members</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Organization/List.html" style="cursor: pointer;" >Organizations</a></div></div></li>');
	MainMenu.push('</ul></div>');

	$("#MainMenu").html(MainMenu.join(" "));
}

$(document).ready(function() {
	CreateMainMenu();
});