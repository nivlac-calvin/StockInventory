using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemNoController : MonoBehaviour {
	public int numOfItem;
	[SerializeField]
	GameObject noPrefab;
	public List<GameObject> prefabList;

	void Start(){
		for(int i = 0; i < numOfItem ; i++){
			instantiateNewPrefab ((i+1).ToString());
		}
	}

	void instantiateNewPrefab(string inputText){
		GameObject newObj = Instantiate (noPrefab,this.transform);
		newObj.SetActive (true);
		newObj.GetComponent<Text> ().text = inputText;
		prefabList.Add (newObj);
	}

	public List<int> indexsOfSelected(){
	
		List<int> newIntList = new List<int> ();
		for(int i = 0 ; i < prefabList.Count ; i++){
			if(prefabList[i].transform.GetChild(0).GetComponent<Toggle>().isOn)
				newIntList.Add (i);
		}
		return newIntList;
	}

	public void refreshSelection(){
		for(int i = 0 ; i < prefabList.Count ; i++){
			prefabList[i].transform.GetChild (0).GetComponent<Toggle> ().isOn = false;
		}
	}

}
