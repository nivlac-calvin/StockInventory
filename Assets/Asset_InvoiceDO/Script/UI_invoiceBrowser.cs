using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_invoiceBrowser : MonoBehaviour {

	[SerializeField]
	GameObject invoicePrefab;
	[SerializeField]
	Transform prefabMama;

	List<GameObject> prefabList = new List<GameObject>();

	void instantiateButton_S(InvoiceRecordsDetails inputInvoice){

		GameObject newButton = Instantiate (invoicePrefab,prefabMama);
		newButton.SetActive (true);
//		SmartSuggestion_Button_Project button_s = newButton.GetComponent<SmartSuggestion_Button_Project> ();
//
//		button_s.thisInvoiceRecord = inputInvoice;
//		button_s.refreshText ();

		prefabList.Add (newButton);		
	}

	void destroyButtonList(){

		for(int i = 0 ; i < prefabList.Count ; i++){

			Destroy (prefabList[i]);
		}

		prefabList.Clear ();
	}

}
