using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Moveable_Pawn : Pawn, Moveable {

	public int MoveStep { get; protected set;}

	public Moveable_Pawn(World world, int location_x, int location_y, float hp, int area, int movement = 1) : base(world, location_x, location_y, hp, area)
	{
		MoveStep = movement;
	}

	public HashSet<Tile> GenerateMoves() {
		// Generate a list of possible hex to move to
		Tile current_t = world.GetTileAt(Loc_X, Loc_Y);
		HashSet<Tile> moves = current_t.GetNeighbours (MoveStep + area);
		/*
		foreach (Tile t in Tiles_under)
			moves.Remove (t);
		*/
		moves.RemoveWhere (s => s.Owner != null);
		return moves;
	}

	public void Move(Tile t) {
		Loc_X = t.XCoord;
		Loc_Y = t.YCoord;

		// Recalculate the tiles that are currently under this Pawn
		SetTileUnder();
	}
}
