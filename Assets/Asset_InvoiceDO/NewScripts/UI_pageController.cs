using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_pageController : MonoBehaviour {

	public int invoiceID;
	InvoiceRecords_Details thisInvoiceData = new InvoiceRecords_Details();

	void OnEnable(){	
		StartCoroutine (LoadCoroutine());
	}

	IEnumerator LoadCoroutine(){

		while (DataManager_New.instance == null) {
			yield return null;
		}

		while(!DataManager_New.instance.loaded_partner){
			yield return null;
		}

		thisInvoiceData = DataManager_New.instance.invoiceList[invoiceID];

		refresh ();
	}

	public void Reload(){
		StartCoroutine (LoadCoroutine());
	}

	[SerializeField]
	GameObject pagePrefab;

	public List<GameObject> pageList = new List<GameObject>();
	public List<Data_Page> pagesData_List = new List<Data_Page>();

	int currentPage;
	int totalPage;

	void refresh(){
		
		for(int i = 0; i< pageList.Count ; i++){
			pageList [i].SetActive (false);
		}

		pagesData_List.Clear ();

		for(int i = 0 ; i <thisInvoiceData.itemDetails_List.Count ; i++){

			if (i > (pageList.Count-1)) {
				GameObject newObj = Instantiate (pagePrefab, this.transform);
				newObj.GetComponent<Data_Page> ().thisPage_id = i;
				newObj.GetComponent<Data_Page> ().thisPage_invoiceID = invoiceID;
				newObj.GetComponent<Data_Page> ().startLoad ();
				pageList.Add (newObj);
			} else {
				pageList [i].GetComponent<Data_Page> ().thisPage_id = i;
				pageList [i].GetComponent<Data_Page> ().thisPage_invoiceID = invoiceID;
				pageList [i].GetComponent<Data_Page> ().startLoad ();
			}

			pagesData_List.Add (pageList[i].GetComponent<Data_Page>());

		}
		totalPage = pagesData_List.Count;

		if (currentPage > (totalPage - 1))
			currentPage = totalPage - 1;
			
			
		pageList [currentPage].SetActive (true);
		projectTitle.text = thisInvoiceData.invoiceID +"  "+ thisInvoiceData.projectTitle;

		pageNum_text.text = (currentPage+1)+ " / " + totalPage;

	}

	public void addPage(){
		saveAll ();
		DataManager_New.instance.addNewPage(invoiceID);
		currentPage ++;

		thisInvoiceData = DataManager_New.instance.invoiceList[invoiceID];

		refresh ();
	}

	public void deletePage(){
		if (pageList.Count > 1) {
			saveAll ();
			DataManager_New.instance.invoiceList[invoiceID].itemDetails_List.RemoveAt(currentPage);
			DataManager_New.instance.saveCurrentInvoice ();
			thisInvoiceData = DataManager_New.instance.invoiceList[invoiceID];

			refresh ();
		}
	}

	public void saveAll(){
		pagesData_List [currentPage].takeFromUI ();
		pagesData_List [currentPage].saveThisPage ();

	}

	public void clearThisPage(){
		pagesData_List [currentPage].clearThisPage ();
	}

	void rename(){
		for(int i = 0 ; i< pageList.Count ; i++){
			pageList[i].name = "PAGE" + (i+1);
		}
	}

	[SerializeField]
	Text pageNum_text;

	public void nextPage(){
		saveAll ();
		if (currentPage < totalPage - 1) {
			for(int i = 0 ; i < pageList.Count ; i++){
				pageList [i].SetActive (false);
			}
			currentPage++;
			pageList [currentPage].SetActive (true);
		}
		refreshPageText ();	
	}

	public void prevPage(){
		saveAll ();
		if (currentPage > 0) {
			for(int i = 0 ; i < pageList.Count ; i++){
				pageList [i].SetActive (false);
			}
			currentPage--;
			pageList [currentPage].SetActive (true);
		}
		refreshPageText ();	
	}

	void refreshPageText(){
		pagesData_List.Clear ();

		for(int i = 0 ; i< pageList.Count ; i++){
			pagesData_List.Add (pageList[i].GetComponent<Data_Page>());
		}
		pageNum_text.text = (currentPage+1)+ " / " + totalPage;
	}

	public void deleteSelected(){
		pagesData_List [currentPage].deleteSelected ();
	}

	public void clearPage(){
		pagesData_List [currentPage].clearThisPage ();
	}

	[SerializeField]
	Text projectTitle;

	[SerializeField]
	UI_Invoice_Preview inv_Previewer;
	public void buttonPreview(){
		if (!inv_Previewer.gameObject.activeSelf) {
			inv_Previewer.gameObject.SetActive (true);
			inv_Previewer.InvoiceId = invoiceID;
			inv_Previewer.startLoad ();
		} else
			inv_Previewer.gameObject.SetActive (false);

	}
}
