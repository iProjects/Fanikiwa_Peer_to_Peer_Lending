package com.sp.fanikiwa;

import java.util.List;

import com.google.api.client.extensions.android.http.AndroidHttp;
import com.google.api.client.json.gson.GsonFactory;
import com.sp.fanikiwa.entity.accountendpoint.Accountendpoint;
import com.sp.fanikiwa.entity.memberendpoint.model.CollectionResponseAccount;
import com.sp.fanikiwa.entity.memberendpoint.model.Account;
import com.sp.fanikiwa.entity.memberendpoint.Memberendpoint;
import com.sp.fanikiwa.entity.memberendpoint.model.Member;
import com.sp.fanikiwa.R;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.text.SpannableString;
import android.text.style.ForegroundColorSpan;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Toast;

public class FragmentTabMyAccounts extends Fragment {

	// variables declaration
	ListView listViewAccounts;
	private AdapterMyAccounts adapter = null;
	public static final String LOGIN_PREFERENCES = "LoginPrefs";
	public static final String USER_ID = "USER_ID";

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View view = inflater.inflate(R.layout.fragmenttab_myaccounts,
				container, false);
		listViewAccounts = (ListView) view.findViewById(R.id.listaccounts);
		new MyAccountsListAsyncTask(this.getActivity()).execute();
		return view;
	}

	public void LOadMiniStatement(View view) {
		// Create new fragment and transaction
		FragmentTabMiniStatement miniStatementFragment = new FragmentTabMiniStatement();
		FragmentTransaction fragmentTransaction = getFragmentManager()
				.beginTransaction();
		// add the fragment_container view with this fragment,
		// and add the transaction to the back stack
		fragmentTransaction.add(R.id.pager, miniStatementFragment);
		fragmentTransaction.addToBackStack(null);
		// Commit the transaction
		fragmentTransaction.commit();
	}

	public void LoadStatement(View view) {
		// Create new fragment and transaction
		FragmentTabStatement statementFragment = new FragmentTabStatement();
		FragmentTransaction fragmentTransaction = getFragmentManager()
				.beginTransaction();
		// add the fragment_container view with this fragment,
		// and add the transaction to the back stack
		fragmentTransaction.add(R.id.pager, statementFragment);
		fragmentTransaction.addToBackStack(null);
		// Commit the transaction
		fragmentTransaction.commit();
	}

	private class MyAccountsListAsyncTask extends
			AsyncTask<Void, Void, CollectionResponseAccount> {
		Context context;
		private ProgressDialog pd;

		public MyAccountsListAsyncTask(Context context) {
			this.context = context;
		}

		@Override
		protected void onPreExecute() {
			super.onPreExecute();
			pd = new ProgressDialog(context);
			pd.setMessage("Loading accounts...");
			pd.show();
		}

		@Override
		protected CollectionResponseAccount doInBackground(Void... unused) {
			CollectionResponseAccount accounts = new CollectionResponseAccount();
			try {
				Memberendpoint.Builder memberbuilder = new Memberendpoint.Builder(
						AndroidHttp.newCompatibleTransport(),
						new GsonFactory(), null);

				Memberendpoint memberservice = memberbuilder.build();

				String userid = getSharedPreferences(LOGIN_PREFERENCES,
						getString(R.string.Userid), "anonymous");

				accounts = memberservice.listMemberAccountWeb(userid).execute();
			} catch (Exception e) {
				Log.d("Could not retrieve Accounts", e.getMessage(), e);
			}
			return accounts;
		}

		@Override
		protected void onPostExecute(CollectionResponseAccount accounts) {
			pd.dismiss();
			if (accounts != null) {
				List<Account> list = accounts.getItems();
				adapter = new AdapterMyAccounts(context, list);
				listViewAccounts.setAdapter(adapter);
			}
		}
	}

	private void setSharedPreferences(final String name, String key,
			String value) {

		SharedPreferences sharedPref = FragmentTabMyAccounts.this.getActivity()
				.getApplicationContext()
				.getSharedPreferences(name, Context.MODE_PRIVATE);
		SharedPreferences.Editor editor = sharedPref.edit();
		editor.putString(key, value);
		editor.commit();

	}

	private String getSharedPreferences(final String name, String key,
			String defaultValue) {

		SharedPreferences sharedPref = FragmentTabMyAccounts.this.getActivity()
				.getSharedPreferences(name, Context.MODE_PRIVATE);
		String value = sharedPref.getString(key, defaultValue);
		return value;
	}

}
