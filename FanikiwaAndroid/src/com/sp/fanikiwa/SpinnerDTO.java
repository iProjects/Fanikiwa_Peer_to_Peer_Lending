package com.sp.fanikiwa;

public class SpinnerDTO {

	private String id;
	private String description;

	public SpinnerDTO(String id, String description) {
		this.setId(id);
		this.setDescription(description);
	}

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	@Override
	public String toString() {
		return this.description; // Value to be displayed in the Spinner
	}
}
