package com.sp.fanikiwa.entity;

import java.util.Date;

import com.googlecode.objectify.Ref;
import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;
import com.googlecode.objectify.annotation.Load;

@Entity
public class MemberDTO {

	@Id
	Long memberId;

	private Date dateActivated;

	private Date dateJoined;

	private Date dateOfBirth;
	@Index
	private String email;

	private String gender;

	private String informBy;

	private int maxRecordsToDisplay;
	@Index
	private String nationalID;

	private String otherNames;

	private String photo;
	@Index
	private String pwd;

	private int refferedBy;
	@Index
	private String status;

	private String surname;
	@Index
	private String telephone;

	// FK
	Long investmentAccount;

	Long loanAccount;

	Long currentAccount;

	Long interestIncAccount;

	Long interestExpAccount;

	Long customer;

	public MemberDTO() {
	}

	public Long getMemberId() {
		return this.memberId;
	}

	public void setMemberId(Long memberId) {
		this.memberId = memberId;
	}

	public Date getDateActivated() {
		return this.dateActivated;
	}

	public void setDateActivated(Date dateActivated) {
		this.dateActivated = dateActivated;
	}

	public Date getDateJoined() {
		return this.dateJoined;
	}

	public void setDateJoined(Date dateJoined) {
		this.dateJoined = dateJoined;
	}

	public Date getDateOfBirth() {
		return this.dateOfBirth;
	}

	public void setDateOfBirth(Date dateOfBirth) {
		this.dateOfBirth = dateOfBirth;
	}

	public String getEmail() {
		return this.email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getGender() {
		return this.gender;
	}

	public void setGender(String gender) {
		this.gender = gender;
	}

	public String getInformBy() {
		return this.informBy;
	}

	public void setInformBy(String informBy) {
		this.informBy = informBy;
	}

	public int getMaxRecordsToDisplay() {
		return this.maxRecordsToDisplay;
	}

	public void setMaxRecordsToDisplay(int maxRecordsToDisplay) {
		this.maxRecordsToDisplay = maxRecordsToDisplay;
	}

	public String getNationalID() {
		return this.nationalID;
	}

	public void setNationalID(String nationalID) {
		this.nationalID = nationalID;
	}

	public String getOtherNames() {
		return this.otherNames;
	}

	public void setOtherNames(String otherNames) {
		this.otherNames = otherNames;
	}

	public String getPhoto() {
		return this.photo;
	}

	public void setPhoto(String photo) {
		this.photo = photo;
	}

	public String getPwd() {
		return this.pwd;
	}

	public void setPwd(String pwd) {
		this.pwd = pwd;
	}

	public int getRefferedBy() {
		return this.refferedBy;
	}

	public void setRefferedBy(int refferedBy) {
		this.refferedBy = refferedBy;
	}

	public String getStatus() {
		return this.status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public String getSurname() {
		return this.surname;
	}

	public void setSurname(String surname) {
		this.surname = surname;
	}

	public String getTelephone() {
		return this.telephone;
	}

	public void setTelephone(String telephone) {
		this.telephone = telephone;
	}

	public Long getInvestmentAccount() {
		return investmentAccount;
	}

	public void setInvestmentAccount(Long investmentAccount) {
		this.investmentAccount = investmentAccount;
	}

	public Long getLoanAccount() {
		return loanAccount;
	}

	public void setLoanAccount(Long loanAccount) {
		this.loanAccount = loanAccount;
	}

	public Long getCurrentAccount() {
		return currentAccount;
	}

	public void setCurrentAccount(Long currentAccount) {
		this.currentAccount = currentAccount;
	}

	public Long getInterestIncAccount() {
		return interestIncAccount;
	}

	public void setInterestIncAccount(Long interestIncAccount) {
		this.interestIncAccount = interestIncAccount;
	}

	public Long getInterestExpAccount() {
		return interestExpAccount;
	}

	public void setInterestExpAccount(Long interestExpAccount) {
		this.interestExpAccount = interestExpAccount;
	}

	public Long getCustomer() {
		return customer;
	}

	public void setCustomer(Long customer) {
		this.customer = customer;
	}
 
}