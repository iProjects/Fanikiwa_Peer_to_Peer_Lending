package com.sp.utils;

import java.util.List;
import java.util.Map;

import com.fasterxml.jackson.databind.ObjectMapper;

public  class StringExtension {
	public static boolean isNullOrEmpty(String s) {
	    return s == null || s.length() == 0;
	}
	
	public static String format(String format, Object ... inputs) {
		ObjectMapper m = new ObjectMapper();
		Map<String,Object> props = m.convertValue(inputs[0], Map.class);

		StringTemplate t1 = new StringTemplate(format);
        t1.setBlankNull();
		return t1.substitute( props);
		
    }
	

	//parses objects from string by calling the object's constructor
	public static <T> T parseObjectFromString(String s, Class<T> clazz) throws Exception {
	    return clazz.getConstructor(new Class[] {String.class }).newInstance(s);
	}
	
	public static boolean isAlphaNumeric(String s){
	    String pattern= "^[a-zA-Z0-9]*$";
	        if(s.matches(pattern)){
	            return true;
	        }
	        return false;   
	}
	public static boolean isKenyaMobileNumber(String s)
	{
		return s.startsWith("0")  || s.startsWith("+254"); 
	}
	public static boolean isNumeric(String str)
	{
	  return str.matches("-?\\d+(\\.\\d+)?");  //match a number with optional '-' and decimal.
	}
	
	static public String join(List<String> list, String conjunction)
	{
	   StringBuilder sb = new StringBuilder();
	   boolean first = true;
	   for (String item : list)
	   {
	      if (first)
	         first = false;
	      else
	         sb.append(conjunction);
	      sb.append(item);
	   }
	   return sb.toString();
	}
}
