using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public delegate void ClickEvent();
	public static event ClickEvent OnClicked;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
