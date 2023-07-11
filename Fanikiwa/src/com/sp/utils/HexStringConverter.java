package com.sp.utils;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class HexStringConverter {
	private static HexStringConverter hexStringConverter = null;

	public static HexStringConverter getInstance() {
		if (hexStringConverter == null)
			hexStringConverter = new HexStringConverter();
		return hexStringConverter;
	}

	private HexStringConverter() {
	}

	public String encodeHex(String str) {
		StringBuffer buf = new StringBuffer();
		for (int i = 0; i < str.length(); i++) {
			String tmp = Integer.toHexString(str.charAt(i));
			if (tmp.length() == 1)
				buf.append("0x0" + tmp);
			else
				buf.append("0x" + tmp);
		}
		return buf.toString();
	}

	public String decodeHex(String hexString) {
		Pattern p = Pattern.compile("(0x([a-fA-F0-9]{2}([a-fA-F0-9]{2})?))");
		Matcher m = p.matcher(hexString);
		StringBuffer buf = new StringBuffer();
		int hashCode = 0;
		while (m.find()) {
			hashCode = Integer.decode("0x" + m.group(2));
			m.appendReplacement(buf, new String(Character.toChars(hashCode)));
		}
		m.appendTail(buf);
		return buf.toString();
	}
}