using UnityEngine;
using System.Collections;

public class Characeter : MonoBehaviour  {

	public Pawn Body { get; set;}

	[SerializeField] float hp;
	[SerializeField] int range;

	void Start() {
		if (Body.GetType () == typeof(Creature)) {
			Creature pawn_body = Body as Creature;
			hp = pawn_body.Hp;
			range = pawn_body.Range;

		}
			
	}

}
