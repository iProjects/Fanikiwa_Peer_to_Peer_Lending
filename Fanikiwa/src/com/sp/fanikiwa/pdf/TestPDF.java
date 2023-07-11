package com.sp.fanikiwa.pdf;

import java.util.logging.Level;
import java.util.logging.Logger;

import com.itextpdf.text.Chunk;
import com.itextpdf.text.Document;
import com.itextpdf.text.DocumentException;
import com.itextpdf.text.Paragraph;
import com.sp.fanikiwa.MailHandlerServlet;

public class TestPDF {
	private static final Logger log = Logger.getLogger(TestPDF.class
			.getName());
	
	Document document;
	Object context;

	public TestPDF(Document document) {
		super();
		this.document = document;
		Build( );
	}
	
	private void Build() 
    {
    	 try {
        document.addTitle("Fanikiwa");
        document.addSubject("Fanikiwa Loan Contract");
        document.addKeywords("Loan,Contract");
        document.addAuthor("Fanikiwa");
        document.addCreator("Fanikiwa");
        
        Paragraph paragraph = new Paragraph();
        paragraph.add(new Chunk("Your loan contract with XXX for amount YYY !"));
        document.add(paragraph);
		} catch (DocumentException e) {
			// TODO Auto-generated catch block
			log.log(Level.SEVERE,e.getMessage(),e);
		}
    }
}
