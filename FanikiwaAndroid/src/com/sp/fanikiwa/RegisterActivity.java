package com.sp.fanikiwa;

import java.util.Date;

import com.google.api.client.extensions.android.http.AndroidHttp;
import com.google.api.client.json.gson.GsonFactory;
import com.google.api.client.util.ArrayMap;
import com.sp.fanikiwa.entity.memberendpoint.Memberendpoint;
import com.sp.fanikiwa.entity.memberendpoint.model.Member;
import com.sp.fanikiwa.entity.memberendpoint.model.MemberDTO;
import com.sp.fanikiwa.entity.memberendpoint.model.RequestResult;
import com.sp.fanikiwa.entity.memberendpoint.model.UserDTO;
import com.sp.fanikiwa.entity.userprofileendpoint.Userprofileendpoint;
import com.sp.fanikiwa.entity.userprofileendpoint.model.Userprofile;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.ActionBarActivity;
import android.text.SpannableString;
import android.text.style.ForegroundColorSpan;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

public class RegisterActivity extends Activity {

	EditText txtemail;
	EditText txtpwd;
	EditText txttelephone;
	EditText txtsurname;
	public String rootUrl;

	public static final String LOGIN_PREFERENCES = "LoginPrefs";
	public static final String PREFS_BOOT = "BootPreferences";

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		// Set View to register.xml
		setContentView(R.layout.register);

		// Event Listener for Login button
		Button btnLogin = (Button) findViewById(R.id.btnLogin);
		btnLogin.setOnClickListener(new View.OnClickListener() {

			public void onClick(View arg0) {
				// Closing registration screen
				// Switching to Login Screen/closing register screen
				Intent i = new Intent(getApplicationContext(),
						LoginActivity.class);
				startActivity(i);
				finish();
			}
		});

		// Event Listener for Register button
		Button btnRegister = (Button) findViewById(R.id.btnRegister);
		btnRegister.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {

				txtemail = (EditText) findViewById(R.id.reg_email);
				txtemail.requestFocus();
				txtpwd = (EditText) findViewById(R.id.reg_password);
				txttelephone = (EditText) findViewById(R.id.reg_telephone);
				txtsurname = (EditText) findViewById(R.id.reg_surname);

				String email = txtemail.getText().toString().trim();
				String pwd = txtpwd.getText().toString().trim();
				String telephone = txttelephone.getText().toString().trim();
				String surname = txtsurname.getText().toString().trim();

				if ((email.length() == 0)) {
					SpannableString spannableString = new SpannableString(
							"You need to provide values for Email");
					spannableString.setSpan(
							new ForegroundColorSpan(getResources().getColor(
									android.R.color.holo_red_light)), 0,
							spannableString.length(), 0);
					Toast.makeText(getBaseContext(), spannableString,
							Toast.LENGTH_LONG).show();
					return;
				}

				if ((pwd.length() == 0)) {
					SpannableString spannableString = new SpannableString(
							"You need to provide values for Password");
					spannableString.setSpan(
							new ForegroundColorSpan(getResources().getColor(
									android.R.color.holo_red_light)), 0,
							spannableString.length(), 0);
					Toast.makeText(getBaseContext(), spannableString,
							Toast.LENGTH_LONG).show();
					return;
				}

				if ((telephone.length() == 0)) {
					SpannableString spannableString = new SpannableString(
							"You need to provide values for Telephone");
					spannableString.setSpan(
							new ForegroundColorSpan(getResources().getColor(
									android.R.color.holo_red_light)), 0,
							spannableString.length(), 0);
					Toast.makeText(getBaseContext(), spannableString,
							Toast.LENGTH_LONG).show();
					return;
				}

				if ((surname.length() == 0)) {
					SpannableString spannableString = new SpannableString(
							"You need to provide values for SurName");
					spannableString.setSpan(
							new ForegroundColorSpan(getResources().getColor(
									android.R.color.holo_red_light)), 0,
							spannableString.length(), 0);
					Toast.makeText(getBaseContext(), spannableString,
							Toast.LENGTH_LONG).show();
					return;
				}

				// Go ahead and perform the transaction
				String[] params = { email, pwd, telephone, surname };
				new RegisterAsyncTask(RegisterActivity.this).execute(params);

			}
		});

	}

	private class RegisterAsyncTask extends
			AsyncTask<String, Void, RequestResult> {
		Context context;
		private ProgressDialog pd;

		public RegisterAsyncTask(Context context) {
			this.context = context;
		}

		protected void onPreExecute() {
			super.onPreExecute();
			pd = new ProgressDialog(context);
			pd.setMessage("Registering...");
			pd.show();
		}

		protected RequestResult doInBackground(String... params) {
			RequestResult response = new RequestResult();
			try {

				Memberendpoint.Builder builder = new Memberendpoint.Builder(
						AndroidHttp.newCompatibleTransport(),
						new GsonFactory(), null);
				// BuilderHelper.SetRoot(builder);

				Memberendpoint service = builder.build();
				UserDTO userDTO = new UserDTO();
				// at this point, fill the member with the details from the UI
				// email, pwd, telephone, surname
				userDTO.setEmail(params[0]);
				userDTO.setPwd(params[1]);
				userDTO.setTelephone(params[2]);
				userDTO.setSurname(params[3]);
				userDTO.setUserType("Member");
				userDTO.setStatus("Active");

				response = service.register(userDTO).execute();

				// save in shared preffrences
				if (response.getSuccess()) {
					saveUserSharedPreferences( (ArrayMap) response.getClientToken());
				}

				Log.d("RegisterAsyncTask", "Member registered. Memerid="
						+ response.getResultMessage().toString());
				return response;
			} catch (Exception e) {
				response.setSuccess(false);
				response.setResultMessage("Operation failed. Error.../n "
						+ e.getMessage());
				Log.d("RegisterAsyncTask", "Error on registration activity..."
						+ e.getMessage(), e);
			}
			return response;
		}

		protected void onPostExecute(RequestResult requestResult) {
			// Clear the progress dialog and the fields
			pd.dismiss();
			txtsurname.setText("");
			txtemail.setText("");
			txtpwd.setText("");
			txttelephone.setText("");

			if (requestResult.getSuccess()) {
				SpannableString spannableString = new SpannableString(
						"Registration succesfull /n"
								+ requestResult.getResultMessage().toString());
				spannableString.setSpan(new ForegroundColorSpan(getResources()
						.getColor(android.R.color.holo_blue_light)), 0,
						spannableString.length(), 0);
				Toast.makeText(getBaseContext(), spannableString,
						Toast.LENGTH_LONG).show();

				Intent i = new Intent(getApplicationContext(),
						LoginActivity.class);
				startActivity(i);

			} else {
				Toast.makeText(getBaseContext(),
						requestResult.getResultMessage().toString(),
						Toast.LENGTH_LONG).show();
			}

		}
	}

	private void saveUserSharedPreferences(final ArrayMap user) {

		if (user == null) {
			//
			Toast.makeText(getBaseContext(), "User profile is null",
					Toast.LENGTH_LONG).show();
			return;
		}
		
		SharedPreferences sharedPref = getApplicationContext()
				.getSharedPreferences(LOGIN_PREFERENCES, Context.MODE_PRIVATE);

		// userpofile
		SharedPreferences.Editor editor = sharedPref.edit();
		editor.putString("USERID", user.get("userId").toString());
		editor.putString("PWD", user.get("pwd").toString());
		editor.putString("TELNO", user.get("telephone").toString());
		editor.putString("USERTYPE", user.get("userType").toString());
		editor.commit();

		// boot
		SharedPreferences bootprefs = getSharedPreferences(PREFS_BOOT,
				MODE_PRIVATE);
		SharedPreferences.Editor editorBoot = bootprefs.edit();
		editorBoot.putBoolean("FirstTime", false);
		editorBoot.putBoolean("LoggedIn", false);
		editorBoot.commit();

	}

	private String getSharedPreferences(final String name, String key,
			String defaultValue) {

		SharedPreferences sharedPref = getApplicationContext()
				.getSharedPreferences(name, Context.MODE_PRIVATE);
		String value = sharedPref.getString(key, defaultValue);
		return value;
	}

}
