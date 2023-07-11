package com.sp.fanikiwa;

import java.util.Locale;

import android.support.v7.app.ActionBarActivity;
import android.support.v7.app.ActionBar;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.app.FragmentPagerAdapter;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.view.ViewPager;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

public class MainActivity extends ActionBarActivity {

	/**
	 * The {@link android.support.v4.view.PagerAdapter} that will provide
	 * fragments for each of the sections. We use a {@link FragmentPagerAdapter}
	 * derivative, which will keep every loaded fragment in memory. If this
	 * becomes too memory intensive, it may be best to switch to a
	 * {@link android.support.v4.app.FragmentStatePagerAdapter}.
	 */
	SectionsPagerAdapter mSectionsPagerAdapter;

	/**
	 * The {@link ViewPager} that will host the section contents.
	 */
	ViewPager mViewPager;
	ActionBar actionbar;
	FragmentTransaction fragmentTransaction = getSupportFragmentManager()
			.beginTransaction();

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);

		// Create the adapter that will return a fragment for each of the three
		// primary sections of the activity.
		// mSectionsPagerAdapter = new SectionsPagerAdapter(
		// getSupportFragmentManager());
		//
		// // Set up the ViewPager with the sections adapter.
		// mViewPager = (ViewPager) findViewById(R.id.pager);
		// mViewPager.setAdapter(mSectionsPagerAdapter);

		// Locate the viewpager in activity_main.xml
		ViewPager viewPager = (ViewPager) findViewById(R.id.pager);
		// Set the ViewPagerAdapter into ViewPager
		viewPager.setAdapter(new AdapterViewPager(getSupportFragmentManager()));

		actionbar = getSupportActionBar();
		actionbar.setTitle(getString(R.string.title_activity_main));

		Fragment existsMiniStatementFragment = getSupportFragmentManager()
				.findFragmentById(R.id.pager);
		if (existsMiniStatementFragment instanceof FragmentTabMiniStatement) {
			fragmentTransaction.hide(existsMiniStatementFragment);
		}

		Fragment existsStatementFragment = getSupportFragmentManager()
				.findFragmentById(R.id.pager);
		if (existsStatementFragment instanceof FragmentTabStatement) {
			fragmentTransaction.hide(existsStatementFragment);
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu items for use in the action bar
		MenuInflater inflater = getMenuInflater();
		inflater.inflate(R.menu.main_activity_actions, menu);
		return super.onCreateOptionsMenu(menu);
	}

	// @Override
	// public boolean onCreateOptionsMenu(Menu menu) {
	// // Inflate the menu; this adds items to the action bar if it is present.
	// getMenuInflater().inflate(R.menu.main, menu);
	// return true;
	// }

	// @Override
	// public boolean onOptionsItemSelected(MenuItem item) {
	// // Handle presses on the action bar items
	// switch (item.getItemId()) {
	// case R.id.action_search:
	// openSearch();
	// return true;
	// case R.id.action_settings:
	// openSettings();
	// return true;
	// default:
	// return super.onOptionsItemSelected(item);
	// }
	// }
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
		case R.id.action_search:
			Toast.makeText(this, getString(R.string.action_search),
					Toast.LENGTH_LONG).show();
			return true;
		case R.id.action_settings:
			Toast.makeText(this, getString(R.string.action_settings),
					Toast.LENGTH_LONG).show();
			return true;
		case R.id.action_quit:
			Toast.makeText(this, getString(R.string.action_quit),
					Toast.LENGTH_LONG).show();
			finish(); // close the activity
			return true;
		default:
			return super.onOptionsItemSelected(item);
		}
	}

	// @Override
	// public boolean onOptionsItemSelected(MenuItem item) {
	// // Handle action bar item clicks here. The action bar will
	// // automatically handle clicks on the Home/Up button, so long
	// // as you specify a parent activity in AndroidManifest.xml.
	// int id = item.getItemId();
	// if (id == R.id.action_settings) {
	// return true;
	// }
	// return super.onOptionsItemSelected(item);
	// }

	public void LoadMiniStatement(View view) {

		Intent i = new Intent(getApplicationContext(),
				MiniStatementActivity.class);
		startActivity(i);

		// // Create new transaction
		// FragmentTransaction fragmentTransaction = getSupportFragmentManager()
		// .beginTransaction();
		// // Create new fragment
		// FragmentTabMiniStatement miniStatementFragment = new
		// FragmentTabMiniStatement();
		// // add the fragment_container view with this fragment,
		// // and add the transaction to the back stack
		// fragmentTransaction.add(R.id.pager, miniStatementFragment,
		// "FragmentTabMiniStatement");
		// Fragment existsminiStatementFragment = getSupportFragmentManager()
		// .findFragmentById(R.id.pager);
		// if (existsminiStatementFragment instanceof FragmentTabMiniStatement)
		// {
		// fragmentTransaction.hide(miniStatementFragment);
		// }
		// fragmentTransaction.addToBackStack("FragmentTabMiniStatement");
		// // Commit the transaction
		// fragmentTransaction.commit();
	}

	public void LoadStatement(View view) {
		// Create new fragment and transaction
		FragmentTabStatement statementFragment = new FragmentTabStatement();
		// add the fragment_container view with this fragment,
		// and add the transaction to the back stack
		fragmentTransaction.add(R.id.pager, statementFragment,
				"FragmentTabStatement");
		fragmentTransaction.addToBackStack(null);
		// Commit the transaction
		fragmentTransaction.commit();
	}

	/**
	 * A {@link FragmentPagerAdapter} that returns a fragment corresponding to
	 * one of the sections/tabs/pages.
	 */
	public class SectionsPagerAdapter extends FragmentPagerAdapter {

		public SectionsPagerAdapter(FragmentManager fm) {
			super(fm);
		}

		@Override
		public Fragment getItem(int position) {
			// getItem is called to instantiate the fragment for the given page.
			// Return a PlaceholderFragment (defined as a static inner class
			// below).
			return PlaceholderFragment.newInstance(position + 1);
		}

		@Override
		public int getCount() {
			// Show 3 total pages.
			return 3;
		}

		@Override
		public CharSequence getPageTitle(int position) {
			Locale l = Locale.getDefault();
			switch (position) {
			case 0:
				return getString(R.string.title_section1).toUpperCase(l);
			case 1:
				return getString(R.string.title_section2).toUpperCase(l);
			case 2:
				return getString(R.string.title_section3).toUpperCase(l);
			}
			return null;
		}
	}

	/**
	 * A placeholder fragment containing a simple view.
	 */
	public static class PlaceholderFragment extends Fragment {
		/**
		 * The fragment argument representing the section number for this
		 * fragment.
		 */
		private static final String ARG_SECTION_NUMBER = "3";

		/**
		 * Returns a new instance of this fragment for the given section number.
		 */
		public static PlaceholderFragment newInstance(int sectionNumber) {
			PlaceholderFragment fragment = new PlaceholderFragment();
			Bundle args = new Bundle();
			args.putInt(ARG_SECTION_NUMBER, sectionNumber);
			fragment.setArguments(args);
			return fragment;
		}

		public PlaceholderFragment() {
		}

		@Override
		public View onCreateView(LayoutInflater inflater, ViewGroup container,
				Bundle savedInstanceState) {
			View rootView = inflater.inflate(R.layout.fragment_main, container,
					false);
			return rootView;
		}
	}

}
