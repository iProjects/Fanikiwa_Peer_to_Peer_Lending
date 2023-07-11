package com.sp.fanikiwa.entity;

import java.util.Date;

import com.googlecode.objectify.Ref;
import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;
import com.googlecode.objectify.annotation.Load;

@Entity
public class Member {

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
	@Load
	Ref<Account> investmentAccount;

	@Load
	Ref<Account> loanAccount;

	@Load
	Ref<Account> currentAccount;

	@Load
	Ref<Account> interestIncAccount;

	@Load
	Ref<Account> interestExpAccount;

	@Load
	Ref<Customer> customer;

	public Member() {
	}

	public Long getMemberId() {
		return memberId;
	}

	public void setMemberId(Long memberId) {
		this.memberId = memberId;
	}

	public Date getDateActivated() {
		return dateActivated;
	}

	public void setDateActivated(Date dateActivated) {
		this.dateActivated = dateActivated;
	}

	public Date getDateJoined() {
		return dateJoined;
	}

	public void setDateJoined(Date dateJoined) {
		this.dateJoined = dateJoined;
	}

	public Date getDateOfBirth() {
		return dateOfBirth;
	}

	public void setDateOfBirth(Date dateOfBirth) {
		this.dateOfBirth = dateOfBirth;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getGender() {
		return gender;
	}

	public void setGender(String gender) {
		this.gender = gender;
	}

	public String getInformBy() {
		return informBy;
	}

	public void setInformBy(String informBy) {
		this.informBy = informBy;
	}

	public int getMaxRecordsToDisplay() {
		return maxRecordsToDisplay;
	}

	public void setMaxRecordsToDisplay(int maxRecordsToDisplay) {
		this.maxRecordsToDisplay = maxRecordsToDisplay;
	}

	public String getNationalID() {
		return nationalID;
	}

	public void setNationalID(String nationalID) {
		this.nationalID = nationalID;
	}

	public String getOtherNames() {
		return otherNames;
	}

	public void setOtherNames(String otherNames) {
		this.otherNames = otherNames;
	}

	public String getPhoto() {
		return photo;
	}

	public void setPhoto(String photo) {
		this.photo = photo;
	}

	public String getPwd() {
		return pwd;
	}

	public void setPwd(String pwd) {
		this.pwd = pwd;
	}

	public int getRefferedBy() {
		return refferedBy;
	}

	public void setRefferedBy(int refferedBy) {
		this.refferedBy = refferedBy;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public String getSurname() {
		return surname;
	}

	public void setSurname(String surname) {
		this.surname = surname;
	}

	public String getTelephone() {
		return telephone;
	}

	public void setTelephone(String telephone) {
		this.telephone = telephone;
	}

	public Account getInvestmentAccount() {
		return this.investmentAccount.get();
	}

	public void setInvestmentAccount(Account investmentAccount) {
		this.investmentAccount = Ref.create(investmentAccount);
	}

	public Account getLoanAccount() {
		return this.loanAccount.get();
	}

	public void setLoanAccount(Account loanAccount) {
		this.loanAccount = Ref.create(loanAccount);
	}

	public Account getCurrentAccount() {
		return this.currentAccount.get();
	}

	public void setCurrentAccount(Account currentAccount) {
		this.currentAccount = Ref.create(currentAccount);
	}

	public Account getInterestExpAccount() {
		return this.interestExpAccount.get();
	}

	public void setInterestExpAccount(Account interestExpAccount) {
		this.interestExpAccount = Ref.create(interestExpAccount);
	}

	public Account getInterestIncAccount() {
		return this.interestIncAccount.get();
	}

	public void setInterestIncAccount(Account interestIncAccount) {
		this.interestIncAccount = Ref.create(interestIncAccount);
	}

	public Customer getCustomer() {
		return this.customer.get();
	}

	public void setCustomer(Customer customer) {
		this.customer = Ref.create(customer);
	}

}