using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class dropdown_controller : MonoBehaviour {

	List<string> OptionList = new List<string>();
	[SerializeField]
	GameObject targetDropdown_Obj;
	Dropdown targetDropDown;
	[SerializeField]
	GameObject inputNew_Obj;

	[SerializeField]
	Text targetText;

	// Use this for initialization
	void Start () {
		baseOptionList.Add ("Option 1");
		baseOptionList.Add ("Option 2");

		targetDropDown = targetDropdown_Obj.GetComponentInChildren<Dropdown> ();
		targetDropDown.onValueChanged.AddListener(delegate {
			myDropdownValueChangedHandler(targetDropDown);
		});
		loadToOptionList ();	
	}

	private void myDropdownValueChangedHandler(Dropdown target) {
		Debug.Log("selected: "+target.value + " String : " + target.captionText.text + "   Count : " + target.options.Count);

		if (target.value == (target.options.Count - 1 )) {
			inputNew_Obj.SetActive (true);
		} else {
			StartCoroutine (waitNhide_dropdown());
		}
	}

	IEnumerator waitNhide_dropdown(){

		targetText.text = targetDropDown.captionText.text;
		yield return new WaitForSeconds (0.3f);
		targetDropdown_Obj.SetActive (false);
		Debug.Log ("Hide Complete");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	List<string> baseOptionList = new List<string>();

	public void addNewOption(){
		string newOption = inputNew_Obj.GetComponent<InputField> ().text;
		if (newOption != " " || newOption != null) {
			baseOptionList.Add (newOption);
		}
		loadToOptionList ();
		inputNew_Obj.SetActive (false);
		StartCoroutine (waitNhide_dropdown ());
	}

	void loadToOptionList(){
		OptionList.Clear ();
		if (baseOptionList.Count > 0) {

			for(int i = 0; i < baseOptionList.Count; i++){
				OptionList.Add (baseOptionList [i]);
			}
		}
		OptionList.Add ("Add New");
		updateDropdownOption ();
	
	}

	void updateDropdownOption(){

		if (OptionList.Count > 0) {
		
			targetDropDown.ClearOptions ();
			targetDropDown.AddOptions (OptionList);
		}
	}

	//UI control

	int pressCounter = 0;
	public void ButtonPress(){
	
		if (pressCounter > 0) {
			displayDropdown ();
		} else {		
			StartCoroutine (countDown());
		}
	}

	IEnumerator countDown(){

		pressCounter++;
		yield return new WaitForSeconds (1);
		pressCounter --;
	}


	void displayDropdown(){	
		targetDropdown_Obj.SetActive (true);
		loadToOptionList ();
	}
}
