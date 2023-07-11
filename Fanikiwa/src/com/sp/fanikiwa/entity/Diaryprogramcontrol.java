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
public class Diaryprogramcontrol {

	@Id
	Long id;
 
	private Date lastRun;
 
	private Date nextRun;

	public Diaryprogramcontrol() {
	}

	public Long getId() {
		return this.id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public Date getLastRun() {
		return this.lastRun;
	}

	public void setLastRun(Date lastRun) {
		this.lastRun = lastRun;
	}

	public Date getNextRun() {
		return this.nextRun;
	}

	public void setNextRun(Date nextRun) {
		this.nextRun = nextRun;
	}

}