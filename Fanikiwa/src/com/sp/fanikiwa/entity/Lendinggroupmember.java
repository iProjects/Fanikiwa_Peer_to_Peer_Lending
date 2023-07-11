package com.sp.fanikiwa.entity;

import com.googlecode.objectify.Ref;
import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;
import com.sp.fanikiwa.Enums.LendingGroupMemberTypes;

@Entity
public class Lendinggroupmember {

	@Id
	Long id;
	
	@Index String groupName; //parent
 
	
	@Index private LendingGroupMemberTypes idType; //Email | Telno | Member Id
	private String name;

 

	public Lendinggroupmember() {
	}

	public Long getId() {
		return this.id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public LendingGroupMemberTypes getIdType() {
		return idType;
	}

	public void setIdType(LendingGroupMemberTypes idType) {
		this.idType = idType;
	}

	public String getGroupName() {
		return this.groupName;
	}

	public void setGroupName(String groupName) {
		this.groupName = groupName;
	}

}