using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintingManager : MonoBehaviour {

	[SerializeField]
	GameObject[] hideWhenPrint;
	[SerializeField]
	GameObject do_button;
	[SerializeField]
	GameObject inv_button;

	[SerializeField]
	RectTransform targetRect_01; 
	[SerializeField]
	RectTransform targetRect_02; 
	[SerializeField]
	RectTransform targetRect_print;

	[SerializeField]
	GameObject loadingObj;

	// Use this for initialization
	void Start () {
		//float ratioHeight = (float)Screen.height / 842;

		//Screen.SetResolution ((int)(585 * ratioHeight),Screen.height,true);
		//System.Diagnostics.Process.Start("mspaint.exe", "/pt " + Application.dataPath +"\\UIpack\\sample.png");

	}

	public void PrintIt(){
		StartCoroutine (print_IE());
	}


	IEnumerator print_IE(){
		bool doButt_bool = do_button.activeSelf;
		bool invButt_bool = inv_button.activeSelf;
		RectTransform oldRectTrans = targetRect_01;

		targetRect_01.anchoredPosition = new Vector2 (0,0);
		targetRect_01.localRotation = new Quaternion (0,0,0.7f,0.7f);
		targetRect_01.localScale = new Vector3 (0.7f,0.7f,1);

		targetRect_02.anchoredPosition = new Vector2 (0,0);
		targetRect_02.localRotation = new Quaternion (0,0,0.7f,0.7f);
		targetRect_02.localScale = new Vector3 (0.7f,0.7f,1);

		Screen.SetResolution (842,590,false);
		for(int i = 0 ; i < hideWhenPrint.Length;i++){
			hideWhenPrint [i].SetActive (false);
		}

		do_button.SetActive (false);
		inv_button.SetActive (false);

		loadingObj.SetActive (true);
		yield return new WaitForSeconds (1);
		loadingObj.SetActive (false);
		Application.CaptureScreenshot(Application.dataPath + "\\ScreenShot.png");
		yield return new WaitForSeconds (1);

		System.Diagnostics.Process.Start("mspaint.exe",Application.dataPath + "\\ScreenShot.png");
		for(int i = 0 ; i < hideWhenPrint.Length;i++){
			hideWhenPrint [i].SetActive (true);
		}

		do_button.SetActive (doButt_bool);
		inv_button.SetActive (invButt_bool);

		targetRect_01.anchoredPosition = new Vector2 (1366,0);
		targetRect_01.localRotation = new Quaternion (0,0,0,0);
		targetRect_01.localScale = new Vector3 (0.39f,0.39f,1);

		targetRect_02.anchoredPosition = new Vector2 (1366,0);
		targetRect_02.localRotation = new Quaternion (0,0,0,0);
		targetRect_02.localScale = new Vector3 (0.39f,0.39f,1);

		Screen.orientation = ScreenOrientation.Landscape;
		Screen.SetResolution (1366,768,true);
	}
}
