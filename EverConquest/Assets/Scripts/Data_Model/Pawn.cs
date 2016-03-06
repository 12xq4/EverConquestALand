using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Pawn {

	protected int area;

	public int Area {
		get {
			return area;
		}
	}

	public int Loc_X { get; protected set; }
	public int Loc_Y { get; protected set; }
	protected World world;

	public float Hp { get; protected set;}
	int speed = 0;

	public int Speed {
		get {
			return speed;
		}
		protected set {
			speed = value;
		}
	}

	HashSet<Tile> tiles_under; // This keeps track the tile under a pawn object. 
							   // This would be 1 element set if pawn is a character
							   // then maybe a 7 element set if pawn is a larger creature, and so on.

	public HashSet<Tile> Tiles_under {
		get {
			return tiles_under;
		}
	}

	public Pawn (World world, int location_x, int location_y, float hp, int area = 0) {
		this.world = world;
		this.area = area;
		Loc_X = location_x;
		Loc_Y = location_y;

		Hp = hp;
		tiles_under = new HashSet<Tile> ();
		SetTileUnder ();
	}

	protected void SetTileUnder() {
		if (tiles_under != null) {
			foreach (Tile tl in tiles_under)
				tl.Owner = null;
			tiles_under.Clear ();
		}
		Tile t = world.GetTileAt (Loc_X, Loc_Y);
		if (area <= 0)
			tiles_under.Add (t);
		else
			tiles_under = t.GetNeighbours (area);
		foreach (Tile tile in tiles_under) {
			tile.Owner = this;
		}
	}

	public void TakDmg (float dmg) {
		Hp -= dmg;
		if (Hp <= 0) {
			Hp = 0;
			Debug.Log ("Object is dead.");
		}
	}
}
