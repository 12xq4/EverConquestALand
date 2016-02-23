using UnityEngine;
using System.Collections;

public class Hex : MonoBehaviour {

	public Tile tile_rep;
	enum Neighbours {Left = 0, Right, TopLeft, TopRight, BotLeft, BotRight};

	public Tile[] GetNeighbours() {
		Tile[] tiles = new Tile[6];
		int x_co = tile_rep.XCoord;
		int y_co = tile_rep.YCoord;

		tiles[(int)Neighbours.Left] = Map.Map_Instance.World.GetTileAt (x_co-1, y_co);
		tiles[(int)Neighbours.Right] = Map.Map_Instance.World.GetTileAt (x_co + 1, y_co);
		if (x_co % 2 == 0) {
			tiles[(int)Neighbours.TopLeft] = Map.Map_Instance.World.GetTileAt (x_co - 1, y_co + 1);
			tiles[(int)Neighbours.TopRight] = Map.Map_Instance.World.GetTileAt (x_co, y_co + 1);
			tiles[(int)Neighbours.BotLeft] = Map.Map_Instance.World.GetTileAt (x_co - 1, y_co - 1);
			tiles[(int)Neighbours.BotRight] = Map.Map_Instance.World.GetTileAt (x_co, y_co - 1);
		} else {
			tiles[(int)Neighbours.TopLeft] = Map.Map_Instance.World.GetTileAt (x_co, y_co + 1);
			tiles[(int)Neighbours.TopRight] = Map.Map_Instance.World.GetTileAt (x_co + 1, y_co + 1);
			tiles[(int)Neighbours.BotLeft] = Map.Map_Instance.World.GetTileAt (x_co, y_co - 1);
			tiles[(int)Neighbours.BotRight]= Map.Map_Instance.World.GetTileAt (x_co + 1, y_co - 1);
		}

		return tiles;	
	}
}
