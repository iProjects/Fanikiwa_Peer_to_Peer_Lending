package com.sp.fanikiwa.entity;

import java.util.List;

public class BatchSimulateStatus {
	public BatchSimulateStatus(List<SimulatePostStatus> status) {
		SimulateStatus = status;
	}

	public boolean CanPost() {
		for (SimulatePostStatus s : SimulateStatus) {
			if (!s.isCanPost())
				return false;
		}
		return true;
	}

	public List<SimulatePostStatus> SimulateStatus;
}
