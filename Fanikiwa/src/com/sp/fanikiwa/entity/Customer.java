package com.sp.fanikiwa.entity;

import java.util.Date;

import com.googlecode.objectify.Ref;
import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;
import com.googlecode.objectify.annotation.Load;

@Entity
public class Customer {

	@Id
	Long customerId;
 
	private String address;
 
	private String billToAddress;
 
	private String billToEmail;
 
	private String billToName;
 
	private String billToTelephone;
 
	private String branch;
 
	private Date createdDate;
 
	private String customerNo;
 
	private String email;
 
	private Long memberId;
 
	private String name;
 
	private String telephone;
 
	@Load
	Ref<Organization> organization;

	public Customer() {
	}

	public Long getCustomerId() {
		return this.customerId;
	}

	public void setCustomerId(Long customerId) {
		this.customerId = customerId;
	}

	public String getAddress() {
		return this.address;
	}

	public void setAddress(String address) {
		this.address = address;
	}

	public String getBillToAddress() {
		return this.billToAddress;
	}

	public void setBillToAddress(String billToAddress) {
		this.billToAddress = billToAddress;
	}

	public String getBillToEmail() {
		return this.billToEmail;
	}

	public void setBillToEmail(String billToEmail) {
		this.billToEmail = billToEmail;
	}

	public String getBillToName() {
		return this.billToName;
	}

	public void setBillToName(String billToName) {
		this.billToName = billToName;
	}

	public String getBillToTelephone() {
		return this.billToTelephone;
	}

	public void setBillToTelephone(String billToTelephone) {
		this.billToTelephone = billToTelephone;
	}

	public String getBranch() {
		return this.branch;
	}

	public void setBranch(String branch) {
		this.branch = branch;
	}

	public Date getCreatedDate() {
		return this.createdDate;
	}

	public void setCreatedDate(Date createdDate) {
		this.createdDate = createdDate;
	}

	public String getCustomerNo() {
		return this.customerNo;
	}

	public void setCustomerNo(String customerNo) {
		this.customerNo = customerNo;
	}

	public String getEmail() {
		return this.email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public Long getMemberId() {
		return this.memberId;
	}

	public void setMemberId(Long memberId) {
		this.memberId = memberId;
	}

	public String getName() {
		return this.name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public Organization getOrganization() {
		return this.organization.get();
	}

	public void setOrganization(Organization organization) {
		this.organization = Ref.create(organization);
	}

	public String getTelephone() {
		return this.telephone;
	}

	public void setTelephone(String telephone) {
		this.telephone = telephone;
	}

}