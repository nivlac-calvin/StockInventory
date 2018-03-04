using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class UI_PriceTabController : MonoBehaviour {
	public int thisInvoiceID;
	[SerializeField]
	GameObject detailsPanel;
	[SerializeField]
	InputField priceInText_InputField;
	bool display = false;

	[SerializeField]
	Text subTotal_priceText;
	[SerializeField]
	Text gst_priceText;
	[SerializeField]
	Text total_priceText;


	public void displayButton(){
		display = !display;
		detailsPanel.gameObject.SetActive (display);
		calculate ();
	}

	public float currentSubTotal = 0;

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

		calculate ();
		display = true;
		displayButton ();

	}

	void calculate(){
		float total_amount = 0;
		InvoiceRecords_Details thisRecord = DataManager_New.instance.invoiceList [thisInvoiceID];

		for(int i = 0 ; i < thisRecord.itemDetails_List.Count ; i++){

			for(int j = 0 ; j < thisRecord.itemDetails_List[i].nettAmount_List.Count ; j++){
				float result = 0;
				float.TryParse (thisRecord.itemDetails_List[i].nettAmount_List[j],out result);

				total_amount += result;

			}		
		}

		currentSubTotal = total_amount;

		subTotal_priceText.text = "RM " + currentSubTotal.ToString ("N");	
		float sixPer = currentSubTotal / 100 * 6;
		gst_priceText.text = "GST RM " + sixPer.ToString ("N");
		total_priceText.text = "RM " + (currentSubTotal + sixPer).ToString ("N");
		priceInText_InputField.text = PriceToWords((currentSubTotal + sixPer).ToString (".00"));
		priceInText_InputField.textComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
		priceInText_InputField.textComponent.verticalOverflow = VerticalWrapMode.Truncate;
	}

	public string PriceToWords(string inputString){
		string fullText = "";
		string[] price_Split = inputString.Split (new string[] {"."}, System.StringSplitOptions.None);

		fullText += "RM : " + NumberToText (long.Parse(price_Split[0]));

		if(NumberToText (long.Parse(price_Split[1])).ToLower().Contains("zero") == false) {

			fullText += " And " + NumberToText (long.Parse(price_Split[1])) + " Cent";
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
