package com.sp.fanikiwa;

import android.content.Context;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;

public class AdapterViewPager extends FragmentPagerAdapter {

	final int PAGE_COUNT = 3;
	// Tab Titles
	private String tabtitles[] = new String[] { "Offers", "Account",
			"My Profile" };
	Context context;

	public AdapterViewPager(FragmentManager fm) {
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
			FragmentTabOffers fragmenttab1 = new FragmentTabOffers(); 
			return fragmenttab1;
		case 1:
			FragmentTabAccounts fragmenttab2 = new FragmentTabAccounts();
			return fragmenttab2;
		case 2:
			FragmentTabProfile fragmenttab3 = new FragmentTabProfile();
			return fragmenttab3;
		}
		return null;
	}

	@Override
	public CharSequence getPageTitle(int position) {
		return tabtitles[position];
	}
}