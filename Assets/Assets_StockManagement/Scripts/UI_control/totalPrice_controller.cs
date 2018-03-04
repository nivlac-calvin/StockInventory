using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class totalPrice_controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable(){
	
		Recalculate ();
	}

	[SerializeField]
	Text qtyText;
	[SerializeField]
	Text U_priceText;
	[SerializeField]
	Text T_priceText;

	public void Recalculate(){
	
		float totalPrice = float.Parse (qtyText.text) * float.Parse (U_priceText.text);
		T_priceText.text = totalPrice.ToString ("F2");
	}

}
