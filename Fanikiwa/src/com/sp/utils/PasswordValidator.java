package com.sp.utils;

import java.util.regex.Matcher;
import java.util.regex.Pattern;
 
public class PasswordValidator{
 
	  private static Pattern pattern;
	  private static Matcher matcher;
 
	  /*
	   * (			# Start of group
  (?=.*\d)		#   must contains one digit from 0-9
  (?=.*[a-z])		#   must contains one lowercase characters
  (?=.*[A-Z])		#   must contains one uppercase characters
  (?=.*[@#$%])		#   must contains one special symbols in the list "@#$%"
              .		#     match anything with previous condition checking
                {6,20}	#        length at least 6 characters and maximum of 20	
)			# End of group
	   * */
	  private static final String PASSWORD_PATTERN = 
              "((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$%]).{6,20})";

	  /**
	   * Validate password with regular expression
	   * @param password password for validation
	   * @return true valid password, false invalid password
	   */
	  public static boolean validate(final String password){
 
		  pattern = Pattern.compile(PASSWORD_PATTERN);
		  matcher = pattern.matcher(password);
		  return matcher.matches();
 
	  }
}