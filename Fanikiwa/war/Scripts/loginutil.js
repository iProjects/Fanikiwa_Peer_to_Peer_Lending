
$(document)
		.ready(
				function() {
					var loggedinuser = JSON.parse(sessionStorage
							.getItem('loggedinuser'));
					if (loggedinuser != null || loggedinuser != undefined) {

						var login = [];
						login
								.push('<text> Welcome To Fanikiwa, <a id="lnkloggedinuser"'
										+ 'style="cursor: pointer;" href="#" onclick="ManageProfile()"'
										+ 'title="Manage Profile"></a> <a id="lnklogoff"'
										+ 'style="cursor: pointer;" href="#" onclick="LogOff()"'
										+ 'title="Log Off">Log off</a> </text>');
						$("#login").html(login.join(" "));
						
					} else {
						var loginopts = [];
						loginopts
								.push('<text> Welcome To Fanikiwa, Already Registered Click <a '
										+ 'href="/Views/Account/Login.html" style="cursor: pointer;"> Here </a> To '
										+ 'Login</a><br> Not Registered Click <a '
										+ 'href="/Views/Account/Register.html" style="cursor: pointer;"> Here '
										+ '</a> To Register </text>');
						$("#login").html(loginopts.join(" "));
					}

					setTime();
				});