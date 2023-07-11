package com.sp.fanikiwa.entity;

import java.util.Date;

import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;

@Entity
public class Contact {

	@Id
	Long contactId;
 
	private Date dateActivated;
 
	private Date dateJoined;
 
	private Date dateOfBirth;
 
	private String email;
 
	private String gender;
 
	private String informBy;
 
	private String nationalID;
 
	private String otherNames;
 
	private String photo;
 
	private String pwd;
 
	private int refferedBy;
 
	private String status;
 
	private String surname;
 
	private String telephone;

	public Contact() {
	}

	public Long getContactId() {
		return this.contactId;
	}

	public void setContactId(Long contactId) {
		this.contactId = contactId;
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

}