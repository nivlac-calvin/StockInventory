using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputFieldGenerator : MonoBehaviour {
	public int numOfItem;
	public List<string> currentStringList = new List<string> ();
	public List<GameObject> prefabList;
	[SerializeField]
	GameObject input_prefab;
	[SerializeField]
	Transform targetParent;

	public void newEmptyList(){
		currentStringList.Clear ();
		for(int i = 0 ; i < numOfItem ; i++){
			currentStringList.Add ("");
		}
		RefreshList ();
	}

	void Update(){
		if(transform.parent.gameObject.activeSelf){
			if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
				updateStringList ();
			}
		}
	}

	public void generateList(List<string> inputStringList){
		currentStringList.Clear ();
		currentStringList = inputStringList;
		RefreshList ();
	}

	public void RefreshList(){	
		for(int i = 0 ; i< prefabList.Count ; i++){
			prefabList[i].SetActive (false);
		}

		for (int i = 0; i < currentStringList.Count; i++) {

			if (i < prefabList.Count) {
				prefabList[i].SetActive (true);
				prefabList[i].GetComponent<InputField> ().text = currentStringList[i];
			} else {
				instantiateNewPrefab (currentStringList[i]);
			}
		}
	}

	void instantiateNewPrefab(string inputString){
		GameObject newObj = Instantiate (input_prefab,targetParent);
		newObj.SetActive (true);
		newObj.GetComponent<InputField> ().text = inputString;
		prefabList.Add (newObj);
	}

	public void updateStringList(){
		Debug.Log (this.gameObject.name + " Enter");
		for(int i = 0 ; i< prefabList.Count; i++){
			currentStringList [i] = prefabList [i].GetComponent<InputField>().text;
		}
	}

	public void removeItem(int index){
		currentStringList.RemoveAt (index);
		currentStringList.Add ("");
		RefreshList ();	
	}

	public void clearList(){
	
		for(int i = 0 ; i< currentStringList.Count; i++){
			currentStringList [i] = "";
		}
		RefreshList ();
	}
}
