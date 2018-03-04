using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartSuggestion_Button : MonoBehaviour {

	[HideInInspector]
	public SmartSuggestion_Controller controller_;
	public ItemRecordDetails thisRecordDetails;
	[SerializeField]
	Text buttonText;

	public void refreshText(){
		buttonText.text = thisRecordDetails.ItemName;
	}

	public void chooseThis(){

		controller_.updateSelected (thisRecordDetails);
	
	}
}
