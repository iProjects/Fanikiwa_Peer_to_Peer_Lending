package com.sp.fanikiwa.entity;

public class DoubleEntry {
	private Transaction dr;
	private Transaction cr;

	public DoubleEntry() {
	}

	public DoubleEntry(Transaction dr, Transaction cr) {
		this.dr = dr;
		this.cr = cr;
	}

	public void setDr(Transaction value) {
		this.dr = value;
	}

	public Transaction getDr() {
		return this.dr;
	}

	public void setCr(Transaction value) {
		this.cr = value;
	}

	public Transaction getCr() {
		return this.cr;
	}
}
