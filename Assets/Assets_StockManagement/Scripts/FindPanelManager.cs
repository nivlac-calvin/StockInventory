using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FindPanelManager : MonoBehaviour {

	bool sortby_brand,sortby_partNo,sortby_name;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	List<Item> itemData_List = new List<Item>();
	List<Item> sorted_List = new List<Item>();

	void startFind(string searchName){

		for(int i = 0; i < itemData_List.Count ; i++){

			if (sortby_brand) {

				if (itemData_List [i].brand_name.Contains (searchName)) {
					sorted_List.Add (itemData_List [i]);
				}
							
			}

			if (sortby_partNo) {

				if (itemData_List [i].part_no.Contains (searchName)) {
					sorted_List.Add (itemData_List [i]);
				}

			}

		}	


	}
}
