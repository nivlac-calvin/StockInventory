using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class UI_Invoice_Preview : MonoBehaviour {

	public int InvoiceId;
	int currentPage;

	InvoiceRecords_Details thisInvoiceRecords;

	[SerializeField]
	Text targetText_AccountNo;
	[SerializeField]
	Text targetText_Address;
	[SerializeField]
	Text targetText_InvoiceNo;
	[SerializeField]
	Text targetText_Page_No;
	[SerializeField]
	Text targetText_Date;

	[SerializeField]
	Text targetText_Term;
	[SerializeField]
	Text targetText_SalesEngineer;
	[SerializeField]
	Text targetText_PO;
	[SerializeField]
	Text targetText_DO;

	[SerializeField]
	UI_SimpleStringList target_quantityList;
	[SerializeField]
	UI_SimpleStringList target_descriptionList;
	[SerializeField]
	UI_SimpleStringList target_unitPriceList;
	[SerializeField]
	UI_SimpleStringList target_nettAmountList;

	[SerializeField]
	Text targetText_subTotal;
	[SerializeField]
	Text targetText_gst;
	[SerializeField]
	Text targetText_total;
	[SerializeField]
	Text targetText_priceToWord;

	[SerializeField]
	GameObject controlPanel;

	[SerializeField]
	Text titleText;
	[SerializeField]
	Text idText;
	[SerializeField]
	GameObject doOtherPanel;
	[SerializeField]
	GameObject pricePanel;

	void OnEnable(){	
		StartCoroutine (load_coroutine());
	}

	IEnumerator load_coroutine(){

		while (DataManager_New.instance == null) {
			yield return null;
		}
		while(!DataManager_New.instance.loaded_partner){
			yield return null;
		}
		thisInvoiceRecords = DataManager_New.instance.invoiceList [InvoiceId];
		refreshUI ();
	}

	public void startLoad(){
		StartCoroutine (load_coroutine());
	}

	public void refreshUI(){
		if (!doOtherPanel.activeSelf) {
			if (currentPage == thisInvoiceRecords.itemDetails_List.Count - 1)
				pricePanel.SetActive (true);
			else
				pricePanel.SetActive (false);
		}

		Debug.Log (thisInvoiceRecords.clientData_ID);
		if (thisInvoiceRecords.clientData_ID != null && thisInvoiceRecords.clientData_ID != "" && thisInvoiceRecords.clientData_ID != "empty") {
			Debug.Log (thisInvoiceRecords.clientData_ID);
			clientDataDetails clientData = DataManager_New.instance.getPartnerByID (int.Parse (thisInvoiceRecords.clientData_ID));
			targetText_AccountNo.text = clientData.clientAccNo;
			targetText_Address.text = boldFirstLine(clientData.clientAddress);
			targetText_Term.text = clientData.clientTerm;
		} else {
			targetText_AccountNo.text = "";
			targetText_Address.text = "";
		}

		targetText_InvoiceNo.text = thisInvoiceRecords.invoiceID;
		targetText_Page_No.text = (currentPage+1) +" / "+ thisInvoiceRecords.itemDetails_List.Count +" ";
		targetText_Date.text = thisInvoiceRecords.invoiceDate;


		targetText_SalesEngineer.text = thisInvoiceRecords.salesEng_Code;
		targetText_PO.text = thisInvoiceRecords.PurchaseOrder;
		targetText_DO.text = thisInvoiceRecords.invoiceID;

		target_quantityList.setStringList(thisInvoiceRecords.itemDetails_List[currentPage].quantity_List);
		target_descriptionList.setStringList(thisInvoiceRecords.itemDetails_List[currentPage].description_List);
		target_unitPriceList.setStringList(thisInvoiceRecords.itemDetails_List[currentPage].unitPrice_List);
		target_nettAmountList.setStringList(thisInvoiceRecords.itemDetails_List[currentPage].nettAmount_List);

		float total_amount = 0;

		for(int i = 0 ; i < thisInvoiceRecords.itemDetails_List.Count ; i++){

			for(int j = 0 ; j < thisInvoiceRecords.itemDetails_List[i].nettAmount_List.Count ; j++){
				float result = 0;
				float.TryParse (thisInvoiceRecords.itemDetails_List[i].nettAmount_List[j],out result);

				total_amount += result;

			}		
		}

		float currentSubTotal = total_amount;

		targetText_subTotal.text = "<b>" + currentSubTotal.ToString ("N")+ "</b>";	
		float sixPer = currentSubTotal / 100 * 6;
		targetText_gst.text = "<b>" + sixPer.ToString ("N") + "</b>";
		targetText_total.text = "<b>" + (currentSubTotal + sixPer).ToString ("N") + "</b>";
		targetText_priceToWord.text = "<b>" +PriceToWords((currentSubTotal + sixPer).ToString (".00")) + "</b>";

//
//		targetText_subTotal.text = thisInvoiceRecords;
//		targetText_gst;
//		targetText_total;
//		targetText_priceToWord;	
	}

	string boldFirstLine(string inputString){

		string[] splited = inputString.Split ('\n');
		string newFull = "";
		for(int i = 0; i < splited.Length ; i++){
			if (i == 0) {
				splited[i] = "<b>"+splited[i]+"</b>";
			}	
			if(i < splited.Length - 1)
				splited [i] += "\n";
			newFull += splited [i];
		}

		return newFull;
	}
		
	[SerializeField]
	GameObject[] hideWhenDO_list;

	public void switchToDO(){
		titleText.text = "DELIVERY ORDER";
		idText.text = "DO NO           :";
		target_descriptionList.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2(1026,target_descriptionList.gameObject.GetComponent<RectTransform> ().sizeDelta.y);
		for(int i = 0 ; i < hideWhenDO_list.Length ; i++){
			hideWhenDO_list [i].SetActive (false);
		}
		doOtherPanel.SetActive (true);
	}

	public void switchToINV(){
		titleText.text = "TAX INVOICE";
		idText.text = "INVOICE NO :";
		target_descriptionList.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2(680,target_descriptionList.gameObject.GetComponent<RectTransform> ().sizeDelta.y);
		for(int i = 0 ; i < hideWhenDO_list.Length ; i++){
			hideWhenDO_list [i].SetActive (true);
		}
		doOtherPanel.SetActive (false);
	}



	public void nextPage(){
		if (currentPage < thisInvoiceRecords.itemDetails_List.Count - 1) {
			currentPage++;
			refreshUI ();
		}
	}

	public void prevPage(){
		if (currentPage > 0) {
			currentPage--;
			refreshUI ();
		}
	}


	public string PriceToWords(string inputString){
		string fullText = "";
		string[] price_Split = inputString.Split (new string[] {"."}, System.StringSplitOptions.None);

		long parseResult_01 = 0;

		if(!long.TryParse (price_Split[0],out parseResult_01))
			Debug.Log (parseResult_01);

		long parseResult_02 = 0;

		if(!long.TryParse (price_Split[1],out parseResult_02))
			Debug.Log (parseResult_02);

		fullText += "RM : " + NumberToText (parseResult_01);

		if(NumberToText (parseResult_02).ToLower().Contains("zero") == false) {

			fullText += " And " + NumberToText (parseResult_02) + " Cent";
		}

		return fullText + " Only";

	}

	public string NumberToText(long number)
	{
		StringBuilder wordNumber = new StringBuilder();                       

		string[] powers = new string[] { "Thousand ", "Million ", "Billion " };
		string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
		string[] ones = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", 
			"Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

		if (number == 0) { return "Zero"; }
		if (number < 0) 
		{ 
			wordNumber.Append("Negative ");
			number = -number;
		}

		long[] groupedNumber = new long[] { 0, 0, 0, 0 };
		int groupIndex = 0;

		while (number > 0)
		{
			groupedNumber[groupIndex++] = number % 1000;
			number /= 1000;
		}

		for (int i = 3; i >= 0; i--)
		{
			long group = groupedNumber[i];

			if (group >= 100)
			{
				wordNumber.Append(ones[group / 100 - 1] + " Hundred ");
				group %= 100;

				if (group == 0 && i > 0)
					wordNumber.Append(powers[i - 1]);
			}

			if (group >= 20)
			{
				if ((group % 10) != 0)
					wordNumber.Append(tens[group / 10 - 2] + " " + ones[group % 10 - 1] + " ");
				else
					wordNumber.Append(tens[group / 10 - 2] + " ");                    
			}
			else if (group > 0)
				wordNumber.Append(ones[group - 1] + " ");

			if (group != 0 && i > 0)
				wordNumber.Append(powers[i - 1]);
		}

		return wordNumber.ToString().Trim();
	}

}
