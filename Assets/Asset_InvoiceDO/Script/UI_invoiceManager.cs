using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_invoiceManager : MonoBehaviour {

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
	UI_stringTab SalesEngineerCode_Tab;
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
	[SerializeField]
	UI_genericList unitPrice_UI;
	[SerializeField]
	UI_genericList nettAmount_UI;

	public static UI_invoiceManager instance;

	public InvoiceRecordsDetails thisInvoiceData;
	public int currentInvoiceIndex;

	void Awake(){
	
		instance = this;
	}

	void Start(){
	
		//refreshUI(Main_InvoiceManager.instance.getInvoice (0));

	}

	public void addNewProject_Button(){
	
		Main_InvoiceManager.instance.addNewEmptyProject ();
		refreshUI(Main_InvoiceManager.instance.getInvoice (Main_InvoiceManager.instance.currentInvoiceList.Count-1));
	}

	public void refreshUI(InvoiceRecordsDetails inputInvoiceData){
	
		currentInvoiceIndex = int.Parse(inputInvoiceData.invoiceID);
		Debug.Log ("Refreshing ID : " + currentInvoiceIndex);
		thisInvoiceData = inputInvoiceData;

		//Top
		invoiceID_Text.text = thisInvoiceData.invoiceID;
		AccNo.setText(thisInvoiceData.clientAccNo);
		Address.setText(thisInvoiceData.clientAddress);
		Date_Tab.setText(thisInvoiceData.invoiceDate);

		//Main
		Term_Tab.setText (thisInvoiceData.term);
		SalesEngineerCode_Tab.setText (thisInvoiceData.salesEng_Code);
		PO_Tab.setText (thisInvoiceData.PurchaseOrder);
		DO_Tab.setText (thisInvoiceData.invoiceID);

		itemNo_UI.generateList (thisInvoiceData.itemNo_List);
		quantity_UI.generateList (thisInvoiceData.quantity_List);
		List<string> newStringList = thisInvoiceData.invoiceItem_List;
		newStringList.Insert (0,thisInvoiceData.projectTitle);
		itemsList_UI.generateList (newStringList);
		unitPrice_UI.generateList (thisInvoiceData.unitPrice_List);
		nettAmount_UI.generateList (thisInvoiceData.nettAmount_List);
	}

	public void refreshUI_justCopy(InvoiceRecordsDetails inputInvoiceData){

		thisInvoiceData = inputInvoiceData;
		thisInvoiceData.invoiceID = currentInvoiceIndex.ToString ("0000");
		Debug.Log (" Copy : "+ thisInvoiceData.invoiceID);
		//Top
		invoiceID_Text.text = thisInvoiceData.invoiceID;
		AccNo.setText(thisInvoiceData.clientAccNo);
		Address.setText(thisInvoiceData.clientAddress);
		Date_Tab.setText(thisInvoiceData.invoiceDate);

		//Main
		Term_Tab.setText (thisInvoiceData.term);
		SalesEngineerCode_Tab.setText (thisInvoiceData.salesEng_Code);
		PO_Tab.setText (thisInvoiceData.PurchaseOrder);
		DO_Tab.setText (thisInvoiceData.invoiceID);

		itemNo_UI.generateList (thisInvoiceData.itemNo_List);
		quantity_UI.generateList (thisInvoiceData.quantity_List);
		List<string> newStringList = thisInvoiceData.invoiceItem_List;
		newStringList.Insert (0,thisInvoiceData.projectTitle);
		itemsList_UI.generateList (newStringList);
		unitPrice_UI.generateList (thisInvoiceData.unitPrice_List);
		nettAmount_UI.generateList (thisInvoiceData.nettAmount_List);
	}

	public void saveCurrentInvoice(){
	
		LoadingPanel.instance.waitFor (1);
		thisInvoiceData.clientAccNo = AccNo.myText.text;
		thisInvoiceData.clientAddress = Address.myText.text;
		thisInvoiceData.invoiceDate = Date_Tab.myText.text;

		thisInvoiceData.term = Term_Tab.myText.text;
		thisInvoiceData.salesEng_Code = SalesEngineerCode_Tab.myText.text;
		thisInvoiceData.PurchaseOrder = PO_Tab.myText.text;

		thisInvoiceData.itemNo_List = itemNo_UI.currentStringList;
		thisInvoiceData.quantity_List = quantity_UI.currentStringList;
		thisInvoiceData.projectTitle = itemsList_UI.currentStringList [0];
		itemsList_UI.currentStringList.RemoveAt (0);
		thisInvoiceData.invoiceItem_List = itemsList_UI.currentStringList;
		thisInvoiceData.unitPrice_List = unitPrice_UI.currentStringList;
		thisInvoiceData.nettAmount_List = nettAmount_UI.currentStringList;

		Main_InvoiceManager.instance.ModifyList (currentInvoiceIndex,thisInvoiceData);

		refreshUI (Main_InvoiceManager.instance.getInvoice(currentInvoiceIndex));
	}

	public void backToInvoice(){
	
		UI_invoiceManager.instance.refreshUI(Main_InvoiceManager.instance.getInvoice (currentInvoiceIndex));
	}

}
