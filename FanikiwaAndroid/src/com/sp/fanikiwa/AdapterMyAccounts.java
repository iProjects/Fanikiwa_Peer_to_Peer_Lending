package com.sp.fanikiwa;

import java.text.DecimalFormat;
import java.util.List;

import com.sp.fanikiwa.entity.memberendpoint.model.Account;
import com.sp.fanikiwa.R;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

public class AdapterMyAccounts extends BaseAdapter {

	Context context;
	protected List<Account> Accounts;
	LayoutInflater inflater;

	public AdapterMyAccounts(Context context, List<Account> listAccounts) {
		this.Accounts = listAccounts;
		this.inflater = LayoutInflater.from(context);
		this.context = context;
	}

	@Override
	public int getCount() {
		if (Accounts != null)
			return Accounts.size();
		else
			return 0;
	}

	@Override
	public Account getItem(int position) {
		return Accounts.get(position);
	}

	@Override
	public long getItemId(int position) {
		return Accounts.get(position).getAccountID();
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		ViewHolder holder;
		if (convertView == null) {

			holder = new ViewHolder();
			convertView = this.inflater.inflate(R.layout.itemmyaccount, parent,
					false);

			holder.txtAccountID = (TextView) convertView
					.findViewById(R.id.account_id);
			holder.txtAccountName = (TextView) convertView
					.findViewById(R.id.account_name);
			holder.txtClearedBalance = (TextView) convertView
					.findViewById(R.id.account_clearedbalance);
			holder.txtBookBalance = (TextView) convertView
					.findViewById(R.id.account_bookbalance);
			holder.txtLimit = (TextView) convertView
					.findViewById(R.id.account_limit);

			convertView.setTag(holder);
		} else {

			holder = (ViewHolder) convertView.getTag();

			convertView = this.inflater.inflate(R.layout.itemmyaccount, parent,
					false);

			holder.txtAccountID = (TextView) convertView
					.findViewById(R.id.account_id);
			holder.txtAccountName = (TextView) convertView
					.findViewById(R.id.account_name);
			holder.txtClearedBalance = (TextView) convertView
					.findViewById(R.id.account_clearedbalance);
			holder.txtBookBalance = (TextView) convertView
					.findViewById(R.id.account_bookbalance);
			holder.txtLimit = (TextView) convertView
					.findViewById(R.id.account_limit);

			convertView.setTag(holder);
		}

		Account account = Accounts.get(position);
		holder.txtAccountID.setText("Account ID : " + account.getAccountID());
		holder.txtAccountName.setText("Account Name : "
				+ account.getAccountName().trim());
		holder.txtClearedBalance.setText("Cleared Balance : "
				+ FormatDecimal(account.getClearedBalance()).toString());
		holder.txtBookBalance.setText("Book Balance : "
				+ FormatDecimal(account.getBookBalance()).toString());
		holder.txtLimit.setText("Limit : "
				+ FormatDecimal(account.getLimit()).toString());

		return convertView;
	}

	private Double FormatDecimal(Double amount) {
		DecimalFormat decimalFormat = new DecimalFormat("#.##");
		return Double.parseDouble(decimalFormat.format(amount));
	}

	private class ViewHolder {
		TextView txtAccountID;
		TextView txtAccountName;
		TextView txtClearedBalance;
		TextView txtBookBalance;
		TextView txtLimit;
	}
}
