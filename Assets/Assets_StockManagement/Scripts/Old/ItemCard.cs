using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemCard : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public Item targetItem;
	public int indexID;

	[SerializeField]
	Text IndexID;
	[SerializeField]
	Text BrandText;
	[SerializeField]
	Text PartNoText;
	[SerializeField]
	Text DescriptionText;

	[SerializeField]
	Text qtyAll_Text;
	[SerializeField]
	Text U_Price_Text;
	[SerializeField]
	Text T_Price_Text;
	[SerializeField]
	GameObject hightlight_obj;

	[SerializeField]
	EditPanelManager EditMng;

	public DataManager thisDataMng;


	public void refreshCard(){
		targetItem.calculatePrice ();

		IndexID.text = indexID.ToString();
		BrandText.text = targetItem.brand_name;
		PartNoText.text = targetItem.part_no;
		DescriptionText.text = targetItem.description;

		qtyAll_Text.text = targetItem.qty_all.ToString ();
		U_Price_Text.text = targetItem.U_price.ToString ();
		T_Price_Text.text = targetItem.T_price.ToString ();

		if (targetItem.qty_all <= targetItem.alert_qty) {
			hightlight_obj.SetActive (true);
		} else {		
			hightlight_obj.SetActive (false);
		}
		thisDataMng.saveThis ();

	}

	public void edit(){
		EditMng.gameObject.SetActive (true);
		EditMng.thisTarget = this;
		EditMng.displayItem ();
	}

}
