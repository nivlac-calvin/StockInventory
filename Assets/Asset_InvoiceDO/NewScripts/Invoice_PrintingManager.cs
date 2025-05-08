using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invoice_PrintingManager : MonoBehaviour {

	[SerializeField]
	GameObject[] hideWhenPrint;

	[SerializeField]
	RectTransform targetRect_01; 

	[SerializeField]
	GameObject loadingObj;

	// Use this for initialization
	void Start () {
		//float ratioHeight = (float)Screen.height / 842;

		//Screen.SetResolution ((int)(585 * ratioHeight),Screen.height,true);
		//System.Diagnostics.Process.Start("mspaint.exe", "/pt " + Application.dataPath +"\\UIpack\\sample.png");

	}

	void Update(){
	
		if(Input.GetKey(KeyCode.F11)){
			resetResolution ();
		}
	
	}
		
	public void PrintIt(){
		StartCoroutine (print_IE());
	}


	IEnumerator print_IE(){
		RectTransform oldRectTrans = targetRect_01;

		targetRect_01.anchoredPosition = new Vector2 (0,0);
		targetRect_01.localRotation = new Quaternion (0,0,0.7f,0.7f);
		targetRect_01.localScale = new Vector3 (0.7f,0.7f,1);

		Screen.SetResolution (1096,768,false);
		for(int i = 0 ; i < hideWhenPrint.Length;i++){
			hideWhenPrint [i].SetActive (false);
		}

		loadingObj.SetActive (true);
		yield return new WaitForSeconds (1);
		loadingObj.SetActive (false);
		ScreenCapture.CaptureScreenshot(Application.dataPath + "\\ScreenShot.png");
		yield return new WaitForSeconds (1);

		System.Diagnostics.Process.Start("mspaint.exe",Application.dataPath + "\\ScreenShot.png");
		for(int i = 0 ; i < hideWhenPrint.Length;i++){
			hideWhenPrint [i].SetActive (true);
		}

		targetRect_01.anchoredPosition = new Vector2 (1366,0);
		targetRect_01.localRotation = new Quaternion (0,0,0,0);
		targetRect_01.localScale = new Vector3 (0.39f,0.39f,1);

		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.SetResolution (1366,768,true);
	}

	public void resetResolution(){
		loadingObj.SetActive (true);
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.SetResolution (1366,768,true);	
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
}