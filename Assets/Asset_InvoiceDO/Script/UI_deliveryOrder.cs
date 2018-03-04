using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_deliveryOrder : MonoBehaviour {

	[SerializeField]
	GameObject UIobj;
	[SerializeField]
	Text invoiceID_Text;
	[SerializeField]
	UI_stringTab AccNo;
	[SerializeField]
	UI_stringTab Address;
	[SerializeField]
	UI_stringTab Date_Tab;

	[SerializeField]
	UI_stringTab Term_Tab;
	[SerializeField]
	UI_stringTab PO_Tab;
	[SerializeField]
	UI_stringTab DO_Tab;

	[SerializeField]
	UI_genericList itemNo_UI;
	[SerializeField]
	UI_genericList quantity_UI;
	[SerializeField]
	UI_genericList itemsList_UI;

	public InvoiceRecordsDetails thisInvoiceData;
	int currentInvoiceIndex;


	public void refreshUI(){
	
		UI_invoiceManager.instance.saveCurrentInvoice ();

		UIobj.SetActive (true);
		thisInvoiceData = Main_InvoiceManager.instance.getInvoice(UI_invoiceManager.instance.currentInvoiceIndex);

		//Top
		invoiceID_Text.text = thisInvoiceData.invoiceID;
		AccNo.setText(thisInvoiceData.clientAccNo);
		Address.setText(thisInvoiceData.clientAddress);
		Date_Tab.setText(thisInvoiceData.invoiceDate);

		//Main
		Term_Tab.setText (thisInvoiceData.term);
		PO_Tab.setText (thisInvoiceData.PurchaseOrder);
		DO_Tab.setText (thisInvoiceData.invoiceID);

		itemNo_UI.generateList (thisInvoiceData.itemNo_List);
		quantity_UI.generateList (thisInvoiceData.quantity_List);

		List<string> newStringList = thisInvoiceData.invoiceItem_List;
		newStringList.Insert (0,thisInvoiceData.projectTitle);
		itemsList_UI.generateList (newStringList);
	}
}
