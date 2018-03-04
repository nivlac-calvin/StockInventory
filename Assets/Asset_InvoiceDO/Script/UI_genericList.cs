using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_genericList : MonoBehaviour {
	public string ListName;
	public List<string> currentStringList = new List<string>();

	[SerializeField]
	GameObject UI_prefab;
	[SerializeField]
	Transform targetParent;
	List<GameObject> prefabList = new List<GameObject>();

	public void generateList(List<string> inputStringList){
		Debug.Log ("GenerateList");
		currentStringList.Clear ();
		currentStringList = inputStringList;
		RefreshList ();
	}

	void RefreshList(){
		destroyPrefabList ();

		for (int i = 0; i < currentStringList.Count; i++) {

			instantiatePrefab (currentStringList[i]);

		}
	}

	public void AddNewPrefab(string inputString){
	
		currentStringList.Add (inputString);
		instantiatePrefab (inputString);
	}

	void instantiatePrefab(string inputString){
		GameObject newObj = Instantiate (UI_prefab,targetParent);
		newObj.SetActive (true);
		newObj.GetComponent<UI_stringTab> ().setText (inputString);
		newObj.GetComponent<UI_stringTab> ().index = prefabList.Count;
		prefabList.Add (newObj);
	}

	void destroyPrefabList(){

		for(int i = 0 ; i < prefabList.Count ; i++){

			Destroy (prefabList[i]);
		}

		prefabList.Clear ();
	}
		
	public void removeItem(int inputIndex){

		currentStringList.RemoveAt (inputIndex);
		RefreshList ();
	}
		

	public void modified(int inputIndex,string inputString){

		currentStringList [inputIndex] = inputString;
		if (inputIndex == currentStringList.Count - 1 && inputString.Trim() != "") {
			AddNewPrefab ("");
		}
	
	}
}
