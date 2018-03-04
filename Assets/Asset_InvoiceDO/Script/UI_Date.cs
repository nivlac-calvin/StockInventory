using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Date : MonoBehaviour {

	string getDateNow(){

		return System.DateTime.Now.ToString ("dd/MM/yyyy");	
	}

	string formatString(string inputString){
		System.DateTime output_string;
		System.DateTime.TryParseExact (inputString,"dd/MM/yyyy",System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.None,out output_string);
		//System.DateTime.TryParse (inputString,out output_string);

		return output_string.ToString("dd/MM/yyyy");	
	}

}
