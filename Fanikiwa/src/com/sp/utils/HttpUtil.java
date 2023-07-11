package com.sp.utils;

import java.util.Map;

import javax.servlet.ServletRequest;

import org.json.simple.JSONObject;

public class HttpUtil {
	public static JSONObject requestParamsToJSON(ServletRequest req) {
		  JSONObject jsonObj = new JSONObject();
		  Map<String,String[]> params = req.getParameterMap();
		  for (Map.Entry<String,String[]> entry : params.entrySet()) {
		    String v[] = entry.getValue();
		    Object o = (v.length == 1) ? v[0] : v;
		    jsonObj.put(entry.getKey(), o);
		  }
		  return jsonObj;
		}
}
