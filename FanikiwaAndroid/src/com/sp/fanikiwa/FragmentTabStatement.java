package com.sp.fanikiwa;

import java.util.List;

import com.google.api.client.extensions.android.http.AndroidHttp;
import com.google.api.client.json.gson.GsonFactory;
import com.sp.fanikiwa.entity.accountendpoint.Accountendpoint; 
import com.sp.fanikiwa.entity.accountendpoint.model.CollectionResponseStatementModel;
import com.sp.fanikiwa.entity.accountendpoint.model.StatementModel;
import com.sp.fanikiwa.R;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;

public class FragmentTabStatement extends Fragment {

	// variables declaration
	ListView listViewStatement;
	private AdapterMiniStatement adapter = null;
	public static final String LOGIN_PREFERENCES = "LoginPrefs";
	public static final String STATEMENT_PREFERENCES = "STATEMENT_PREFERENCES";

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View view = inflater.inflate(R.layout.fragmenttab_statement, container,
				false);
		listViewStatement = (ListView) view.findViewById(R.id.liststatement);
		new StatementListAsyncTask(this.getActivity()).execute();
		return view;
	}

	private class StatementListAsyncTask extends
			AsyncTask<Void, Void, CollectionResponseStatementModel> {
		Context context;
		private ProgressDialog pd;

		public StatementListAsyncTask(Context context) {
			this.context = context;
		}

		protected void onPreExecute() {
			super.onPreExecute();
			pd = new ProgressDialog(context);
			pd.setMessage("Loading statement...");
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
						STATEMENT_PREFERENCES,
						getString(R.string.statementaccountid), "0"));
				transactions = service.miniStatement(accountID).execute();
			} catch (Exception e) {
				Log.d("Could not retrieve statement", e.getMessage(), e);
			}
			return transactions;
		}

		protected void onPostExecute(
				CollectionResponseStatementModel transactions) {
			pd.dismiss();
			if (transactions != null) {
				List<StatementModel> list = transactions.getItems();
				adapter = new AdapterMiniStatement(context, list);
				listViewStatement.setAdapter(adapter);
			}
		}
	}

	private void setSharedPreferences(final String name, String key,
			String value) {
		SharedPreferences sharedPref = FragmentTabStatement.this.getActivity()
				.getApplicationContext()
				.getSharedPreferences(name, Context.MODE_PRIVATE);
		SharedPreferences.Editor editor = sharedPref.edit();
		editor.putString(key, value);
		editor.commit();

	}

	private String getSharedPreferences(final String name, String key,
			String defaultValue) {
		SharedPreferences sharedPref = FragmentTabStatement.this.getActivity()
				.getSharedPreferences(name, Context.MODE_PRIVATE);
		String value = sharedPref.getString(key, defaultValue);
		return value;
	}

}
