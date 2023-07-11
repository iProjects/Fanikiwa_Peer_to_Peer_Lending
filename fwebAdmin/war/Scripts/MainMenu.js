function CreateMainMenu() {
	var MainMenu = [];
	MainMenu.push('<div class="nav"><ul class="menu">');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Home/Index.html" style="cursor: pointer;" >Home</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Organization/List.html" style="cursor: pointer;" >Organizations</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Coa/List.html" style="cursor: pointer;" >Coa</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/AccountType/List.html" style="cursor: pointer;" >Account Types</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Customer/List.html" style="cursor: pointer;" >Customers</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Account/List.html" style="cursor: pointer;" >Accounts</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Tieredtable/List.html" style="cursor: pointer;" >Tiered Tables</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/TransactionType/List.html" style="cursor: pointer;" >Transaction Types</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Setting/List.html" style="cursor: pointer;" >Settings</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Userprofile/List.html"style="cursor: pointer;" >Users</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Offer/List.html" style="cursor: pointer;" >Offers</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Loan/List.html" style="cursor: pointer;">Loans</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/STO/List.html" style="cursor: pointer;">Standing Orders</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Diaryprogramcontrol/List.html" style="cursor: pointer;">Diary Program Control</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/ValueDatedTransaction/List.html" style="cursor: pointer;">Value Dated Transaction</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Member/List.html" style="cursor: pointer;" >Members</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/LendingGroup/List.html"style="cursor: pointer;" >Lending Groups</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/MpesaIPNMessage/List.html"style="cursor: pointer;" >Mpesa IPN Messages</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/WithdrawalMessage/List.html"style="cursor: pointer;" >Withdrawal Messages</a></div></div></li>');
	MainMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Account/Post.html"style="cursor: pointer;" >Post</a></div></div></li>');
	MainMenu.push('</ul></div>');

	$("#MainMenu").html(MainMenu.join(" "));
}

$(document).ready(function() {
	var loggedinuser = JSON.parse(sessionStorage.getItem('loggedinuser'));
	if (loggedinuser === null || loggedinuser === undefined) {

	} else {
		CreateMainMenu();
	}
});