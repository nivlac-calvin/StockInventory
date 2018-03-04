using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartSuggestion_Button_Project : MonoBehaviour {
	public bool justCopy = false;
	public InvoiceRecordsDetails thisInvoiceRecord;
	[SerializeField]
	Text buttonText;

	public void refreshText(){
		buttonText.text = "Inv No : " + thisInvoiceRecord.invoiceID  + "   Title : "  + thisInvoiceRecord.projectTitle;
	}

	public void chooseThis(){
		if (justCopy)
			UI_invoiceManager.instance.refreshUI_justCopy (Main_InvoiceManager.instance.getInvoice (int.Parse(thisInvoiceRecord.invoiceID)));
		else
			UI_invoiceManager.instance.refreshUI(Main_InvoiceManager.instance.getInvoice (int.Parse(thisInvoiceRecord.invoiceID)));
	
	}
}
