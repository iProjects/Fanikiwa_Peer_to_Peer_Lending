package com.sp.fanikiwa;

import java.util.ArrayList;
import java.util.HashMap;

import com.google.api.client.extensions.android.http.AndroidHttp;
import com.google.api.client.json.gson.GsonFactory;
import com.sp.fanikiwa.entity.offerendpoint.Offerendpoint;
import com.sp.fanikiwa.entity.offerendpoint.model.OfferDTO;
import com.sp.fanikiwa.entity.offerendpoint.model.RequestResult;
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
import android.view.View.OnClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;
import android.text.SpannableString;
import android.text.style.ForegroundColorSpan;

public class FragmentTabCreateOffer extends Fragment {

	EditText txtDescription;
	CheckBox chkPrivateOffer;
	Spinner cboOfferType;
	EditText txtAmount;
	EditText txtInterest;
	EditText txtTerm;
	CheckBox chkPartialPay;
	ProgressDialog mProgressDialog;
	public static final String LOGIN_PREFERENCES = "LoginPrefs"; 
	ArrayList<SpinnerDTO> offertypes = new ArrayList<SpinnerDTO>();

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// Get the view from fragmenttab1.xml
		View view = inflater.inflate(R.layout.fragmenttab_createoffer,
				container, false);

		txtDescription = (EditText) view.findViewById(R.id.offer_description);
		txtDescription.requestFocus();
		txtAmount = (EditText) view.findViewById(R.id.offer_Amount);
		txtInterest = (EditText) view.findViewById(R.id.offer_Interest);
		txtTerm = (EditText) view.findViewById(R.id.offer_Term);
		cboOfferType = (Spinner) view.findViewById(R.id.offer_OfferType);
		chkPrivateOffer = (CheckBox) view.findViewById(R.id.offer_PublicOffer);
		chkPartialPay = (CheckBox) view.findViewById(R.id.offer_partialPay);

		// Populate Spinners AsyncTask
		new PopulateSpinnersAsyncTask(this.getActivity()).execute();

		// Event Listener for MakeOFfer button
		Button btnMakeOFfer = (Button) view.findViewById(R.id.btnMakeOffer);
		btnMakeOFfer.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {

				// Check if values are provided
				String description = txtDescription.getText().toString().trim();
				String amount = txtAmount.getText().toString().trim();
				String interest = txtInterest.getText().toString().trim();
				String term = txtTerm.getText().toString().trim();
				SpinnerDTO selectedoffertype = (SpinnerDTO) cboOfferType
						.getSelectedItem(); // Object which was selected.
				String offertype = selectedoffertype.getId().trim();
				boolean privateoffer = chkPrivateOffer.isChecked();
				boolean partialpay = chkPartialPay.isChecked();

				if ((description.length() == 0)) {
					SpannableString spannableString = new SpannableString(
							"You need to provide values for Description");
					spannableString.setSpan(
							new ForegroundColorSpan(getResources().getColor(
									android.R.color.holo_red_light)), 0,
							spannableString.length(), 0);
					Toast.makeText(FragmentTabCreateOffer.this.getActivity(),
							spannableString, Toast.LENGTH_LONG).show();
					return;
				}
				if ((amount.length() == 0)) {
					SpannableString spannableString = new SpannableString(
							"You need to provide values for Amount");
					spannableString.setSpan(
							new ForegroundColorSpan(getResources().getColor(
									android.R.color.holo_red_light)), 0,
							spannableString.length(), 0);
					Toast.makeText(FragmentTabCreateOffer.this.getActivity(),
							spannableString, Toast.LENGTH_LONG).show();
					return;
				}

				if ((interest.length() == 0)) {
					SpannableString spannableString = new SpannableString(
							"You need to provide values for Interest");
					spannableString.setSpan(
							new ForegroundColorSpan(getResources().getColor(
									android.R.color.holo_red_light)), 0,
							spannableString.length(), 0);
					Toast.makeText(FragmentTabCreateOffer.this.getActivity(),
							spannableString, Toast.LENGTH_LONG).show();
					return;
				}

				if ((term.length() == 0)) {
					SpannableString spannableString = new SpannableString(
							"You need to provide values for Term");
					spannableString.setSpan(
							new ForegroundColorSpan(getResources().getColor(
									android.R.color.holo_red_light)), 0,
							spannableString.length(), 0);
					Toast.makeText(FragmentTabCreateOffer.this.getActivity(),
							spannableString, Toast.LENGTH_LONG).show();
					return;
				}

				if ((offertype.length() == 0)) {
					SpannableString spannableString = new SpannableString(
							"You need to provide values for Offer Type");
					spannableString.setSpan(
							new ForegroundColorSpan(getResources().getColor(
									android.R.color.holo_red_light)), 0,
							spannableString.length(), 0);
					Toast.makeText(FragmentTabCreateOffer.this.getActivity(),
							spannableString, Toast.LENGTH_LONG).show();
					return;
				}

				// Go ahead and perform the transaction
				Object[] params = { description, amount, interest, term,
						offertype, privateoffer, partialpay };
				new MakeOfferAsyncTask(FragmentTabCreateOffer.this
						.getActivity()).execute(params);

			}
		});

		return view;

	}

	private class MakeOfferAsyncTask extends
			AsyncTask<Object, Void, RequestResult> {
		Context context;
		private ProgressDialog pd;

		public MakeOfferAsyncTask(Context context) {
			this.context = context;
		}

		@Override
		protected void onPreExecute() {
			super.onPreExecute();
			pd = new ProgressDialog(context);
			pd.setMessage("creating offer...");
			pd.show();
		}

		@Override
		protected RequestResult doInBackground(Object... params) {
			RequestResult response = new RequestResult();
			try {
				Offerendpoint.Builder builder = new Offerendpoint.Builder(
						AndroidHttp.newCompatibleTransport(),
						new GsonFactory(), null);

				Offerendpoint service = builder.build();
				OfferDTO offerDto = new OfferDTO();
				offerDto.setDescription(params[0].toString());
				double amount = Double.parseDouble(params[1].toString());
				offerDto.setAmount(amount);
				double interest = Double.parseDouble(params[2].toString());
				offerDto.setInterest(interest);
				int term = Integer.parseInt(params[3].toString());
				offerDto.setTerm(term);
				offerDto.setOfferType(params[4].toString());
				Boolean privateOffer = Boolean.parseBoolean(params[5]
						.toString());
				offerDto.setPrivateOffer(privateOffer);
				Boolean partialpay = Boolean.parseBoolean(params[6].toString());
				offerDto.setPartialPay(partialpay);

				String userid = getSharedPreferences(LOGIN_PREFERENCES,
						getString(R.string.Userid), "anonymous");
				offerDto.setEmail(userid);

				response = service.saveOffer(offerDto).execute();
			} catch (Exception e) {
				response.setSuccess(false);
				response.setResultMessage("operation failed! Error.../n "
						+ e.getMessage());
				Log.d("Could not Authenticate", e.getMessage(), e);
				return response;
			}
			return response;
		}

		@Override
		protected void onPostExecute(RequestResult response) {
			// Clear the progress dialog and the fields
			pd.dismiss();
			if (response.getSuccess()) {
				txtDescription.setText("");
				txtAmount.setText("");
				txtInterest.setText("");
				txtTerm.setText("");
				chkPrivateOffer.setChecked(false);
				cboOfferType.setSelection(0);
				chkPartialPay.setChecked(false);

				// Display success message to user
				SpannableString spannableString = new SpannableString(
						"operation successfull.../n "
								+ response.getResultMessage());
				spannableString.setSpan(new ForegroundColorSpan(getResources()
						.getColor(android.R.color.holo_blue_light)), 0,
						spannableString.length(), 0);
				Toast.makeText(context, spannableString, Toast.LENGTH_LONG)
						.show();

			} else {
				Toast.makeText(FragmentTabCreateOffer.this.getActivity(),
						response.getResultMessage().toString(),
						Toast.LENGTH_LONG).show();
			}

		}
	}

	// Download JSON file AsyncTask
	private class PopulateSpinnersAsyncTask extends AsyncTask<Void, Void, Void> {
		Context context;

		public PopulateSpinnersAsyncTask(Context context) {
			this.context = context;
		}

		@Override
		protected Void doInBackground(Void... params) {
			offertypes = new ArrayList<SpinnerDTO>();
			try {
				offertypes.add(new SpinnerDTO("L", "Lend"));
				offertypes.add(new SpinnerDTO("B", "Borrow"));
			} catch (Exception e) {
				Log.e("Error", e.getMessage());
				e.printStackTrace();
			}
			return null;
		}

		@Override
		protected void onPostExecute(Void args) {

			cboOfferType.setAdapter(new ArrayAdapter<SpinnerDTO>(context,
					android.R.layout.simple_spinner_dropdown_item, offertypes));
		}
	}

	private void setSharedPreferences(final String name, String key,
			String value) {
		SharedPreferences sharedPref = FragmentTabCreateOffer.this
				.getActivity().getSharedPreferences(name, Context.MODE_PRIVATE);
		SharedPreferences.Editor editor = sharedPref.edit();
		editor.putString(key, value);
		editor.commit();
	}

	private String getSharedPreferences(final String name, String key,
			String defaultValue) {
		SharedPreferences sharedPref = FragmentTabCreateOffer.this
				.getActivity().getSharedPreferences(name, Context.MODE_PRIVATE);
		String value = sharedPref.getString(key, defaultValue);
		return value;
	}

}
