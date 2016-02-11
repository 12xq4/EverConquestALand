using UnityEngine;
using System.Collections;

public class Tile {
	public enum TileType {} ;

	// local variables
	TileType type;
	protected int X_coord { get; set;}
	protected int Y_coord { get; set;}

	public Tile (TileType type, int x, int y) {
		this.type = type;
		X_coord = x;
		Y_coord = y;
	}
}
