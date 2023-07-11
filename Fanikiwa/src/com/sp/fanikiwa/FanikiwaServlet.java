package com.sp.fanikiwa;

import java.io.IOException;

import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.*;

@SuppressWarnings("serial")
public class FanikiwaServlet extends HttpServlet {
	
	public void doGet(HttpServletRequest req, HttpServletResponse resp)
			throws IOException {
		resp.setContentType("text/plain");
		resp.getWriter().println("Hello, world");
	}
	 
	    public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain) throws IOException, ServletException {
	        HttpServletRequest req=(HttpServletRequest)request;
	//check if "role" attribute is null
	        if(req.getSession().getAttribute("role") == null){
	//forward request to login 
	            req.getRequestDispatcher("/Views/Account/Login.html").forward(request, response);
	        }else{
	        chain.doFilter(request, response);
	        }
	    }

	      
}
