/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.diaryprogramcontrolendpoint = fanikiwa.diaryprogramcontrolendpoint
		|| {};
fanikiwa.diaryprogramcontrolendpoint.listdiaryprogramcontroltables = fanikiwa.diaryprogramcontrolendpoint.listdiaryprogramcontroltables
		|| {};

fanikiwa.diaryprogramcontrolendpoint.listdiaryprogramcontroltables.LoadDiaryprogramcontrolTables = function() {

	$('#listdiaryprogramcontrolResult').html('loading...');

	gapi.client.diaryprogramcontrolendpoint.listDiaryprogramcontrol().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listdiaryprogramcontrolResult').html(
								'There are no diary program control...');
					} else {
						buildTable(resp);
					}
				} else {
					console.log('Error: ' + resp.error.message);
					$('#errormessage').html(
							'operation failed! Error...<br/>'
									+ resp.error.message.toString());
					$('#successmessage').html('');
					$('#apiResults').html('');
				}
			},
			function(reason) {
				console.log('Error: ' + reason.result.error.message);
				$('#errormessage').html(
						'operation failed! Error...<br/>'
								+ reason.result.error.message);
				$('#successmessage').html('');
				$('#apiResults').html('');
			});
};

/**
 * Enables the button callbacks in the UI.
 */
fanikiwa.diaryprogramcontrolendpoint.listdiaryprogramcontroltables.enableButtons = function() {
	$("#btnLoad").removeAttr('style');
	$("#btnLoad").removeAttr('disabled');
	$("#btnLoad").val('Run Diary');
	var btnLoad = document.querySelector('#btnLoad');
	btnLoad.addEventListener('click', function() {
		fanikiwa.diaryprogramcontrolendpoint.listdiaryprogramcontroltables
				.RunDiary();
	});
	document.getElementById('dtpRunDate').value = formatDateForControl(new Date());
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.diaryprogramcontrolendpoint.listdiaryprogramcontroltables.init = function(
		apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.diaryprogramcontrolendpoint.listdiaryprogramcontroltables
					.enableButtons();
			fanikiwa.diaryprogramcontrolendpoint.listdiaryprogramcontroltables
					.LoadDiaryprogramcontrolTables();
		}
	}
	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('diaryprogramcontrolendpoint', 'v1', callback, apiRoot);

};

var diaryprogramcontrolTable = '';
function buildTable(response) {

	diaryprogramcontrolTable = '';

	PopulateDiaryprogramcontrolTable(response);

	$("#listdiaryprogramcontrolResult").html(diaryprogramcontrolTable);

	$('#listDiaryprogramcontrolTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

}

function PopulateDiaryprogramcontrolTable(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		diaryprogramcontrolTable += '<table id="listDiaryprogramcontrolTable">';
		diaryprogramcontrolTable += "<thead>";
		diaryprogramcontrolTable += "<tr>";
		diaryprogramcontrolTable += "<th>Id</th>";
		diaryprogramcontrolTable += "<th>Description</th>";
		diaryprogramcontrolTable += "</tr>";
		diaryprogramcontrolTable += "</thead>";
		diaryprogramcontrolTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			diaryprogramcontrolTable += '<tr>';
			diaryprogramcontrolTable += '<td>' + resp.result.items[i].id
					+ '</td>';
			diaryprogramcontrolTable += '<td>'
					+ resp.result.items[i].formatDate(lastRun) + '</td>';
			diaryprogramcontrolTable += '<td>'
					+ resp.result.items[i].formatDate(nextRun) + '</td>';
		}

		diaryprogramcontrolTable += "</tbody>";
		diaryprogramcontrolTable += "</table>";

	} else {
		console.log('Error: ' + resp.error.message);
		$('#errormessage').html(
				'operation failed! Error...<br/>' + resp.error.message);
		$('#successmessage').html('');
		$('#apiResults').html('');
	}
}

fanikiwa.diaryprogramcontrolendpoint.listdiaryprogramcontroltables.RunDiary = function() {

	var rundate = document.getElementById('dtpRunDate').value;
	var url = GETROOT();
	url += '/FanikiwaDiary';

	$('#apiResults').html('Processing...');

	$.ajax({
		url : url,
		type : 'POST',
		dataType : 'text',
		data : JSON.stringify({
			RunDate : rundate
		}),
		contentType : 'application/json',
		mimeType : 'application/json',
		success : function(resp) {
			$('#apiResults').html('');
			$('#errormessage').html('');
			$('#successmessage').html(resp);
		},
		error : function(xhr, status, error) {
			$('#apiResults').html('');
			$('#successmessage').html('');
			$('#errormessage').html(
					"xhr = " + xhr + "<br/>Status = " + status + "<br>Error = "
							+ error);
		}
	});

};
