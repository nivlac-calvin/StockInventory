using UnityEngine;
using System.Collections;

public class hideNshow : MonoBehaviour {

	[SerializeField]
	GameObject[] A_obj;
	[SerializeField]
	GameObject[] B_obj;
	// Use this for initialization
	void Start () {
		switch_side ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool side = false;

	public void switch_side(){

		side = !side;

		for (int i = 0; i < A_obj.Length; i++) {
			A_obj [i].SetActive (side);
		}

		for (int i = 0; i < B_obj.Length; i++) {
			B_obj [i].SetActive (!side);
		}
	}

	public void set_side(bool input_bool){

		side = input_bool;

		for (int i = 0; i < A_obj.Length; i++) {
			A_obj [i].SetActive (side);
		}

		for (int i = 0; i < B_obj.Length; i++) {
			B_obj [i].SetActive (!side);
		}
	}
}
