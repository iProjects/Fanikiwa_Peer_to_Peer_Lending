package com.sp.fanikiwa.entity;

import javax.jdo.annotations.IdGeneratorStrategy;
import javax.jdo.annotations.IdentityType;
import javax.jdo.annotations.PersistenceCapable;
import javax.jdo.annotations.Persistent;
import javax.jdo.annotations.PrimaryKey;

import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;

@Entity
public class Offergroupemailaddress {

	@Id
	Long id;
 
	private String emailAddress;
 
	private int mailingGroupId;

	public Offergroupemailaddress() {
	}

	public Long getId() {
		return this.id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public String getEmailAddress() {
		return this.emailAddress;
	}

	public void setEmailAddress(String emailAddress) {
		this.emailAddress = emailAddress;
	}

	public int getMailingGroupId() {
		return this.mailingGroupId;
	}

	public void setMailingGroupId(int mailingGroupId) {
		this.mailingGroupId = mailingGroupId;
	}

}