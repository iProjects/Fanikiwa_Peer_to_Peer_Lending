$(document).ready(function() {
	var loggedinuser = JSON.parse(sessionStorage.getItem('loggedinuser'));
	if (loggedinuser === null || loggedinuser === undefined) {
		window.location.href = "/Views/Account/Login.html";
	}
});
