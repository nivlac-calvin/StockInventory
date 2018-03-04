using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Page : MonoBehaviour {
	
	public int thisPage_invoiceID;
	public int thisPage_id;

	public InvoiceItemRecord thisPageRecords;
	[SerializeField]
	UI_InputFieldGenerator targetQuantity;
	[SerializeField]
	UI_InputFieldGenerator targetDescription;
	[SerializeField]
	UI_InputFieldGenerator targetUnitPrice;
	[SerializeField]
	UI_InputFieldGenerator targetNettAmount;
	public UI_ItemNoController itemNo_Con;

	public void startLoad(){

		thisPageRecords = new InvoiceItemRecord ();
		thisPageRecords.quantity_List = new List<string> ();
		thisPageRecords.description_List = new List<string> ();
		thisPageRecords.unitPrice_List = new List<string> ();
		thisPageRecords.nettAmount_List = new List<string> ();

		targetQuantity.newEmptyList ();
		targetDescription.newEmptyList ();
		targetUnitPrice.newEmptyList ();
		targetNettAmount.newEmptyList ();

		thisPageRecords = DataManager_New.instance.invoiceList [thisPage_invoiceID].itemDetails_List[thisPage_id];

		for(int i = 0 ; i < thisPageRecords.quantity_List.Count ; i++){
			targetQuantity.currentStringList[i] = thisPageRecords.quantity_List[i];
		}

		for(int i = 0 ; i < thisPageRecords.description_List.Count ; i++){
			targetDescription.currentStringList[i] = thisPageRecords.description_List[i];
		}
		for(int i = 0 ; i < thisPageRecords.unitPrice_List.Count ; i++){
			targetUnitPrice.currentStringList[i] = thisPageRecords.unitPrice_List[i];
		}
		for(int i = 0 ; i < thisPageRecords.nettAmount_List.Count ; i++){
			targetNettAmount.currentStringList[i] = thisPageRecords.nettAmount_List[i];
		}

		targetQuantity.RefreshList ();
		targetDescription.RefreshList ();
		targetUnitPrice.RefreshList ();
		targetNettAmount.RefreshList ();

	}
		
	public void saveThisPage(){
	
		for(int i = 0 ; i < targetQuantity.currentStringList.Count ; i ++){
			thisPageRecords.quantity_List [i] = targetQuantity.currentStringList [i];
		}
			
		for(int i = 0 ; i < targetDescription.currentStringList.Count ; i ++){
			thisPageRecords.description_List [i] = targetDescription.currentStringList [i];
		}

		for(int i = 0 ; i < targetUnitPrice.currentStringList.Count ; i ++){
			thisPageRecords.unitPrice_List [i] = targetUnitPrice.currentStringList [i];
		}

		for(int i = 0 ; i < targetNettAmount.currentStringList.Count ; i ++){
			thisPageRecords.nettAmount_List [i] = targetNettAmount.currentStringList [i];
		}

		DataManager_New.instance.saveCurrentInvoice ();
	}



	void set(InvoiceItemRecord inputData){
		thisPageRecords = inputData;
		putToUI ();	
	}

	public void takeFromUI(){
		targetQuantity.updateStringList ();
		targetDescription.updateStringList ();
		targetUnitPrice.updateStringList ();
		targetNettAmount.updateStringList ();
	}

	void putToUI(){
		targetQuantity.currentStringList = thisPageRecords.quantity_List;
		targetQuantity.RefreshList ();

		targetDescription.currentStringList = thisPageRecords.description_List;
		targetDescription.RefreshList ();

		targetUnitPrice.currentStringList = thisPageRecords.unitPrice_List;
		targetUnitPrice.RefreshList ();
	
		targetNettAmount.currentStringList = thisPageRecords.nettAmount_List;
		targetNettAmount.RefreshList ();
	}

	public void deleteSelected(){
		List<int> newListOfDel = itemNo_Con.indexsOfSelected();

		if (newListOfDel.Count > 0) {
		
			for(int i = 0 ; i < newListOfDel.Count; i++){
				targetQuantity.removeItem (newListOfDel [i]);
				targetDescription.removeItem (newListOfDel [i]);
				targetUnitPrice.removeItem (newListOfDel [i]);
				targetNettAmount.removeItem (newListOfDel [i]);
			}
		}

		itemNo_Con.refreshSelection ();
	}

	public void clearThisPage(){
		targetQuantity.clearList ();
		targetDescription.clearList ();
		targetUnitPrice.clearList ();
		targetNettAmount.clearList ();

		saveThisPage ();
	}
}
