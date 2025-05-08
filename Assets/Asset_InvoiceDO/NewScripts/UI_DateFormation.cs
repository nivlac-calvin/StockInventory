using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DateFormation : MonoBehaviour {
	
	public string currentDateString; //in format dd/mm/yyyy
	[SerializeField]
	Dropdown dayDropDown;
	[SerializeField]
	Dropdown monthDropDown;
	[SerializeField]
	Dropdown yearDropDown;

	bool initialize_done = false;

	public void initialize(){
		dayDropDown.ClearOptions ();
		List<string> dayList = new List<string> ();
		for(int i = 1 ; i< 32 ; i++){
			dayList.Add (i.ToString("00"));
		}
		dayDropDown.AddOptions (dayList);

		monthDropDown.ClearOptions ();
		List<string> monthList = new List<string> ();
		for(int i = 1 ; i< 13 ; i++){
			monthList.Add (i.ToString("00"));
		}
		monthDropDown.AddOptions (monthList);

		initialize_done = true;

		System.DateTime output_date;

		System.DateTime.TryParseExact (currentDateString,"dd/MM/yyyy",System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.None,out output_date);
		if (output_date == new System.DateTime ()) {

			Button_getToday ();

		} else {
			dayDropDown.value = output_date.Day-1;
			monthDropDown.value = output_date.Month-1;

			int indexOfYear = 2;

			for(int i = 0; i < yearDropDown.options.Count ; i++){

				if (yearDropDown.options [i].text == output_date.Year.ToString ("0000")) {

					indexOfYear = i;
				}

			}
			yearDropDown.value = indexOfYear;
		}
		updateCurrentDateString ();
	}

	public void Button_getToday(){
		dayDropDown.value = System.DateTime.Now.Day - 1;
		monthDropDown.value = System.DateTime.Now.Month - 1;

		if(System.DateTime.Now.Year == 2018)
			yearDropDown.value = 3;	
		else if(System.DateTime.Now.Year == 2019)
			yearDropDown.value = 4;	
		else if(System.DateTime.Now.Year == 2020)
			yearDropDown.value = 5;	
		else if(System.DateTime.Now.Year == 2021)
			yearDropDown.value = 6;	
		else if(System.DateTime.Now.Year == 2022)
			yearDropDown.value = 7;	
	}

	public void updateCurrentDateString(){
		if(initialize_done == true)
			currentDateString = dayDropDown.options [dayDropDown.value].text + "//" + monthDropDown.options [monthDropDown.value].text + "//" + yearDropDown.options [yearDropDown.value].text;
	
	}
}
