using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour  {

	public delegate void ChangeUI (float hp, string name, int speed, float atk, float def, Sprite pic = null);
	public static ChangeUI OnUIChange;

	public delegate void CancelUI ();
	public static CancelUI OnUIWiped;

	public Pawn Body { get; set;}
	public string name;
	public Sprite Icon;
	[SerializeField] float hp;
	[SerializeField] int area;
	[SerializeField] int range;
	[SerializeField] float atk;
	[SerializeField] float def;
	int speed;

	GameObject ui;

	void Start() {
		ui = GameObject.FindGameObjectWithTag ("Char Info");
	}

	void Update() {
		if (Body!= null && Body.GetType () == typeof(Creature)) {
			Creature pawn_body = Body as Creature;
			hp = pawn_body.Hp;
			area = pawn_body.Area;
			range = pawn_body.Range;
			atk = pawn_body.Atk;
			def = pawn_body.Def;
			speed = pawn_body.Speed;
		}

		if (Body!= null && Body.GetType () == typeof(Structure)) {
			Structure pawn_body = Body as Structure;
			hp = pawn_body.Hp;
			area = pawn_body.Area;
			range = pawn_body.Range;
			atk = pawn_body.Atk;
			def = pawn_body.Def;
			speed = 0;
		}
			
	}

	void OnMouseOver () {
		if (OnUIChange != null)
			OnUIChange (hp, name, speed, atk, def, Icon);
	}

	void OnMouseExit () {
		if (OnUIWiped != null)
			OnUIWiped ();
	}
}
