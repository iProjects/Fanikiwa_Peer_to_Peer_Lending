package com.sp.fanikiwa;

import java.util.List;

import com.google.api.client.extensions.android.http.AndroidHttp;
import com.google.api.client.json.gson.GsonFactory; 
import com.sp.fanikiwa.entity.accountendpoint.Accountendpoint;
import com.sp.fanikiwa.entity.accountendpoint.model.CollectionResponseStatementModel;
import com.sp.fanikiwa.entity.accountendpoint.model.Account;
import com.sp.fanikiwa.entity.accountendpoint.model.StatementModel;
import com.sp.fanikiwa.R;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.text.SpannableString;
import android.text.style.ForegroundColorSpan;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

public class MiniStatementActivity extends Activity {

	// variables declaration
	ListView listViewMiniStatement;
	private AdapterMiniStatement adapter = null;
	public static final String LOGIN_PREFERENCES = "LoginPrefs";
	public static final String MINISTATEMENT_PREFERENCES = "MINISTATEMENT_PREFERENCES";

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		// Set View to register.xml
		setContentView(R.layout.fragmenttab_ministatement);

		listViewMiniStatement = (ListView) findViewById(R.id.listministatement);
		new MiniStatementListAsyncTask(MiniStatementActivity.this).execute();
	}

	private class MiniStatementListAsyncTask extends
			AsyncTask<Void, Void, CollectionResponseStatementModel> {
		Context context;
		private ProgressDialog pd;

		public MiniStatementListAsyncTask(Context context) {
			this.context = context;
		}

		protected void onPreExecute() {
			super.onPreExecute();
			pd = new ProgressDialog(context);
			pd.setMessage("Loading ministatement...");
			pd.show();
		}

		protected CollectionResponseStatementModel doInBackground(
				Void... unused) {
			CollectionResponseStatementModel transactions = new CollectionResponseStatementModel();
			try {
				Accountendpoint.Builder builder = new Accountendpoint.Builder(
						AndroidHttp.newCompatibleTransport(),
						new GsonFactory(), null);
				Accountendpoint service = builder.build();
				long accountID = Long.parseLong(getSharedPreferences(
						MINISTATEMENT_PREFERENCES,
						getString(R.string.ministatementaccountid), "0"));
				transactions = service.miniStatement(accountID).execute();
			} catch (Exception e) {
				Log.d("Could not retrieve ministatement", e.getMessage(), e);
			}
			return transactions;
		}

		protected void onPostExecute(
				CollectionResponseStatementModel transactions) {
			pd.dismiss();
			if (transactions != null) {
				List<StatementModel> list = transactions.getItems();
				adapter = new AdapterMiniStatement(context, list);
				listViewMiniStatement.setAdapter(adapter);
			}
		}
	}

	private void setSharedPreferences(final String name, String key,
			String value) {
		SharedPreferences sharedPref = getApplicationContext()
				.getSharedPreferences(name, Context.MODE_PRIVATE);
		SharedPreferences.Editor editor = sharedPref.edit();
		editor.putString(key, value);
		editor.commit();

	}

	private String getSharedPreferences(final String name, String key,
			String defaultValue) {
		SharedPreferences sharedPref = getApplicationContext()
				.getSharedPreferences(name, Context.MODE_PRIVATE);
		String value = sharedPref.getString(key, defaultValue);
		return value;
	}

}
