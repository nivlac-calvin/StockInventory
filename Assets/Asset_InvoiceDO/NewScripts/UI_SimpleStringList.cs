using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SimpleStringList : MonoBehaviour {
	//Fixed size, Fixed Hieracy
	[SerializeField]
	bool thisIsIndex;

	[SerializeField]
	GameObject[] obj_list;

	void OnEnable(){

		if(thisIsIndex){
			List<string> inputString_List = new List<string> ();

			for(int i = 0 ; i < 17 ; i++){

				inputString_List.Add ((i+1).ToString());

			}
			setStringList (inputString_List);
		}
	}

	public void setStringList(List<string> inputString){

		for(int i = 0 ; i < obj_list.Length ; i++){

			if (i < inputString.Count) {
			
				obj_list [i].transform.GetChild (0).GetComponent<Text> ().text = inputString [i];
			} else
				obj_list [i].transform.GetChild (0).GetComponent<Text> ().text = "";
		
		}
	
	}

}
