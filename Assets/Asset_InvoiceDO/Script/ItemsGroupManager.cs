using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct invoiceItem{

	public string itemDescription;
	public float itemPrice;
};

[System.Serializable]
public struct itemGroups{
	public string titleString;
	public List<invoiceItem> invoiceItemList;
};

public class ItemsGroupManager : MonoBehaviour {

	[SerializeField]
	GameObject invoiceItemPrefab;
	[SerializeField]
	Transform momOfItems;
	[SerializeField]
	itemGroups thisItemGroups;
	List<GameObject> prefabsList =  new List<GameObject>();

	void Start(){
		Refresh ();
	}

	void inputTitle(string inputString){
		thisItemGroups.titleString = inputString;
		Refresh ();	
	}

	void addNewItem(invoiceItem inputItem){
		thisItemGroups.invoiceItemList.Add (inputItem);
		Refresh ();	
	}

	int currentIndex = 0;
	void Refresh(){
	
		for(int i = 0 ;i< prefabsList.Count ; i++){

			Destroy (prefabsList[i]);
		}

		prefabsList.Clear ();

		instantiateTitle (thisItemGroups.titleString);
		currentIndex = 0;
		for(int i = 0 ; i < thisItemGroups.invoiceItemList.Count ; i++){
			instantiatePrefab (thisItemGroups.invoiceItemList[i].itemDescription);
		}
	}

	public void RemoveAt(int input){
		Debug.Log ("RemoveAt : " + input);
		thisItemGroups.invoiceItemList.RemoveAt (input);
		Refresh ();	
	}

	public void moveUp(int ID,invoiceItem inputItem){
		thisItemGroups.invoiceItemList.RemoveAt (ID);
		thisItemGroups.invoiceItemList.Insert (ID-1,inputItem);
		Refresh ();
	}

	public void moveDown(int ID,invoiceItem inputItem){
		thisItemGroups.invoiceItemList.RemoveAt (ID);
		thisItemGroups.invoiceItemList.Insert (ID+1,inputItem);
		Refresh ();
	}

	public void saveThis(int inputID, invoiceItem inputItem){

		thisItemGroups.invoiceItemList [inputID] = inputItem;
	
	}

	void instantiateTitle(string inputString){
		inputString = "" + inputString + "";
		GameObject newObj = Instantiate (invoiceItemPrefab,momOfItems);
		newObj.SetActive (true);
		newObj.GetComponent<UI_ItemDetailsTab> ().thisInvoiceItem = new invoiceItem ();
		newObj.GetComponent<UI_ItemDetailsTab> ().thisInvoiceItem.itemDescription = inputString;
		newObj.GetComponent<UI_ItemDetailsTab> ().Refresh_Invoice ();
		newObj.GetComponent<Button> ().interactable = false;
		prefabsList.Add (newObj);
	}

	void instantiatePrefab(string inputString){

		GameObject newObj = Instantiate (invoiceItemPrefab,momOfItems);
		newObj.SetActive (true);
		newObj.GetComponent<UI_ItemDetailsTab> ().theManager = this;
		newObj.GetComponent<UI_ItemDetailsTab> ().thisInvoiceItem = new invoiceItem ();
		newObj.GetComponent<UI_ItemDetailsTab> ().thisInvoiceItem.itemDescription = inputString;
		newObj.GetComponent<UI_ItemDetailsTab> ().Refresh_Invoice ();
		newObj.GetComponent<UI_ItemDetailsTab> ().ID = currentIndex;
		currentIndex++;
		prefabsList.Add (newObj);
	}
}
