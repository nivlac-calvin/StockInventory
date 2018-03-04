using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ProjectRecords_Manager : MonoBehaviour {

	public static UI_ProjectRecords_Manager instance;

	void Awake(){
	
		instance = this;
	}

	[SerializeField]
	GameObject UI_prefab;
	[SerializeField]
	Transform targetParent;

	List<GameObject> prefabList = new List<GameObject>();
		
	public void generateList(ProjectRecordsDetails inputProject){
		destroyPrefabList ();
		instantiatePrefab (inputProject.projectName);

		for (int i = 0; i < inputProject.projectItem_List.Count; i++) {
		
			instantiatePrefab ( inputProject.projectItem_List[i]);

		}
	}

	public void instantiatePrefab(string inputString){

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
		
		if (inputIndex > 0) {
			Destroy (prefabList [inputIndex]);
			prefabList.RemoveAt (inputIndex);
		}
		
	}

	public void saveCurrentProjectRecords(){

		ProjectRecordsDetails thisProject = new ProjectRecordsDetails();
		thisProject.projectName = prefabList [0].GetComponent<UI_stringTab> ().myText.text;
		thisProject.projectItem_List = new List<string> ();

		for(int i = 1 ; i< prefabList.Count ; i++){

			thisProject.projectItem_List.Add (prefabList [i].GetComponent<UI_stringTab> ().myText.text);
		
		}

		ProjectRecords_Manager.instance.addNewProject (thisProject);
	}
}
