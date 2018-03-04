using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SmartSuggestion_Controller : MonoBehaviour {
	
	ItemRecordDetails selectedItemRecord;

	//================ Name =================
	[SerializeField]
	InputField TargetInputField_Name;
	List<GameObject> buttonList = new List<GameObject> ();
	[SerializeField]
	Transform buttonsMama;
	[SerializeField]
	GameObject SuggestionButton;

	public void searchForSuggestion(){
		buttonsMama.gameObject.SetActive (true);
		List<ItemRecordDetails> suggestionList = ItemRecords_Manager.searchByName(TargetInputField_Name.text);

		if(suggestionList != null){
			cleanUpPrefabList ();
			for(int i = 0; i < suggestionList.Count ; i++){
				instantiateButton_S(suggestionList [i]);
			}
		}
	}

	void instantiateButton_S(ItemRecordDetails inputRecord){

		GameObject newButton = Instantiate (SuggestionButton,buttonsMama);
		newButton.SetActive (true);
		SmartSuggestion_Button button_s = newButton.GetComponent<SmartSuggestion_Button> ();
		button_s.controller_ = this;
		button_s.thisRecordDetails = inputRecord;
		button_s.refreshText ();

		buttonList.Add (newButton);		
	}

	void cleanUpPrefabList(){
		for (int i = 0; i < buttonList.Count; i++) {
			Destroy (buttonList[i]);
		}
		buttonList.Clear ();	
	}

	public void updateSelected(ItemRecordDetails inputRecord){

		selectedItemRecord = inputRecord;
		TargetInputField_Name.text = selectedItemRecord.ItemName;
		buttonsMama.gameObject.SetActive (false);
		searchForSuggestion_price ();
	}

	//================ Price =================
	[SerializeField]
	InputField TargetInputField_Price;
	List<GameObject> priceList = new List<GameObject> ();
	[SerializeField]
	Transform priceMama;
	[SerializeField]
	GameObject SuggestionPrice;

	public void searchForSuggestion_price(){
		if (selectedItemRecord.priceList != null) {
			priceMama.gameObject.SetActive (true);
			List<float> suggestionList = selectedItemRecord.priceList;

			if(suggestionList != null){
				cleanUpPrefabList_price ();
				for(int i = 0; i < suggestionList.Count ; i++){
					instantiateButton_P(suggestionList [i]);
				}
			}
		}
	}

	void instantiateButton_P(float price){
		Debug.Log (price);
		GameObject newButton = Instantiate (SuggestionPrice,priceMama);
		newButton.SetActive (true);
		SmartSuggestion_Price button_p = newButton.GetComponent<SmartSuggestion_Price> ();
		button_p.priceText.text = price.ToString("00.00");
		priceList.Add (newButton);		
	}

	void cleanUpPrefabList_price(){
		for (int i = 0; i < priceList.Count; i++) {
			Destroy (priceList[i]);
		}
		priceList.Clear ();	
	}

	//================Others========================

	public void saveCurrent(){
		float inputPrice_value = 0;

		if (TargetInputField_Price.text != "")
			float.TryParse (TargetInputField_Price.text,out inputPrice_value);
		
		ItemRecords_Manager.addNew (TargetInputField_Name.text,inputPrice_value);	
	}

}
