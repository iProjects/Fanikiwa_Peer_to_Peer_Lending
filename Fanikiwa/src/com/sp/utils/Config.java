package com.sp.utils;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

import com.sp.fanikiwa.api.AccountEndpoint;
import com.sp.fanikiwa.api.SettingsEndpoint;
import com.sp.fanikiwa.api.TransactionTypeEndpoint;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.Settings;
import com.sp.fanikiwa.entity.TransactionType;

public class Config {
	private final static String DATE_FORMAT = "dd-MMM-yyyy";
	
	public static String GetString(String key) {
		SettingsEndpoint sep = new SettingsEndpoint();
		Settings setting = sep.getSettingsByKey(key);
		if (setting != null) {
			return setting.getValue();
		} else {
			return null;
		}
	}

	public static String GetString(String key, String def) {
		String ret = GetString(key);
		return StringExtension.isNullOrEmpty(ret) ? def : ret;
	}

	public static boolean GetBool(String key, boolean def) {
		String ret = GetString(key);
		return StringExtension.isNullOrEmpty(ret) ? def : Boolean
				.parseBoolean(ret);
	}

	public static boolean GetBool(String key) {
		String ret = GetString(key);
		return StringExtension.isNullOrEmpty(ret) ? null : Boolean
				.parseBoolean(ret);
	}

	public static int GetInt(String key, int def) {
		String ret = GetString(key);
		return StringExtension.isNullOrEmpty(ret) ? def : Integer.parseInt(ret);
	}

	public static int GetInt(String key) {
		String ret = GetString(key);
		return StringExtension.isNullOrEmpty(ret) ? null : Integer
				.parseInt(ret);
	}

	public static Long GetLong(String key, Long def) {
		String ret = GetString(key);
		return StringExtension.isNullOrEmpty(ret) ? def : Long.parseLong(ret);
	}

	public static Long GetLong(String key) {
		String ret = GetString(key);
		return StringExtension.isNullOrEmpty(ret) ? null : Long.parseLong(ret);
	}

	public static Double GetDouble(String key, Double def) {
		String ret = GetString(key);
		return StringExtension.isNullOrEmpty(ret) ? def : Double
				.parseDouble(ret);
	}

	public static Double GetDouble(String key) {
		String ret = GetString(key);
		return StringExtension.isNullOrEmpty(ret) ? null : Double
				.parseDouble(ret);
	}

	public static Date GetDate(String key, Date def) throws ParseException {
		String ret = GetString(key);
		SimpleDateFormat formatter = new SimpleDateFormat(DATE_FORMAT);
		Date date = formatter.parse(ret);
		return StringExtension.isNullOrEmpty(ret) ? def : date;
	}
	public static Date GetDate(String key) throws ParseException {
		return GetDate(key, null);
	}
	
	//GL helpers
	public static TransactionType GetTransactionType(String key)
	{
		Long id = GetLong( key);
		TransactionTypeEndpoint tep = new TransactionTypeEndpoint();
		return tep.getTransactionType(id);
	}
	public static Account GetAccount(String key)
	{
		Long id = GetLong( key);
		AccountEndpoint tep = new AccountEndpoint();
		return tep.getAccount(id);
	}
	
}
