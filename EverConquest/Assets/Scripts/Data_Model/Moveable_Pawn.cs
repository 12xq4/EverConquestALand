using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Moveable_Pawn : Pawn, Moveable {

	public int MoveStep { get; protected set;}

	public Moveable_Pawn(World world, int location_x, int location_y, int area, int movement = 1) : base(world, location_x, location_y, area)
	{
		MoveStep = movement;
	}

	public HashSet<Tile> GenerateMoves() {
		// Generate a list of possible hex to move to
		Tile current_t = world.GetTileAt(Loc_X, Loc_Y);
		HashSet<Tile> moves = current_t.GetNeighbours (MoveStep);
		return moves;
	}

	public void Move(Tile t) {
		Loc_X = t.XCoord;
		Loc_Y = t.YCoord;

		// Recalculate the tiles that are currently under this Pawn
		SetTileUnder();
	}
}
