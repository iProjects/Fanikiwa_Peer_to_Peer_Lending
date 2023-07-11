package com.sp.fanikiwa;

import com.google.api.client.extensions.android.http.AndroidHttp;
import com.google.api.client.json.gson.GsonFactory;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;

import com.google.api.client.util.ArrayMap;
import com.sp.fanikiwa.entity.userprofileendpoint.Userprofileendpoint;
import com.sp.fanikiwa.entity.userprofileendpoint.model.RequestResult;

import android.text.SpannableString;
import android.text.style.ForegroundColorSpan;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class LoginActivity extends Activity {

	EditText txtusername;
	EditText txtpwd;
	public static final String LOGIN_PREFERENCES = "LoginPrefs";
	public static final String PREFS_BOOT = "BootPreferences";
	public static final String USER_ID = "USER_ID";

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		// setting default screen to login.xml

		setContentView(R.layout.login);

		txtusername = (EditText) findViewById(R.id.txtusername);

		txtpwd = (EditText) findViewById(R.id.txtpwd);

		// Read the shared prefferences
		SharedPreferences sharedPref = getApplicationContext()
				.getSharedPreferences(LOGIN_PREFERENCES, Context.MODE_PRIVATE);
		String userid = sharedPref.getString("USERID", "");
		String pwd = sharedPref.getString("PWD", "");

		// prepare text fields as per shared prefferences
		txtusername.setText(userid);
		txtusername.requestFocus();

		txtpwd.setText(pwd);

		// Event Listener for Login button
		Button btnLogin = (Button) findViewById(R.id.btnLogin);
		btnLogin.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				/*
				 * call boolean auth= UsersEndpoint.Login(userid, pwd) if(auth)
				 * {show mainactivity; set preferences} else Toast(Error)
				 */
				String username = txtusername.getText().toString().trim();
				String pwd = txtpwd.getText().toString().trim();

				if (username.length() == 0) {
					SpannableString spannableString = new SpannableString(
							"You need to provide values for UserName");
					spannableString.setSpan(
							new ForegroundColorSpan(getResources().getColor(
									android.R.color.holo_red_light)), 0,
							spannableString.length(), 0);
					Toast.makeText(getBaseContext(), spannableString,
							Toast.LENGTH_LONG).show();
					return;
				}

				if (pwd.length() == 0) {
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

				// Go ahead and perform the transaction
				String[] params = { username, pwd };
				new LoginAsyncTask(LoginActivity.this).execute(params);

			}
		});

		// Event Listener for Register button
		Button btnRegister = (Button) findViewById(R.id.btnRegister);
		btnRegister.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {
				// Switching to Register screen
				Intent i = new Intent(getApplicationContext(),
						RegisterActivity.class);
				startActivity(i);
			}
		});

	}

	private class LoginAsyncTask extends AsyncTask<String, Void, RequestResult> {
		Context context;
		private ProgressDialog pd;

		public LoginAsyncTask(Context context) {
			this.context = context;
		}

		@Override
		protected void onPreExecute() {
			super.onPreExecute();
			pd = new ProgressDialog(context);
			pd.setMessage("Authenticating...");
			pd.show();
		}

		@Override
		protected RequestResult doInBackground(String... params) {
			RequestResult response = new RequestResult();
			try {
				Userprofileendpoint.Builder builder = new Userprofileendpoint.Builder(
						AndroidHttp.newCompatibleTransport(),
						new GsonFactory(), null);
				// BuilderHelper.SetRoot(builder);
				Userprofileendpoint service = builder.build();
				// /TODO encrypt password
				response = service.login(params[0], params[1]).execute();
				if (response.getSuccess()) {
					saveUserSharedPreferences( (ArrayMap) response.getClientToken());
				}
				return response;
			} catch (Exception e) {
				response.setSuccess(false);
				response.setResultMessage("An error occured while calling service.login() \nError = "
						+ e.getMessage());
				Log.d("Could not Authenticate", e.getMessage(), e);
				return response;
			}
		}

		@Override
		protected void onPostExecute(RequestResult requestResult) {
			// Clear the progress dialog and the fields
			pd.dismiss();
			if (requestResult.getSuccess()) {
				txtusername.setText("");
				txtpwd.setText("");

				// Display success message to user
				SpannableString spannableString = new SpannableString(
						"Login successfull /n"
								+ requestResult.getResultMessage().toString());
				spannableString.setSpan(new ForegroundColorSpan(getResources()
						.getColor(android.R.color.holo_blue_light)), 0,
						spannableString.length(), 0);
				Toast.makeText(getBaseContext(), spannableString,
						Toast.LENGTH_LONG).show();

				Intent i = new Intent(getApplicationContext(),
						MainActivity.class);
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
		editorBoot.putBoolean("LoggedIn", true);
		editorBoot.commit();

	}

}
