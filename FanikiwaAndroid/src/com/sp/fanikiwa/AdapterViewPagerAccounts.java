package com.sp.fanikiwa;

import android.content.Context;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;

public class AdapterViewPagerAccounts extends FragmentPagerAdapter {

	final int PAGE_COUNT = 1;
	// Tab Titles
	private String tabtitles[] = new String[] { "My Accounts" };
	Context context;

	public AdapterViewPagerAccounts(FragmentManager fm) {
		super(fm);
	}

	@Override
	public int getCount() {
		return PAGE_COUNT;
	}

	@Override
	public Fragment getItem(int position) {
		switch (position) {
		case 0:
			FragmentTabMyAccounts fragmenttab1 = new FragmentTabMyAccounts();
			return fragmenttab1;
		}
		return null;
	}

	@Override
	public CharSequence getPageTitle(int position) {
		return tabtitles[position];
	}
}