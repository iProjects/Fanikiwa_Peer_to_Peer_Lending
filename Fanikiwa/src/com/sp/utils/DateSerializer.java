package com.sp.utils;

import java.lang.reflect.Type;
import java.text.SimpleDateFormat;
import java.util.Date;

import com.google.gson.JsonElement;
import com.google.gson.JsonPrimitive;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;

public class DateSerializer implements JsonSerializer {
	 
	  public JsonElement serialize(Date date, Type typeOfSrc, JsonSerializationContext context) {
		SimpleDateFormat sdf = new SimpleDateFormat("dd-MMM-yyyy");  
	    return new JsonPrimitive(sdf.format(date));
	  }

	@Override
	public JsonElement serialize(Object arg0, Type arg1,
			JsonSerializationContext arg2) {
		// TODO Auto-generated method stub
		return null;
	}

	 
 
}  