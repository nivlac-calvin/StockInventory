using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartSuggestion_Project : MonoBehaviour {

	[SerializeField]
	bool justCopy = false;
	[SerializeField]
	InputField TargetInptField;

	List<string> suggestList = new List<string> ();

	void OnEnable(){
	
		searchForSuggestion ();
	}

	public void searchForSuggestion(){
		destroyButtonList ();

		Main_InvoiceManager.instance.loadFiles ();
		for(int i = 0 ;  i < Main_InvoiceManager.instance.currentInvoiceList.Count ; i++){
			
			if (Main_InvoiceManager.instance.currentInvoiceList [i].projectTitle.ToLower ().Contains (TargetInptField.text.ToLower())) {
				instantiateButton_S (Main_InvoiceManager.instance.currentInvoiceList [i]);
			}
			else if(Main_InvoiceManager.instance.currentInvoiceList [i].invoiceID.Contains (TargetInptField.text.ToLower())){
				instantiateButton_S (Main_InvoiceManager.instance.currentInvoiceList [i]);
			}
		}

	}

	[SerializeField]
	GameObject buttonPrefab;
	[SerializeField]
	Transform buttonsMama;

	List<GameObject> buttonList = new List<GameObject>();

	void instantiateButton_S(InvoiceRecordsDetails inputInvoice){

		GameObject newButton = Instantiate (buttonPrefab,buttonsMama);
		newButton.SetActive (true);
		SmartSuggestion_Button_Project button_s = newButton.GetComponent<SmartSuggestion_Button_Project> ();
		button_s.justCopy = this.justCopy;
		button_s.thisInvoiceRecord = inputInvoice;
		button_s.refreshText ();

		buttonList.Add (newButton);		
	}

	void destroyButtonList(){

		for(int i = 0 ; i < buttonList.Count ; i++){

			Destroy (buttonList[i]);
		}

		buttonList.Clear ();
	}


}
