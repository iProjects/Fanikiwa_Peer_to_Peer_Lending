package com.sp.fanikiwa;

import android.content.Context;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;

public class AdapterViewPagerOffers extends FragmentPagerAdapter {

	final int PAGE_COUNT = 4;
	// Tab Titles
	private String tabtitles[] = new String[] { "My Offers", 
			"Borrow Offers", "Lend Offers", "Create Offer"};
	Context context;

	public AdapterViewPagerOffers(FragmentManager fm) {
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
			FragmentTabMyOffers fragmenttab1 = new FragmentTabMyOffers();
			return fragmenttab1;
		case 1:
			FragmentTabBorrowOffers fragmenttab2 = new FragmentTabBorrowOffers(); 
			return fragmenttab2;
		case 2:
			FragmentTabLendOffers fragmenttab3 = new FragmentTabLendOffers(); 
			return fragmenttab3;
		case 3:
			FragmentTabCreateOffer fragmenttab4 = new FragmentTabCreateOffer(); 
			return fragmenttab4;
		}
		return null;
	}

	@Override
	public CharSequence getPageTitle(int position) {
		return tabtitles[position];
	}
}