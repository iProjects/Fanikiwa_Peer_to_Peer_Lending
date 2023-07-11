var fanikiwa = fanikiwa || {};

function GETROOT() {
	var ROOT = 'https://1-dot-fanikiwaweb.appspot.com/_ah/api';
	if (window.location.hostname == 'localhost')
		ROOT = 'http://localhost:8888/_ah/api';
	return ROOT;
}

function LogOff() {
	sessionStorage.removeItem("loggedinuser");
	window.location.href = "/Views/Account/Login.html";
}

function ManageProfile() {
	//window.location.href = "/Views/Account/EditProfile.html";
}

function LogIn() {
	window.location.href = "/Views/Account/Login.html";
}

function Register() {
	window.location.href = "/Views/Account/Register.html";
}

Number.prototype.formatMoney = function(c, d, t) {
	var n = this, c = isNaN(c = Math.abs(c)) ? 2 : c, d = d == undefined ? "."
			: d, t = t == undefined ? "," : t, s = n < 0 ? "-" : "", i = parseInt(n = Math
			.abs(+n || 0).toFixed(c))
			+ "", j = (j = i.length) > 3 ? j % 3 : 0;
	return s + (j ? i.substr(0, j) + t : "")
			+ i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t)
			+ (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

function formatDate(date) {
	var d = new Date(date), month = '' + (d.getMonth() + 1), day = ''
			+ d.getDate(), year = d.getFullYear();

	if (month.length < 2)
		month = '0' + month;
	if (day.length < 2)
		day = '0' + day;

	return [ month, day, year ].join('-');
}
function formatDateForControl(date) {
	var d = new Date(date), month = '' + (d.getMonth() + 1), day = ''
			+ d.getDate(), year = d.getFullYear();

	if (month.length < 2)
		month = '0' + month;
	if (day.length < 2)
		day = '0' + day;

	return [ year, month, day ].join('-');
}
function decrementDateByMonth(oldDate, offset, offsetType) {
	return formatDateForControl(AddDate(oldDate, offset, offsetType));
}
function decrementDateByYear(oldDate, offset, offsetType) {
	return formatDateForControl(AddDate(oldDate, offset, offsetType));
}
function offsetDate(oldDate, offset, offsetType) {
	return formatDateForControl(AddDate(oldDate, offset, offsetType));
}
function AddDate(oldDate, offset, offsetType) {
	var year = parseInt(oldDate.getFullYear());
	var month = parseInt(oldDate.getMonth());
	var date = parseInt(oldDate.getDate());
	var hour = parseInt(oldDate.getHours());
	var newDate;
	switch (offsetType) {
	case "Y":
	case "y":
		newDate = new Date(year + offset, month, date, hour);
		break;

	case "M":
	case "m":
		newDate = new Date(year, month + offset, date, hour);
		break;

	case "D":
	case "d":
		newDate = new Date(year, month, date + offset, hour);
		break;

	case "H":
	case "h":
		newDate = new Date(year, month, date, hour + offset);
		break;
	}
	return newDate;
}

$(document).ready(function() {
	var loggedinuser = JSON.parse(sessionStorage.getItem('loggedinuser'));
	if (loggedinuser != null || loggedinuser != undefined) {
		var email = JSON.parse(sessionStorage.getItem('loggedinuser')).userId;
		$('#lnkloggedinuser').text(email);
	}
	setTime();
	setupTable();
});

function setTime() {
	var today = new Date();
	var year = today.getFullYear();
	var month = today.getMonth();
	var date = today.getDate();
	var hours = today.getHours();
	var minutes = today.getMinutes();
	var seconds = today.getSeconds();
	var milliseconds = today.getMilliseconds();
	var DayNight = "PM"
	if (hours < 12)
		DayNight = "AM";
	minutes = checkTime(minutes);
	seconds = checkTime(seconds);
	$('#footerdate').html(year);
	// $('#footerdate').html(
	// year + "-" + month + "-" + date + " " + hours + ":" + minutes
	// + ":" + seconds + ":" + milliseconds + " " + DayNight);
	var t = setTimeout(function() {
		setTime()
	}, 0.5);
}

function checkTime(i) {
	if (i < 10) {
		i = "0" + i
	}
	; // add zero in front of numbers < 10
	return i;
}

function setupTable() {
//	$(":table").DataTable(
//			{
//				"aLengthMenu" : [
//						[ 5, 10, 25, 50, 100, -1 ],
//						[ "5 Per Page", "10 Per Page", "25 Per Page",
//								"50 Per Page", "100 Per Page", "All" ] ],
//				"iDisplayLength" : 5
//			});
}

function isNumber(n) {
	  return $.isNumeric(n);
	}