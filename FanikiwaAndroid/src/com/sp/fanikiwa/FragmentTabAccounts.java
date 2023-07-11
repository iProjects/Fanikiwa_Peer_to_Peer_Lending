package com.sp.fanikiwa;

import com.sp.fanikiwa.R;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentTabHost;
import android.support.v4.view.PagerAdapter;
import android.support.v4.view.ViewPager;

public class FragmentTabAccounts extends Fragment {

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// Get the view from fragmenttab1.xml
		View view = inflater.inflate(R.layout.fragmenttab_accounts, container,
				false);

		// populate offers
		// offers = offerendpoint.listMemberOffers(member,limit=5,cursor)
		ViewPager pager = (ViewPager) view.findViewById(R.id.pager);
		pager.setAdapter(buildAdapter());

		return (view);
	}

	private PagerAdapter buildAdapter() {
		return (new AdapterViewPagerAccounts(getChildFragmentManager()));
	}
}