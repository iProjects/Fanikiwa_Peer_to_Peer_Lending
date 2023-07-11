package com.sp.fanikiwa.entity;

import java.util.Date;

import javax.jdo.annotations.IdGeneratorStrategy;
import javax.jdo.annotations.IdentityType;
import javax.jdo.annotations.PersistenceCapable;
import javax.jdo.annotations.Persistent;
import javax.jdo.annotations.PrimaryKey;

import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;

@Entity
public class Mailinggroup {

	@Id
	Long groupId;
 
	private Date createdOn;
 
	private int creator;
 
	private Date lastModified;
 
	private int parentGroupId;
 
	private String shortCode;

	public Mailinggroup() {
	}

	public Long getGroupId() {
		return this.groupId;
	}

	public void setGroupId(Long groupId) {
		this.groupId = groupId;
	}

	public Date getCreatedOn() {
		return this.createdOn;
	}

	public void setCreatedOn(Date createdOn) {
		this.createdOn = createdOn;
	}

	public int getCreator() {
		return this.creator;
	}

	public void setCreator(int creator) {
		this.creator = creator;
	}

	public Date getLastModified() {
		return this.lastModified;
	}

	public void setLastModified(Date lastModified) {
		this.lastModified = lastModified;
	}

	public int getParentGroupId() {
		return this.parentGroupId;
	}

	public void setParentGroupId(int parentGroupId) {
		this.parentGroupId = parentGroupId;
	}

	public String getShortCode() {
		return this.shortCode;
	}

	public void setShortCode(String shortCode) {
		this.shortCode = shortCode;
	}

}