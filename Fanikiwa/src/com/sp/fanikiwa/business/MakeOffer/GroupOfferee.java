package com.sp.fanikiwa.business.MakeOffer;

import java.util.Collection;
import java.util.List;

import com.sp.fanikiwa.api.LendingGroupEndpoint;
import com.sp.fanikiwa.api.LendingGroupMemberEndpoint;
import com.sp.fanikiwa.api.MemberEndpoint;
import com.sp.fanikiwa.entity.Lendinggroup;
import com.sp.fanikiwa.entity.Lendinggroupmember;
import com.sp.fanikiwa.entity.Member;

public class GroupOfferee  extends Offeree {
	private String GroupName;
	private List<Member> members;	
		
	public GroupOfferee(String groupName) {
		super();
		GroupName = groupName;
		//Fill Members
		FillMembers(groupName);
	}
	

	public String getGroupName() {
		return GroupName;
	}
	public void setGroupName(String groupName) {
		GroupName = groupName;
	}
	public List<Member> getMembers() {
		return members;
	}

	//called recursively
	private void FillMembers(String Groupname)
	{
		LendingGroupMemberEndpoint ep = new LendingGroupMemberEndpoint();
		
		Collection<Lendinggroupmember> grpmembers = (ep.selectgroupMembers(Groupname, null, null)).getItems();
		for(Lendinggroupmember m : grpmembers)
		{
			MemberEndpoint mep = new MemberEndpoint();
			Member meb ;
			
			switch(m.getIdType())
			{
			case EMAIL:
				meb = mep.GetMemberByEmail(m.getName());
				if(meb != null)
					members.add(meb);
				break;
			case TELNO:
				meb = mep.GetMemberByTelephone(m.getName());
				if(meb != null)
					members.add(meb);
				break;
			case MEMBER:
				Long id = Long.parseLong(m.getName());
				meb = mep.getMemberByID(id);
				if(meb != null)
					members.add(meb);
				break;
			}

		}
		
		LendingGroupEndpoint lep = new LendingGroupEndpoint();
		Collection<Lendinggroup> subgroups = (lep.retrieveSubgroups(Groupname, null, null)).getItems();
		for(Lendinggroup lg:subgroups) //this is a nested group; find out its members recursively
		{
			FillMembers(lg.getGroupName());
		}
	}

}
