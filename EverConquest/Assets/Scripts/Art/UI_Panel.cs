using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Panel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable() {
		Character.OnUIChange += DrawUIComponent;
		Character.OnUIWiped += WipeUIComponent;
	}

	void OnDisable(){
		Character.OnUIChange -= DrawUIComponent;
		Character.OnUIWiped -= WipeUIComponent;
	}

	void DrawUIComponent(float hp, string name, int speed, float atk, float def, Sprite img){
		transform.FindChild ("HP").transform.GetComponent<Text> ().text = (hp + "");
		transform.FindChild ("Name").transform.GetComponent<Text> ().text = name;
		transform.FindChild ("Speed").transform.GetComponent<Text> ().text = speed + "";
		transform.FindChild ("Atk").transform.GetComponent<Text> ().text = atk + "";
		transform.FindChild ("Def").transform.GetComponent<Text> ().text = def + "";
		transform.FindChild ("Image").transform.GetComponent<Image> ().sprite = img;
	}

	void WipeUIComponent(){
		transform.FindChild ("HP").transform.GetComponent<Text> ().text = "";
		transform.FindChild ("Name").transform.GetComponent<Text> ().text = "";
		transform.FindChild ("Speed").transform.GetComponent<Text> ().text = "";
		transform.FindChild ("Atk").transform.GetComponent<Text> ().text = "";
		transform.FindChild ("Def").transform.GetComponent<Text> ().text = "";
		transform.FindChild ("Image").transform.GetComponent<Image> ().sprite = null;
	}
}
