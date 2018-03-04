using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_DoubleClick : MonoBehaviour {
	[SerializeField]
	UnityEvent action;


	public void buttonClick(){

		StartCoroutine (ClickCount_IE());
	}

	int ClickCounter = 0;
	IEnumerator ClickCount_IE(){

		ClickCounter++;

		if (ClickCounter > 1) {
			ClickCounter = 0;
			action.Invoke();
			yield return null;
		} else {
			yield return new WaitForSeconds (0.5f);
			if(ClickCounter > 0)
				ClickCounter--;
		}
	}
}
