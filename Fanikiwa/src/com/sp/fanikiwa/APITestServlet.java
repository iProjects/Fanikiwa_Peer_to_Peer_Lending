package com.sp.fanikiwa;

import java.io.IOException;
import java.util.Collection;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.sp.fanikiwa.api.OfferEndpoint;
import com.sp.fanikiwa.entity.Offer;

public class APITestServlet extends HttpServlet {

	public void doGet(HttpServletRequest req, HttpServletResponse resp)
			throws IOException {
		
		Test( req,  resp);
	}
	public void doPost(HttpServletRequest req, HttpServletResponse resp)
			throws IOException {
		Test( req,  resp);
	}
	private void Test(HttpServletRequest req, HttpServletResponse resp) throws IOException {
		// TODO Auto-generated method stub
		OfferEndpoint oep = new OfferEndpoint();
		Collection<Offer> offers = oep.listOffer(null, 5).getItems();
		resp.setContentType("text/plain");
		
		for(Offer o : offers)
		{
			resp.getWriter().println(o.getId() + ", " + o.getDescription() + ", " + o.getAmount() + "\n");
		}
	}
}
