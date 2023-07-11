package com.sp.fanikiwa.pdf;

import java.util.logging.Level;
import java.util.logging.Logger;

import com.itextpdf.text.BaseColor;
import com.itextpdf.text.Chunk;
import com.itextpdf.text.Document;
import com.itextpdf.text.DocumentException;
import com.itextpdf.text.Element;
import com.itextpdf.text.Font;
import com.itextpdf.text.Font.FontFamily;
import com.itextpdf.text.PageSize;
import com.itextpdf.text.Paragraph;
import com.itextpdf.text.Phrase;
import com.itextpdf.text.Rectangle;
import com.itextpdf.text.pdf.PdfPCell;
import com.itextpdf.text.pdf.PdfPTable;
import com.sp.fanikiwa.entity.Contract;

public class ContractPDF {

	private static final Logger log = Logger.getLogger(ContractPDF.class
			.getName());
	Document document;
	Contract contract;

	// DEFINED fONTS
	Font timesromanbold = new Font(FontFamily.TIMES_ROMAN, 11, Font.BOLD,
			BaseColor.BLACK);
	Font timesromannormal = new Font(FontFamily.TIMES_ROMAN, 9, Font.NORMAL,
			BaseColor.BLACK);
	Font helveticabold = new Font(FontFamily.HELVETICA, 15, Font.BOLD,
			BaseColor.BLACK);
	Font helveticanormal = new Font(FontFamily.HELVETICA, 9, Font.NORMAL,
			BaseColor.BLACK);

	public ContractPDF(Document document) {
		super();
		this.document = document;
		Build();
	}

	public void Build() {
		try {

			document.addTitle("Fanikiwa");
			document.addSubject("Fanikiwa Loan Contract");
			document.addKeywords("Loan Contract");
			document.addAuthor("Fanikiwa");
			document.addCreator("Fanikiwa");

			AddDocHeaders();

			AddDocBody();

			AddDocFooter();

		} catch (DocumentException e) {
			// TODO Auto-generated catch block
			log.log(Level.SEVERE, e.getMessage(), e);
		} 
	}

	private void AddDocHeaders() throws DocumentException {
		PdfPTable table = new PdfPTable(3);
		table.setWidthPercentage(100);
		table.setSpacingBefore(2f);
		table.setSpacingAfter(2f);
		table.getDefaultCell().setBorder(0);

		PdfPCell headercell = new PdfPCell(new Phrase("P2P LOAN CONTRACT",
				helveticabold));
		headercell.setHorizontalAlignment(Element.ALIGN_CENTER);
		headercell.setBorder(0);
		headercell.setColspan(3);
		table.addCell(headercell);

		PdfPCell amountcell = new PdfPCell(new Phrase("KES "
				+ "_______________" + " (AMOUNT)", helveticanormal));
		amountcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		amountcell.setBorder(0);
		amountcell.setColspan(0);
		table.addCell(amountcell);

		PdfPCell datecell = new PdfPCell(new Phrase("________________"
				+ " (DATE)", helveticanormal));
		datecell.setHorizontalAlignment(Element.ALIGN_RIGHT);
		datecell.setBorder(0);
		datecell.setColspan(0);
		table.addCell(datecell);

		document.add(table);
	}

	private void AddDocBody() throws DocumentException {
		PdfPTable table = new PdfPTable(1);
		table.setWidthPercentage(100);
		table.setSpacingBefore(2f);
		table.setSpacingAfter(2f);
		table.getDefaultCell().setBorder(0);

		PdfPCell headercell = new PdfPCell(new Phrase("FOR VALUE RECEIVED,",
				timesromanbold));
		headercell.setHorizontalAlignment(Element.ALIGN_LEFT);
		headercell.setBorder(0);
		headercell.setColspan(0);
		table.addCell(headercell);

		PdfPCell promisecell = new PdfPCell(
				new Phrase(
						"the undersigned, (the 'Maker'), hereby promises to pay to the order of",
						helveticanormal));
		promisecell.setHorizontalAlignment(Element.ALIGN_LEFT);
		promisecell.setBorder(0);
		promisecell.setColspan(0);
		table.addCell(promisecell);

		PdfPCell lendernamecell = new PdfPCell(new Phrase(
				"__________________(LENDER NAME)", helveticanormal));
		lendernamecell.setHorizontalAlignment(Element.ALIGN_LEFT);
		lendernamecell.setBorder(0);
		lendernamecell.setColspan(0);
		table.addCell(lendernamecell);

		PdfPCell principalcell = new PdfPCell(new Phrase(
				"('Payee'), the principal sum of", helveticanormal));
		principalcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		principalcell.setBorder(0);
		principalcell.setColspan(0);
		table.addCell(principalcell);

		PdfPCell principalsumcell = new PdfPCell(new Phrase("KES ____________",
				helveticanormal));
		principalsumcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		principalsumcell.setBorder(0);
		principalsumcell.setColspan(0);
		table.addCell(principalsumcell);

		PdfPCell termsandconditionscell = new PdfPCell(new Phrase(
				"pursuant to the terms and conditions set forth herein.",
				helveticanormal));
		termsandconditionscell.setHorizontalAlignment(Element.ALIGN_LEFT);
		termsandconditionscell.setBorder(0);
		termsandconditionscell.setColspan(0);
		table.addCell(termsandconditionscell);

		PdfPCell paymentprincipalcell = new PdfPCell(new Phrase(
				"PAYMENT OF PRINCIPAL.", timesromanbold));
		paymentprincipalcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		paymentprincipalcell.setBorder(0);
		paymentprincipalcell.setColspan(0);
		table.addCell(paymentprincipalcell);

		PdfPCell accruedinterestcell = new PdfPCell(
				new Phrase(
						"The principal amount of this Contract and any accrued but unpaid interest shall be due and payable in",
						helveticanormal));
		accruedinterestcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		accruedinterestcell.setBorder(0);
		accruedinterestcell.setColspan(0);
		table.addCell(accruedinterestcell);

		PdfPCell numberofpaymentscell = new PdfPCell(new Phrase(
				"in ____________ (NUMBER OF PAYMENTS)", helveticanormal));
		numberofpaymentscell.setHorizontalAlignment(Element.ALIGN_LEFT);
		numberofpaymentscell.setBorder(0);
		numberofpaymentscell.setColspan(0);
		table.addCell(numberofpaymentscell);

		PdfPCell monthlyinstallmentscell = new PdfPCell(new Phrase(
				"equal monthly installments beginning", helveticanormal));
		monthlyinstallmentscell.setHorizontalAlignment(Element.ALIGN_LEFT);
		monthlyinstallmentscell.setBorder(0);
		monthlyinstallmentscell.setColspan(0);
		table.addCell(monthlyinstallmentscell);

		PdfPCell firstpaymentdatecell = new PdfPCell(
				new Phrase("___________________ (DATE OF FIRST PAYMENT).",
						helveticanormal));
		firstpaymentdatecell.setHorizontalAlignment(Element.ALIGN_LEFT);
		firstpaymentdatecell.setBorder(0);
		firstpaymentdatecell.setColspan(0);
		table.addCell(firstpaymentdatecell);

		PdfPCell paymentsundercontractcell = new PdfPCell(
				new Phrase(
						"All payments under this Contract shall be applied first to accrued but unpaid interest, and next to outstanding principal.  If not sooner paid, the entire remaining indebtedness (including accrued interest) shall be due and payable on",
						helveticanormal));
		paymentsundercontractcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		paymentsundercontractcell.setBorder(0);
		paymentsundercontractcell.setColspan(0);
		table.addCell(paymentsundercontractcell);

		PdfPCell dateoffinalpaymentcell = new PdfPCell(new Phrase(
				"_________________ (DATE OF FINAL PAYMENT).", helveticanormal));
		dateoffinalpaymentcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		dateoffinalpaymentcell.setBorder(0);
		dateoffinalpaymentcell.setColspan(0);
		table.addCell(dateoffinalpaymentcell);

		PdfPCell interestcell = new PdfPCell(new Phrase("INTEREST.",
				timesromanbold));
		interestcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		interestcell.setBorder(0);
		interestcell.setColspan(0);
		table.addCell(interestcell);

		PdfPCell compoundedannuallycell = new PdfPCell(new Phrase(
				"This Contract shall bear interest, compounded annually, at.",
				helveticanormal));
		compoundedannuallycell.setHorizontalAlignment(Element.ALIGN_LEFT);
		compoundedannuallycell.setBorder(0);
		compoundedannuallycell.setColspan(0);
		table.addCell(compoundedannuallycell);

		PdfPCell annualinterestratecell = new PdfPCell(new Phrase(
				"_________ (ANNUAL INTEREST RATE) percent.", helveticanormal));
		annualinterestratecell.setHorizontalAlignment(Element.ALIGN_LEFT);
		annualinterestratecell.setBorder(0);
		annualinterestratecell.setColspan(0);
		table.addCell(annualinterestratecell);

		PdfPCell prepaymentcell = new PdfPCell(new Phrase("PREPAYMENT.",
				timesromanbold));
		prepaymentcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		prepaymentcell.setBorder(0);
		prepaymentcell.setColspan(0);
		table.addCell(prepaymentcell);

		PdfPCell righttoprepaymentcell = new PdfPCell(
				new Phrase(
						"The Maker shall have the right at any time and from time to time to prepay this Contract in whole or in part without premium or penalty.",
						helveticanormal));
		righttoprepaymentcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		righttoprepaymentcell.setBorder(0);
		righttoprepaymentcell.setColspan(0);
		table.addCell(righttoprepaymentcell);

		PdfPCell remediescell = new PdfPCell(new Phrase("REMEDIES.",
				timesromanbold));
		remediescell.setHorizontalAlignment(Element.ALIGN_LEFT);
		remediescell.setBorder(0);
		remediescell.setColspan(0);
		table.addCell(remediescell);

		PdfPCell remediesofpayeecell = new PdfPCell(
				new Phrase(
						"No delay or omission on part of the holder of this Contract in exercising any right hereunder shall operate as a waiver of any such right or of any other right of such holder, nor shall any delay, omission or waiver on any one occasion be deemed a bar to or waiver of the same or any other right on any future occasion.  The rights and remedies of the Payee shall be cumulative and may be pursued singly, successively, or together, in the sole discretion of the Payee.",
						helveticanormal));
		remediesofpayeecell.setHorizontalAlignment(Element.ALIGN_LEFT);
		remediesofpayeecell.setBorder(0);
		remediesofpayeecell.setColspan(0);
		table.addCell(remediesofpayeecell);

		PdfPCell eventsofaccelerationcell = new PdfPCell(new Phrase(
				"EVENTS OF ACCELERATION.", timesromanbold));
		eventsofaccelerationcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		eventsofaccelerationcell.setBorder(0);
		eventsofaccelerationcell.setColspan(0);
		table.addCell(eventsofaccelerationcell);

		PdfPCell occurrenceofeventsofaccelerationcell = new PdfPCell(
				new Phrase(
						"The occurrence of any of the following shall constitute an 'Event of Acceleration' by Maker under this Contract:",
						helveticanormal));
		occurrenceofeventsofaccelerationcell
				.setHorizontalAlignment(Element.ALIGN_LEFT);
		occurrenceofeventsofaccelerationcell.setBorder(0);
		occurrenceofeventsofaccelerationcell.setColspan(0);
		table.addCell(occurrenceofeventsofaccelerationcell);

		PdfPCell failuretopayacell = new PdfPCell(
				new Phrase(
						"(a)	Maker’s failure to pay any part of the principal or interest as and when due under this Contract; or",
						helveticanormal));
		failuretopayacell.setHorizontalAlignment(Element.ALIGN_LEFT);
		failuretopayacell.setBorder(0);
		failuretopayacell.setColspan(0);
		table.addCell(failuretopayacell);

		PdfPCell failuretopaybcell = new PdfPCell(
				new Phrase(
						"(b)	Maker’s becoming insolvent or not paying its debts as they become due.",
						helveticanormal));
		failuretopaybcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		failuretopaybcell.setBorder(0);
		failuretopaybcell.setColspan(0);
		table.addCell(failuretopaybcell);

		PdfPCell accelerationcell = new PdfPCell(new Phrase("ACCELERATION.",
				timesromanbold));
		accelerationcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		accelerationcell.setBorder(0);
		accelerationcell.setColspan(0);
		table.addCell(accelerationcell);

		PdfPCell declarecontractcell = new PdfPCell(
				new Phrase(
						"Upon the occurrence of an Event of Acceleration under this Contract, and in addition to any other rights and remedies that Payee may have, Payee shall have the right, at its sole and exclusive option, to declare this Contract immediately due and payable.",
						helveticanormal));
		declarecontractcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		declarecontractcell.setBorder(0);
		declarecontractcell.setColspan(0);
		table.addCell(declarecontractcell);

		PdfPCell subordinationcell = new PdfPCell(new Phrase("SUBORDINATION.",
				timesromanbold));
		subordinationcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		subordinationcell.setBorder(0);
		subordinationcell.setColspan(0);
		table.addCell(subordinationcell);

		PdfPCell obligationscell = new PdfPCell(
				new Phrase(
						"The Maker’s obligations under this Contract are subordinated to all indebtedness, if any, of Maker, to any unrelated third party lender to the extent such indebtedness is outstanding on the date of this Contract and such subordination is required under the loan documents providing for such indebtedness.",
						helveticanormal));
		obligationscell.setHorizontalAlignment(Element.ALIGN_LEFT);
		obligationscell.setBorder(0);
		obligationscell.setColspan(0);
		table.addCell(obligationscell);

		PdfPCell waiversbymakercell = new PdfPCell(new Phrase(
				"WAIVERS BY MAKER.", timesromanbold));
		waiversbymakercell.setHorizontalAlignment(Element.ALIGN_LEFT);
		waiversbymakercell.setBorder(0);
		waiversbymakercell.setColspan(0);
		table.addCell(waiversbymakercell);

		PdfPCell partiestothiscontractcell = new PdfPCell(
				new Phrase(
						"All parties to this Contract including Maker and any sureties, endorsers, and guarantors hereby waive protest, presentment, notice of dishonor, and notice of acceleration of maturity and agree to continue to remain bound for the payment of principal, interest and all other sums due under this Contract notwithstanding any change or changes by way of release, surrender, exchange, modification or substitution of any security for this Contract or by way of any extension or extensions of time for the payment of principal and interest; and all such parties waive all and every kind of notice of such change or changes and agree that the same may be made without notice or consent of any of them.",
						helveticanormal));
		partiestothiscontractcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		partiestothiscontractcell.setBorder(0);
		partiestothiscontractcell.setColspan(0);
		table.addCell(partiestothiscontractcell);

		PdfPCell expensescell = new PdfPCell(new Phrase("EXPENSES.",
				timesromanbold));
		expensescell.setHorizontalAlignment(Element.ALIGN_LEFT);
		expensescell.setBorder(0);
		expensescell.setColspan(0);
		table.addCell(expensescell);

		PdfPCell paymentunderthiscontractcell = new PdfPCell(
				new Phrase(
						"In the event any payment under this Contract is not paid when due, the Maker agrees to pay, in addition to the principal and interest hereunder, reasonable attorneys’ fees not exceeding a sum equal to 15% of the then outstanding balance owing on the Contract, plus all other reasonable expenses incurred by Payee in exercising any of its rights and remedies upon default.",
						helveticanormal));
		paymentunderthiscontractcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		paymentunderthiscontractcell.setBorder(0);
		paymentunderthiscontractcell.setColspan(0);
		table.addCell(paymentunderthiscontractcell);

		PdfPCell governinglawcell = new PdfPCell(new Phrase("GOVERNING LAW.",
				timesromanbold));
		governinglawcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		governinglawcell.setBorder(0);
		governinglawcell.setColspan(0);
		table.addCell(governinglawcell);

		PdfPCell contractgoverninglawcell = new PdfPCell(
				new Phrase(
						"This Contract shall be governed by, and construed in accordance with, the laws of Kenya.",
						helveticanormal));
		contractgoverninglawcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		contractgoverninglawcell.setBorder(0);
		contractgoverninglawcell.setColspan(0);
		table.addCell(contractgoverninglawcell);

		PdfPCell successorscell = new PdfPCell(new Phrase("SUCCESSORS.",
				timesromanbold));
		successorscell.setHorizontalAlignment(Element.ALIGN_LEFT);
		successorscell.setBorder(0);
		successorscell.setColspan(0);
		table.addCell(successorscell);

		PdfPCell foregoingpromisecell = new PdfPCell(
				new Phrase(
						"All of the foregoing is the promise of Maker and shall bind Maker and Maker’s successors, heirs and assigns; provided, however, that Maker may not assign any of its rights or delegate any of its obligations hereunder without the prior written consent of the holder of this Contract.",
						helveticanormal));
		foregoingpromisecell.setHorizontalAlignment(Element.ALIGN_LEFT);
		foregoingpromisecell.setBorder(0);
		foregoingpromisecell.setColspan(0);
		table.addCell(foregoingpromisecell);

		PdfPCell inwitnesswhereofcell = new PdfPCell(new Phrase(
				"IN WITNESS WHEREOF,", timesromanbold));
		inwitnesswhereofcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		inwitnesswhereofcell.setBorder(0);
		inwitnesswhereofcell.setColspan(0);
		table.addCell(inwitnesswhereofcell);

		PdfPCell executescontractcell = new PdfPCell(
				new Phrase(
						"Maker has executed this Contract as of the day and year first above written.",
						helveticanormal));
		executescontractcell.setHorizontalAlignment(Element.ALIGN_LEFT);
		executescontractcell.setBorder(0);
		executescontractcell.setColspan(0);
		table.addCell(executescontractcell);

		PdfPCell makersignaturecell = new PdfPCell(new Phrase(
				"Maker:_____________________________(Signature)",
				timesromanbold));
		makersignaturecell.setHorizontalAlignment(Element.ALIGN_RIGHT);
		makersignaturecell.setBorder(0);
		makersignaturecell.setColspan(0);
		table.addCell(makersignaturecell);

		PdfPCell borrowernamecell = new PdfPCell(
				new Phrase("_____________________________(BORROWER NAME)",
						helveticanormal));
		borrowernamecell.setHorizontalAlignment(Element.ALIGN_RIGHT);
		borrowernamecell.setBorder(0);
		borrowernamecell.setColspan(0);
		table.addCell(borrowernamecell);

		document.add(table);
	}

	private void AddDocFooter() throws DocumentException {

		document.newPage();

		PdfPTable table = new PdfPTable(2);
		table.setWidthPercentage(100);
		table.setSpacingBefore(2f);
		table.setSpacingAfter(2f);
		table.getDefaultCell().setBorder(0);

		PdfPCell repaymentschedulecell = new PdfPCell(new Phrase(
				"Repayment schedule", helveticabold));
		repaymentschedulecell.setHorizontalAlignment(Element.ALIGN_CENTER);
		repaymentschedulecell.setBorder(0);
		repaymentschedulecell.setColspan(2);
		table.addCell(repaymentschedulecell);

		PdfPCell cell1 = new PdfPCell(new Phrase("Repayment schedule1"));
		cell1.setHorizontalAlignment(Element.ALIGN_CENTER);
		cell1.setBorder(0);
		cell1.setColspan(0);
		table.addCell(cell1);

		document.add(table);
	}

}
