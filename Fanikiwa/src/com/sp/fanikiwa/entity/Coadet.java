package com.sp.fanikiwa.entity;

import com.googlecode.objectify.Ref;
import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;

@Entity
public class Coadet {

	@Id
	Long id;

	private int coaLevel;

	private String description;

	private int rorder;

	private String shortCode;

	private Ref<Coa> coa;

	public Coadet() {
	}

	public Coadet(Long Id) {
		setId(Id);
	}

	public Long getId() {
		return this.id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public int getCOALevel() {
		return this.coaLevel;
	}

	public void setCOALevel(int COALevel) {
		this.coaLevel = COALevel;
	}

	public String getDescription() {
		return this.description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public int getRorder() {
		return this.rorder;
	}

	public void setRorder(int rorder) {
		this.rorder = rorder;
	}

	public String getShortCode() {
		return this.shortCode;
	}

	public void setShortCode(String shortCode) {
		this.shortCode = shortCode;
	}

	public Coa getCoa() {
		return this.coa.get();
	}

	public void setCoa(Coa coa) {
		this.coa = Ref.create(coa);
	}

}