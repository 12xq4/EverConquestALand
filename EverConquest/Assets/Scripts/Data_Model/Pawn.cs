using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Pawn {

	protected int area;
	public int Loc_X { get; protected set; }
	public int Loc_Y { get; protected set; }
	protected World world;

	HashSet<Tile> tiles_under; // This keeps track the tile under a pawn object. 
							   // This would be 1 element set if pawn is a character
							   // then maybe a 7 element set if pawn is a larger creature, and so on.

	public HashSet<Tile> Tiles_under {
		get {
			return tiles_under;
		}
	}

	public Pawn (World world, int location_x, int location_y, int area = 1) {
		this.world = world;
		this.area = area;
		Loc_X = location_x;
		Loc_Y = location_y;

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
		if (area <= 1)
			tiles_under.Add (t);
		else
			tiles_under = t.GetNeighbours (area - 1);
		foreach (Tile tile in tiles_under) {
			tile.Owner = this;
		}
	}
}
