package com.sp.fanikiwa;

import java.util.List;
 
import com.sp.fanikiwa.entity.accountendpoint.model.StatementModel;
import com.sp.fanikiwa.R;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

public class AdapterStatement extends BaseAdapter {

	Context context;
	protected List<StatementModel> transactions;
	LayoutInflater inflater;

	public AdapterStatement(Context context, List<StatementModel> list) {
		this.transactions = list;
		this.inflater = LayoutInflater.from(context);
		this.context = context;
	}

	@Override
	public int getCount() {
		if (transactions != null)
			return transactions.size();
		else
			return 0;
	}

	@Override
	public StatementModel getItem(int position) {
		return transactions.get(position);
	}

	@Override
	public long getItemId(int position) {
		return transactions.get(position).getTransactionID();
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		ViewHolder holder;
		if (convertView == null) {

			holder = new ViewHolder();
			convertView = this.inflater.inflate(R.layout.itemstatement, parent,
					false);

			holder.txtPostDate = (TextView) convertView
					.findViewById(R.id.account_txtPostDate);
			holder.txtNarrative = (TextView) convertView
					.findViewById(R.id.account_txtNarrative);
			holder.txtDebit = (TextView) convertView
					.findViewById(R.id.account_txtDebit);
			holder.txtCredit = (TextView) convertView
					.findViewById(R.id.account_txtCredit);
			holder.txtBalance = (TextView) convertView
					.findViewById(R.id.account_txtBalance);

			convertView.setTag(holder);
		} else {

			holder = (ViewHolder) convertView.getTag();

			convertView = this.inflater.inflate(R.layout.itemstatement, parent,
					false);

			holder.txtPostDate = (TextView) convertView
					.findViewById(R.id.account_txtPostDate);
			holder.txtNarrative = (TextView) convertView
					.findViewById(R.id.account_txtNarrative);
			holder.txtDebit = (TextView) convertView
					.findViewById(R.id.account_txtDebit);
			holder.txtCredit = (TextView) convertView
					.findViewById(R.id.account_txtCredit);
			holder.txtBalance = (TextView) convertView
					.findViewById(R.id.account_txtBalance);

			convertView.setTag(holder);
		}

		StatementModel account = transactions.get(position);
		holder.txtPostDate.setText("Post Date: " + account.getPostDate());
		holder.txtNarrative.setText("Narrative: "
				+ account.getNarrative().toString());
		holder.txtDebit.setText("Debit: " + account.getDebit().toString());
		holder.txtCredit.setText("Credit: " + account.getCredit().toString());
		holder.txtBalance
				.setText("Balance: " + account.getBalance().toString());

		return convertView;
	}

	private class ViewHolder {
		TextView txtPostDate;
		TextView txtNarrative;
		TextView txtDebit;
		TextView txtCredit;
		TextView txtBalance;
	}
}
