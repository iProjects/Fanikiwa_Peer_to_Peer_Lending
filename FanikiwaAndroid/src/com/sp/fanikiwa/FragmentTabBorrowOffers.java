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
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ListView;
import android.widget.Toast;

public class FragmentTabBorrowOffers extends Fragment {

	// variables declaration
	ListView listViewOffers;
	private AdapterBorrowOffers adapter = null;
	public static final String LOGIN_PREFERENCES = "LoginPrefs";

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// Get the view from fragmenttab1.xml
		View view = inflater.inflate(R.layout.fragmenttab_listborrowoffers,
				container, false);

		listViewOffers = (ListView) view.findViewById(R.id.listborrowoffers);
		listViewOffers.setOnItemClickListener(new OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {

				Intent preferences = new Intent(view.getContext(),
						FragmentTabCreateOffer.class);
				preferences.putExtra("offerId", "offerId");
				startActivity(preferences);

			}
		}); 
		
		new BorrowOffersListAsyncTask(this.getActivity()).execute();

		return view;
	}

	private class BorrowOffersListAsyncTask extends
			AsyncTask<Void, Void, CollectionResponseOffer> {
		Context context;
		private ProgressDialog pd;

		public BorrowOffersListAsyncTask(Context context) {
			this.context = context;
		}

		protected void onPreExecute() {
			super.onPreExecute();
			pd = new ProgressDialog(context);
			pd.setMessage("loading borrow offers...");
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

				offers = service.retrieveBorrowOffers(userid).execute();
			} catch (Exception e) {
				Log.d("Could not retrieve Offers", e.getMessage(), e);
			}
			return offers;
		}

		protected void onPostExecute(CollectionResponseOffer Offers) {
			pd.dismiss();
			if (Offers != null) {
				List<Offer> list = Offers.getItems();
				adapter = new AdapterBorrowOffers(context, list);
				listViewOffers.setAdapter(adapter);
			}
		}
	}

	private void setSharedPreferences(final String name, String key,
			String value) {

		SharedPreferences sharedPref = FragmentTabBorrowOffers.this
				.getActivity().getApplicationContext()
				.getSharedPreferences(name, Context.MODE_PRIVATE);
		SharedPreferences.Editor editor = sharedPref.edit();
		editor.putString(key, value);
		editor.commit();

	}

	private String getSharedPreferences(final String name, String key,
			String defaultValue) {

		SharedPreferences sharedPref = FragmentTabBorrowOffers.this
				.getActivity().getApplicationContext()
				.getSharedPreferences(name, Context.MODE_PRIVATE);
		String value = sharedPref.getString(key, defaultValue);
		return value;
	}

}
