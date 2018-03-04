using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testText : MonoBehaviour {

	[SerializeField]
	InputField targetInput;
	bool isFull = false;
	int prevLength = 0;
	public bool isPrice;

	public void check(){
		
		if(targetInput.IsActive())
			StartCoroutine (removeOne());
	}

	IEnumerator removeOne(){
		yield return new WaitForEndOfFrame();

		if (isFull == true) {
			isFull = false;
			if(targetInput.text.Length > prevLength)
				targetInput.text = targetInput.text.Remove (targetInput.text.Length - 1);

			targetInput.DeactivateInputField ();
		} else {
			if (targetInput.textComponent.preferredWidth > GetComponent<RectTransform> ().sizeDelta.x - targetInput.textComponent.fontSize) {
				isFull = true;
				prevLength = targetInput.text.Length;
				targetInput.DeactivateInputField ();
				//targetInput.text = targetInput.text.Remove (targetInput.text.Length - 1);
			}
		}
	}

	public void reFormat(){
		if (isPrice) {
			float value = 0;
			float.TryParse(targetInput.text,out value);

			if (value == 0)
				targetInput.text = "";
			else
				targetInput.text = value.ToString("F2");
		}
	}
}
