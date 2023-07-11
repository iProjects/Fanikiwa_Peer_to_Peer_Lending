package com.sp.fanikiwa;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;

public class BootActivity extends Activity  {
	public static final String PREFS_BOOT = "BootPreferences" ;
	   
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		//setContentView(R.layout.activity_boot);
		// MY_PREFS_NAME - a static String variable like: 
		//public static final String MY_PREFS_NAME = "MyPrefsFile";
		SharedPreferences prefs = getSharedPreferences(PREFS_BOOT, MODE_PRIVATE); 
		boolean firsttime = prefs.getBoolean("FirstTime", true);
		boolean loggedIn = prefs.getBoolean("LoggedIn", true);
		if(firsttime)
		{
			//start the registrationactivity
			 Intent myIntent = new Intent(BootActivity.this, RegisterActivity.class);
					 startActivity(myIntent);
					 
		}else if(loggedIn){
			//start the mainactivity
			 Intent myIntent = new Intent(BootActivity.this, MainActivity.class);
					 startActivity(myIntent);
		}else{
			//start the Loginactivity
			 Intent myIntent = new Intent(BootActivity.this, LoginActivity.class);
					 startActivity(myIntent);
		}
	}

}
