using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Tile {
	public enum TileType {Empty, Terrian} ;
	// local variables
	TileType type;

    // callback has been handled here to attach a redisplay function.
    // we intercept each time a tile type has been reset, then call the callback here 
    // to change the graphics component of the tile, as well as changing the property of the tile.
    Action<Tile> cb_tileRedisplay;

    public TileType Type
    {
        get
        {
            return type;
        }
        set
        {
            TileType oldType = type;    // this is added ot keep track if a tile type has been changed.
                                        // if tile actually changed, call redisplay method.
            type = value;
            // make sure the type has been changed, then call callback function.
            if (oldType != type && cb_tileRedisplay != null)
                cb_tileRedisplay(this);
        }
    }
    
	World world;

	public World World {
		get {
			return world;
		}
		protected set {
			world = value;
		}
	}

    public int XCoord { get; protected set; }
    public int YCoord { get; protected set; }

    public Tile (World world, int x, int y) {
	    World = world;
		XCoord = x;
		YCoord = y;
	}

	public HashSet<Tile> GetNeighbours(int depth = 0) {
		HashSet<Tile> tiles = new HashSet<Tile>();
		Tile[] result;
		tiles.Add (this);
		if (XCoord-1 >= 0)
			tiles.Add (world.GetTileAt (XCoord-1, YCoord));
		if (XCoord+1 <= world.Width)
			tiles.Add (world.GetTileAt (XCoord + 1, YCoord));
		if (YCoord % 2 == 0) {
			if (XCoord-1 >= 0 && YCoord+1 <= world.Height)
				tiles.Add (world.GetTileAt (XCoord - 1, YCoord + 1));
			if (YCoord+1 <= world.Height)
				tiles.Add (world.GetTileAt (XCoord, YCoord + 1));
			if (XCoord-1 >= 0 && YCoord-1 >= 0)
				tiles.Add (world.GetTileAt (XCoord - 1, YCoord - 1));
			if (YCoord-1 >= 0)
				tiles.Add (world.GetTileAt (XCoord, YCoord - 1));
		} else {
			if (YCoord+1 <= world.Height)
				tiles.Add (world.GetTileAt (XCoord, YCoord + 1));
			if (XCoord+1 <= world.Width && YCoord+1 <= world.Height)
				tiles.Add (world.GetTileAt (XCoord + 1, YCoord + 1));
			if (YCoord-1 >= 0)
				tiles.Add (world.GetTileAt (XCoord, YCoord - 1));
			if (YCoord+1 <= world.Height && YCoord-1 >= 0)
				tiles.Add (world.GetTileAt (XCoord + 1, YCoord - 1));
		}
		if (depth <= 0)
			return tiles;
		else {
			foreach (Tile t in tiles) {
				tiles.UnionWith (t.GetNeighbours(depth-1));
			}
			return tiles;
		}
		// return (Tile[]) tiles.ToArray( typeof (Tile));	
	}

    public void RegisterTileRedisplayCallback (Action<Tile> callback)
    {
        // This is where we actually tie a method to cb_tileRedisplay callback.
        // += here means to put it on the list of things/methods to run when this callback is executed.
        cb_tileRedisplay += callback;
    }
}
