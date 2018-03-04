using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class Price_Manager : MonoBehaviour {

	[SerializeField]
	UI_stringTab priceInWord;
	[SerializeField]
	UI_genericList nettAmountList;
	[SerializeField]
	Text subTotal;

	[SerializeField]
	Text gstText;
	[SerializeField]
	Text totalText;

	public void getTotal(){
		subTotal.text = "";
		priceInWord.setText("");

		gstText.text = "";
		totalText.text = "";

		float totalAmount = 0;
		for (int i = 0; i < nettAmountList.currentStringList.Count; i++) {
			float thisAmount = 0;
			float.TryParse (nettAmountList.currentStringList[i],out thisAmount);
			totalAmount += thisAmount;
		}

		subTotal.text = "RM " + totalAmount.ToString("N");
		priceInWord.setText(PriceToWords((totalAmount * 1.06f).ToString(".00")));

		gstText.text = "GST　RM " + (totalAmount * 0.06f).ToString("N");
		totalText.text = "RM " + (totalAmount * 1.06f).ToString("N");
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
