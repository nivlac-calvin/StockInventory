using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InvoiceButton : MonoBehaviour {
	public bool justCopy = false;
	public InvoiceRecords_Details thisInvoiceRecord;
	[SerializeField]
	Text buttonText;
	[SerializeField]
	UI_pageController pageController;
	[SerializeField]
	GameObject EditingPanel;
	[SerializeField]
	UI_OtherDetails otherDetails;
	[SerializeField]
	UI_PriceTabController priceTab;
	[SerializeField]
	UI_Invoice_Preview inv_Previewer;
	[SerializeField]
	Button confirmButton;
	[SerializeField]
	GameObject justCopy_parent;

	public bool isSummary = false;

	public void refreshText(){
		if (isSummary) {
			if (thisInvoiceRecord.clientData_ID != null && thisInvoiceRecord.clientData_ID != "" && thisInvoiceRecord.clientData_ID != "empty") {
				clientDataDetails clientData = DataManager_New.instance.getPartnerByID (int.Parse (thisInvoiceRecord.clientData_ID));
				buttonText.text = thisInvoiceRecord.invoiceDate + "  " + clientData.clientAccNo + " " + thisInvoiceRecord.projectTitle + " RM" + getTotalPrice (thisInvoiceRecord.itemDetails_List).ToString (".00");
			} else
				buttonText.text = "Unknow Client " + thisInvoiceRecord.projectTitle + " RM" + getTotalPrice (thisInvoiceRecord.itemDetails_List).ToString (".00");
		} else {
			buttonText.text = "Inv No : " + thisInvoiceRecord.invoiceID  + "   Title : "  + thisInvoiceRecord.projectTitle;
		}

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

	public void chooseThis(){
		if (!inv_Previewer.gameObject.activeSelf)
			inv_Previewer.gameObject.SetActive (true);
		
		inv_Previewer.InvoiceId = int.Parse (thisInvoiceRecord.invoiceID);
		inv_Previewer.startLoad ();

		confirmButton.onClick.RemoveAllListeners ();
		confirmButton.onClick.AddListener (() => thisConfirm());

//
//		if (justCopy)
//			UI_invoiceManager.instance.refreshUI_justCopy (Main_InvoiceManager.instance.getInvoice (int.Parse(thisInvoiceRecord.invoiceID)));
//		else
//			UI_invoiceManager.instance.refreshUI(Main_InvoiceManager.instance.getInvoice (int.Parse(thisInvoiceRecord.invoiceID)));

	}

	void thisConfirm(){
		if (justCopy) {
			DataManager_New.instance.copyData (int.Parse(thisInvoiceRecord.invoiceID),pageController.invoiceID);
			EditingPanel.SetActive (false);
			EditingPanel.SetActive (true);
			justCopy_parent.gameObject.SetActive (false);
		} else {
			pageController.invoiceID = int.Parse(thisInvoiceRecord.invoiceID);
			otherDetails.thisInvoiceID = int.Parse (thisInvoiceRecord.invoiceID);
			priceTab.thisInvoiceID = int.Parse (thisInvoiceRecord.invoiceID);
			EditingPanel.SetActive (true);
		}
		inv_Previewer.gameObject.SetActive (false);

	}
}
