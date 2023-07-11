package com.sp.fanikiwa;

import java.util.List;

import com.google.api.client.extensions.android.http.AndroidHttp;
import com.google.api.client.json.gson.GsonFactory;
import com.sp.fanikiwa.entity.offerendpoint.Offerendpoint;
import com.sp.fanikiwa.entity.offerendpoint.model.CollectionResponseOffer;
import com.sp.fanikiwa.entity.offerendpoint.model.Offer;
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

public class FragmentTabMyOffers extends Fragment {

	// variables declaration
	ListView listViewOffers;
	private AdapterMyOffers adapter = null;
	public static final String LOGIN_PREFERENCES = "LoginPrefs";

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// Get the view from fragmenttab1.xml
		View view = inflater.inflate(R.layout.fragmenttab_listmyoffers,
				container, false);

		listViewOffers = (ListView) view.findViewById(R.id.listmyoffers);
		// populate offers
		// offers = offerendpoint.listMemberOffers(member,limit=5,cursor)

		new MyOffersListAsyncTask(this.getActivity()).execute();

		return view;
	}

	private class MyOffersListAsyncTask extends
			AsyncTask<Void, Void, CollectionResponseOffer> {
		Context context;
		private ProgressDialog pd;

		public MyOffersListAsyncTask(Context context) {
			this.context = context;
		}

		protected void onPreExecute() {
			super.onPreExecute();
			pd = new ProgressDialog(context);
			pd.setMessage("loading my offers...");
			pd.show();
		}

		protected CollectionResponseOffer doInBackground(Void... unused) {
			CollectionResponseOffer offers = new CollectionResponseOffer();
			try {
				Offerendpoint.Builder builder = new Offerendpoint.Builder(
						AndroidHttp.newCompatibleTransport(),
						new GsonFactory(), null);
				Offerendpoint service = builder.build();

				String userid = getSharedPreferences(LOGIN_PREFERENCES,
						getString(R.string.Userid), "anonymous");

				offers = service.retrieveMyOffers(userid).execute();
			} catch (Exception e) {
				Log.d("Could not retrieve Offers", e.getMessage(), e);
			}
			return offers;
		}

		protected void onPostExecute(CollectionResponseOffer Offers) {
			pd.dismiss();
			if (Offers != null) {
				List<Offer> list = Offers.getItems();
				adapter = new AdapterMyOffers(context, list);
				listViewOffers.setAdapter(adapter);
			}
		}
	}

	private void setSharedPreferences(final String name, String key,
			String value) {

		SharedPreferences sharedPref = FragmentTabMyOffers.this.getActivity()
				.getApplicationContext()
				.getSharedPreferences(name, Context.MODE_PRIVATE);
		SharedPreferences.Editor editor = sharedPref.edit();
		editor.putString(key, value);
		editor.commit();

	}

	private String getSharedPreferences(final String name, String key,
			String defaultValue) {

		SharedPreferences sharedPref = FragmentTabMyOffers.this.getActivity()
				.getApplicationContext()
				.getSharedPreferences(name, Context.MODE_PRIVATE);
		String value = sharedPref.getString(key, defaultValue);
		return value;
	}

}
