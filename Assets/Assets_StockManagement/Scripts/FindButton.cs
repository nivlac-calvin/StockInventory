using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FindButton : MonoBehaviour {

	public bool display = false;
	public InputField keywords_input;

	// Use this for initialization
	void Start () {
	
	}

	[SerializeField]
	Animator thisAnimator;


	public void button_Find(){
		
		if (display) {

			hideThis ();
		
		} else {		
			thisAnimator.SetTrigger ("displayTri");
			display = true;
		}	
	}

	public void hideThis(){
	
		thisAnimator.SetTrigger ("hideTri");
		display = false;

	}
}
