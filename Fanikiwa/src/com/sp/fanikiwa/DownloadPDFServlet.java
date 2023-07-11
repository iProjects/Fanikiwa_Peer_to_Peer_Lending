package com.sp.fanikiwa;

import java.io.IOException;

import javax.servlet.ServletOutputStream;
import javax.servlet.http.*;

import com.itextpdf.text.Document;
import com.itextpdf.text.DocumentException;
import com.itextpdf.text.Paragraph;
import com.itextpdf.text.pdf.PdfWriter;

public class DownloadPDFServlet extends HttpServlet {
	public void doGet(HttpServletRequest req, HttpServletResponse res)
			throws IOException {
		String fileName = "<SomeFileName>.pdf";
		res.setContentType("application/pdf");
		res.setHeader("Content-Disposition", "attachment;filename=\""
				+ fileName + "\"");
		try {
			writePDF("Contract", res.getOutputStream());
		} catch (DocumentException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	private void writePDF(String symbol, ServletOutputStream str) throws DocumentException
	{
		switch(symbol)
		{
		case "Contract":
			break;
		}
		
		
	}
	
	private Document CreateContract(ServletOutputStream str) throws DocumentException
	{
		Document document = new Document();
		   PdfWriter.getInstance(document, str);
		   document.open();
		   document.add(new Paragraph("This is a contract"));
		   document.close();
		   return document;
	}
}
