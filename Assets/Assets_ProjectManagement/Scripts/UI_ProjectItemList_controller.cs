using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ProjectItemList_controller : MonoBehaviour {

	[SerializeField]
	GameObject projectItemPrefab;
	[SerializeField]
	GameObject groupItem;
	List<GameObject> prefab_projectItemList = new List<GameObject>();
	List<ProjectItem> projectItemList = new List<ProjectItem>();

	[SerializeField]
	RectTransform targetRectTransform;

	// Use this for initialization
	void Start () {

		ProjectItem[] thisProjectItem = new ProjectItem[50];

		for(int i = 0 ; i < thisProjectItem.Length ; i++){

			thisProjectItem[i] = new ProjectItem();
		}

		thisProjectItem[0].OrderFrom = "IDEC";
		thisProjectItem[1].OrderFrom = "X";
		thisProjectItem[2].OrderFrom = "IDEC";
		thisProjectItem[3].OrderFrom = "FFEFF";
		thisProjectItem[4].OrderFrom = "IDEC";
		thisProjectItem[5].OrderFrom = "FFEFF";
		thisProjectItem[6].OrderFrom = "XX";
		thisProjectItem[7].OrderFrom = "FFEFF";
		thisProjectItem[8].OrderFrom = "IDEC";
		thisProjectItem[9].OrderFrom = "FFEFF";
		thisProjectItem[10].OrderFrom = "IDEC";
		thisProjectItem[11].OrderFrom = "X";
		thisProjectItem[12].OrderFrom = "IDEC";
		thisProjectItem[13].OrderFrom = "FFEFF";
		thisProjectItem[14].OrderFrom = "IDEC";
		thisProjectItem[15].OrderFrom = "FFEFF";
		thisProjectItem[16].OrderFrom = "XX";
		thisProjectItem[17].OrderFrom = "FFEFF";
		thisProjectItem[18].OrderFrom = "IDEC";
		thisProjectItem[19].OrderFrom = "FFEFF";
		thisProjectItem[20].OrderFrom = "IDEC";
		thisProjectItem[21].OrderFrom = "X";
		thisProjectItem[22].OrderFrom = "IDEC";
		thisProjectItem[23].OrderFrom = "FFEFF";
		thisProjectItem[24].OrderFrom = "IDEC";
		thisProjectItem[25].OrderFrom = "FFEFF";
		thisProjectItem[26].OrderFrom = "XX";
		thisProjectItem[27].OrderFrom = "FFEFF";
		thisProjectItem[28].OrderFrom = "IDEC";
		thisProjectItem[29].OrderFrom = "FFEFF";
		thisProjectItem[30].OrderFrom = "IDEC";
		thisProjectItem[31].OrderFrom = "X";
		thisProjectItem[32].OrderFrom = "IDEC";
		thisProjectItem[33].OrderFrom = "FFEFF";
		thisProjectItem[34].OrderFrom = "IDEC";
		thisProjectItem[35].OrderFrom = "FFEFF";
		thisProjectItem[36].OrderFrom = "XX";
		thisProjectItem[37].OrderFrom = "FFEFF";
		thisProjectItem[38].OrderFrom = "IDEC";
		thisProjectItem[39].OrderFrom = "FFEFF";
		thisProjectItem[40].OrderFrom = "IDEC";
		thisProjectItem[41].OrderFrom = "X";
		thisProjectItem[42].OrderFrom = "IDEC";
		thisProjectItem[43].OrderFrom = "FFEFF";
		thisProjectItem[44].OrderFrom = "IDEC";
		thisProjectItem[45].OrderFrom = "FFEFF";
		thisProjectItem[46].OrderFrom = "XX";
		thisProjectItem[47].OrderFrom = "FFEFF";
		thisProjectItem[48].OrderFrom = "IDEC";
		thisProjectItem[49].OrderFrom = "FFEFF";

		thisProjectItem[0].UniqueID = "0";
		thisProjectItem[1].UniqueID = "1";
		thisProjectItem[2].UniqueID = "2";
		thisProjectItem[3].UniqueID = "3";
		thisProjectItem[4].UniqueID = "4";
		thisProjectItem[5].UniqueID = "5";
		thisProjectItem[6].UniqueID = "6";
		thisProjectItem[7].UniqueID = "7";
		thisProjectItem[8].UniqueID = "8";
		thisProjectItem[9].UniqueID = "9";
		thisProjectItem[0].UniqueID = "10";
		thisProjectItem[1].UniqueID = "11";
		thisProjectItem[2].UniqueID = "12";
		thisProjectItem[3].UniqueID = "13";
		thisProjectItem[4].UniqueID = "14";
		thisProjectItem[5].UniqueID = "15";
		thisProjectItem[6].UniqueID = "16";
		thisProjectItem[7].UniqueID = "17";
		thisProjectItem[8].UniqueID = "18";
		thisProjectItem[9].UniqueID = "19";
		thisProjectItem[0].UniqueID = "20";
		thisProjectItem[1].UniqueID = "21";
		thisProjectItem[2].UniqueID = "22";
		thisProjectItem[3].UniqueID = "23";
		thisProjectItem[4].UniqueID = "24";
		thisProjectItem[5].UniqueID = "25";
		thisProjectItem[6].UniqueID = "26";
		thisProjectItem[7].UniqueID = "27";
		thisProjectItem[8].UniqueID = "28";
		thisProjectItem[9].UniqueID = "29";


		for(int i = 0 ; i < thisProjectItem.Length ; i++){

			projectItemList.Add (thisProjectItem [i]);
		}

		groupByOrderFrom ();
		displayGroupedItems ();
		//SortProjectItemList ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	List<string> OrderFrom_list = new List<string>();
	List<List<ProjectItem>> Group_projectItemList = new List<List<ProjectItem>>();

	void groupByOrderFrom(){

		for(int i = 0 ; i < projectItemList.Count ; i++){

			int checkResult = orderFrom_exist (projectItemList [i].OrderFrom);

			if (checkResult < 9999) {
				Group_projectItemList[checkResult].Add (projectItemList [i]);
			} else {			
				OrderFrom_list.Add(projectItemList[i].OrderFrom);
				List<ProjectItem> group_projectItem = new List<ProjectItem> ();
				group_projectItem.Add(projectItemList[i]);
				Group_projectItemList.Add (group_projectItem);
			}
		}
	

	}

	void displayGroupedItems(){

		float height_new = 0;
		int currentIndex_ID = 0;
		for(int i = 0 ; i < Group_projectItemList.Count ; i ++){
			GameObject thisGroupObj = Instantiate (groupItem, this.transform);
			thisGroupObj.SetActive (true);
			thisGroupObj.GetComponentInChildren<Text> ().text = OrderFrom_list [i];
			height_new += 20;
			for(int j = 0 ; j < Group_projectItemList[i].Count ; j++){
				GameObject ThisObject = Instantiate (projectItemPrefab, this.transform) as GameObject;
				ThisObject.SetActive (true);
				ProjectItemPrefabsManager targetPIPM = ThisObject.GetComponent<ProjectItemPrefabsManager> ();
				targetPIPM.assignItem (Group_projectItemList[i][j]);
				targetPIPM.indexID = currentIndex_ID;
				currentIndex_ID++;
				height_new += 20;
			}
		}

		targetRectTransform.sizeDelta = new Vector2 (targetRectTransform.rect.width,height_new);;
	}

	int orderFrom_exist(string inputString){
		
		for(int i = 0 ; i < OrderFrom_list.Count ; i++){

			if (inputString == OrderFrom_list [i])
				return i;
		}

		return 9999;
	}

	void SortProjectItemList(){
	
		projectItemList.Sort (SortByOrderFrom);

		for(int i = 0; i < projectItemList.Count ; i++){

			Debug.Log("Order Form " + i + " : " +projectItemList [i].OrderFrom + " UniqueID : " + projectItemList[i].UniqueID);

		}
	}

	static int SortByOrderFrom(ProjectItem p1, ProjectItem p2)
	{
		return p1.OrderFrom.CompareTo(p2.OrderFrom);
	}
}
