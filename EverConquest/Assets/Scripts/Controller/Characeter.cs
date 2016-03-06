using UnityEngine;
using System.Collections;

public class Characeter : MonoBehaviour  {

	public Pawn Body { get; set;}

	[SerializeField] float hp;
	[SerializeField] int area;
	[SerializeField] int range;
	[SerializeField] float atk;
	[SerializeField] float def;
	void Update() {
		if (Body!= null && Body.GetType () == typeof(Creature)) {
			Creature pawn_body = Body as Creature;
			hp = pawn_body.Hp;
			area = pawn_body.Area;
			range = pawn_body.Range;
			atk = pawn_body.Atk;
			def = pawn_body.Def;
		}

		if (Body!= null && Body.GetType () == typeof(Structure)) {
			Structure pawn_body = Body as Structure;
			hp = pawn_body.Hp;
			area = pawn_body.Area;
			range = pawn_body.Range;
			atk = pawn_body.Atk;
			def = pawn_body.Def;
		}
			
	}

}
