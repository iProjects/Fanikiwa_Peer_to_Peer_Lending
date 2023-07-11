package com.sp.fanikiwa;

import java.io.IOException;
import java.text.ParseException;
import java.util.Date;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.servlet.RequestDispatcher;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.sp.fanikiwa.api.UserprofileEndpoint;
import com.sp.fanikiwa.entity.ActivationDTO;
import com.sp.fanikiwa.entity.RequestResult;


public class ActivationServlet  extends HttpServlet {

	private static final Logger log = Logger.getLogger(ActivationServlet.class
			.getName());

	

	private static final long serialVersionUID = 1L;

	public void doGet(HttpServletRequest req, HttpServletResponse resp)
			throws IOException {
		try {
			Activate(req, resp);
		} catch (ParseException e) {
			log.log(Level.SEVERE,e.getMessage(),e);
		} catch(Exception e){
			log.log(Level.SEVERE,e.getMessage(),e);
		}
	}

	public void doPost(HttpServletRequest req, HttpServletResponse resp)
			throws IOException {
		try {
			Activate(req, resp);
		} catch (ParseException e) {
			log.log(Level.SEVERE,e.getMessage(),e);
		} catch(Exception e){
			log.log(Level.SEVERE,e.getMessage(),e);
		}
	}

	private void Activate(HttpServletRequest request,
			HttpServletResponse response) throws ParseException, IOException {

		try {
			//1. Get message
			ActivationDTO actDTO = new ActivationDTO();
			actDTO.setActivationMethod(request.getParameter("method"));
			actDTO.setActivatedDate(new Date());
			actDTO.setEmail(request.getParameter("email") ); //take care it might be null
			actDTO.setToken(request.getParameter("token"));
			
			UserprofileEndpoint uep = new UserprofileEndpoint();
			RequestResult re = uep.activate(actDTO);
			
			if(re.isSuccess())
			{
				response.sendRedirect("/Views/Account/Login.html");
//				RequestDispatcher rd = request.getRequestDispatcher("/");
//				rd.forward(request,response);
			}
			else
			{
				response.getWriter().println("Activation not successful: Details =" + re.getResultMessage());
			}

		} catch (Exception e) {
			log.log(Level.SEVERE,e.getMessage(),e);
			response.getWriter().println("An error occured during activation: Details =" + e.getMessage() );
		}
	}

}
