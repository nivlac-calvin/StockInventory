using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Data_MonthlySummary : MonoBehaviour {

	[SerializeField]
	Dropdown dropDown_Mon;
	[SerializeField]
	Dropdown dropDown_Year;

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
		getToday ();

		searchForSuggestion ();
	}

	public void getToday(){
		dropDown_Mon.value = System.DateTime.Now.Month - 1;

		if(System.DateTime.Now.Year == 2018)
			dropDown_Year.value = 1;	
		else if(System.DateTime.Now.Year == 2019)
			dropDown_Year.value = 2;	
		else if(System.DateTime.Now.Year == 2020)
			dropDown_Year.value = 3;	
		else if(System.DateTime.Now.Year == 2021)
			dropDown_Year.value = 4;	
		else if(System.DateTime.Now.Year == 2022)
			dropDown_Year.value = 5;	
	}


	public void searchForSuggestion(){
		if (loaded) {
			monthlyTotal_price = 0;
			destroyButtonList ();
			for (int i = DataManager_New.instance.startFrom; i < DataManager_New.instance.invoiceList.Count; i++) {

				if(DataManager_New.instance.invoiceList [i].invoiceDate != null){

					string[] splitedString = DataManager_New.instance.invoiceList [i].invoiceDate.Split ('/');

					if (int.Parse(splitedString[1]) == (dropDown_Mon.value+1) && int.Parse(splitedString[2]) == (dropDown_Year.value+2017) ) {
						instantiateButton_S (DataManager_New.instance.invoiceList [i]);
						addToTotal (DataManager_New.instance.invoiceList [i]);
					}
				}
			}
			monthlyTotal.text = "RM " + monthlyTotal_price.ToString (".00");;
			StartCoroutine (normalized());
		}
	}

	[SerializeField]
	Text monthlyTotal;
	float monthlyTotal_price = 0;

	void addToTotal(InvoiceRecords_Details inputInvoice){
	
		float thisPrice = getTotalPrice (inputInvoice.itemDetails_List);
		monthlyTotal_price += thisPrice;
	}

	float getTotalPrice(List<InvoiceItemRecord> itemList_){
		float total_amount = 0;

		for(int i = 0 ; i < itemList_.Count ; i++){

			for(int j = 0 ; j <itemList_[i].nettAmount_List.Count ; j++){
				float result = 0;
				float.TryParse (itemList_[i].nettAmount_List[j],out result);

				total_amount += result;

			}		
		}

		return total_amount;
	}


	[SerializeField]
	ScrollRect targetScrollRect;
	[SerializeField]
	GameObject buttonPrefab;
	[SerializeField]
	Transform buttonsMama;
	List<GameObject> buttonList = new List<GameObject>();

	IEnumerator normalized(){

		yield return null;
		targetScrollRect.verticalNormalizedPosition = 0f;
	}

	void instantiateButton_S(InvoiceRecords_Details inputInvoice){

		GameObject newButton = Instantiate (buttonPrefab,buttonsMama);
		newButton.SetActive (true);
		UI_InvoiceButton button_s = newButton.GetComponent<UI_InvoiceButton> ();
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
