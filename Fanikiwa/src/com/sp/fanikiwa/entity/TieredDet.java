package com.sp.fanikiwa.entity;

import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;

@Entity
public class TieredDet {

	@Id
	Long id;

	private boolean absolute;

	private double max;

	private double min;

	private double rate;

	@Index
	private long tieredID;

	public TieredDet() {
	}

	public Long getId() {
		return this.id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public boolean getAbsolute() {
		return this.absolute;
	}

	public void setAbsolute(boolean absolute) {
		this.absolute = absolute;
	}

	public double getMax() {
		return this.max;
	}

	public void setMax(double max) {
		this.max = max;
	}

	public double getMin() {
		return this.min;
	}

	public void setMin(double min) {
		this.min = min;
	}

	public double getRate() {
		return this.rate;
	}

	public void setRate(double rate) {
		this.rate = rate;
	}

	public long getTieredID() {
		return this.tieredID;
	}

	public void setTieredID(long tieredID) {
		this.tieredID = tieredID;
	}

}