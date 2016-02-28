using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pawn {

	int area;
	public int Loc_X { get; protected set; }
	public int Loc_Y { get; protected set; }
	World world;

	HashSet<Tile> tiles_under; // This keeps track the tile under a pawn object. 
							   // This would be 1 element set if pawn is a character
							   // then maybe a 7 element set if pawn is a larger creature, and so on.

	public HashSet<Tile> Tiles_under {
		get {
			return tiles_under;
		}
	}

	public Pawn (World world, int area, int location_x, int location_y) {
		this.world = world;
		this.area = area;
		Loc_X = location_x;
		Loc_Y = location_y;

		Tile t = world.GetTileAt (Loc_X, Loc_Y);
		if (area == 0)
			tiles_under.Add (t);
		else
			tiles_under = t.GetNeighbours (area - 1);
	}
}
