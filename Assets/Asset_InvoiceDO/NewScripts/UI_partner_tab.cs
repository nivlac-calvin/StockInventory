using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_partner_tab : MonoBehaviour {

	public Text targetText;
	public int thisID;

	public UI_OtherDetails target_otherDetails;

	public void refreshThis(){
		clientDataDetails currentData = DataManager_New.instance.getPartnerByID (thisID);
		targetText.text = currentData.clientAccNo;
	}

	public void selectThis(){
		target_otherDetails.Partner_CurrentID = thisID;
		target_otherDetails.Partner_updatePreview ();
	}
}
