using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Data_InvoiceBrowser : MonoBehaviour {

	[SerializeField]
	bool justCopy = false;
	[SerializeField]
	InputField TargetInptField;

	void OnEnable(){
		StartCoroutine (LoadCoroutine());
	}

	bool loaded = false;
	IEnumerator LoadCoroutine(){
	
		while (DataManager_New.instance == null) {
			yield return null;
		}
		while(!DataManager_New.instance.loaded_partner){
			yield return null;
		}
		loaded = true;
		searchForSuggestion ();

	}

	[SerializeField]
	ScrollRect targetScrollRect;

	public void searchForSuggestion(){
		if (loaded) {
			destroyButtonList ();
			for (int i = DataManager_New.instance.startFrom; i < DataManager_New.instance.invoiceList.Count; i++) {

				if (DataManager_New.instance.invoiceList [i].projectTitle.ToLower ().Contains (TargetInptField.text.ToLower ())) {
					instantiateButton_S (DataManager_New.instance.invoiceList [i]);
				} else if (DataManager_New.instance.invoiceList [i].invoiceID.Contains (TargetInptField.text.ToLower ())) {
					instantiateButton_S (DataManager_New.instance.invoiceList [i]);
				}

			}
			StartCoroutine (normalized());
		}
	}

	IEnumerator normalized(){
	
		yield return null;
		targetScrollRect.verticalNormalizedPosition = 0f;
	}

	[SerializeField]
	GameObject buttonPrefab;
	[SerializeField]
	Transform buttonsMama;

	List<GameObject> buttonList = new List<GameObject>();

	void instantiateButton_S(InvoiceRecords_Details inputInvoice){

		GameObject newButton = Instantiate (buttonPrefab,buttonsMama);
		newButton.SetActive (true);
		UI_InvoiceButton button_s = newButton.GetComponent<UI_InvoiceButton> ();
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


	public void button_addNewInvoice(){
		if (loaded) {
			DataManager_New.instance.addNewInvoice ();
			searchForSuggestion ();
		}
	}
}
