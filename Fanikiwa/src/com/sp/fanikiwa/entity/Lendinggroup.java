package com.sp.fanikiwa.entity;

import java.util.Date;

import javax.jdo.annotations.IdGeneratorStrategy;
import javax.jdo.annotations.IdentityType;
import javax.jdo.annotations.PersistenceCapable;
import javax.jdo.annotations.Persistent;
import javax.jdo.annotations.PrimaryKey;

import com.googlecode.objectify.Ref;
import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;

@Entity
public class Lendinggroup {

	@Id
	String groupName;

	private Date createdOn;

	@Index
	private Ref<Member> creator;

	private Date lastModified;

	@Index	private String parentGroup;

	public Lendinggroup() {
	}

	public String getGroupName() {
		return this.groupName;
	}

	public void setGroupName(String groupname) {
		this.groupName = groupname;
	}

	public Date getCreatedOn() {
		return this.createdOn;
	}

	public void setCreatedOn(Date createdOn) {
		this.createdOn = createdOn;
	}

	public Member getCreator() {
		return this.creator.get();
	}

	public void setCreator(Member creator) {
		this.creator = Ref.create(creator);
	}

	public Date getLastModified() {
		return this.lastModified;
	}

	public void setLastModified(Date lastModified) {
		this.lastModified = lastModified;
	}

	public String getParentGroup() {
		return this.parentGroup;
	}

	public void setParentGroup(String parentGroup) {
		this.parentGroup = parentGroup;
	}

}