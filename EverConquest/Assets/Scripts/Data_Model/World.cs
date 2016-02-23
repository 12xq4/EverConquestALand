using UnityEngine;
using System.Collections;

public class World {
    Tile[,] tiles;


	public int Width { get; set;}
	public int Height { get; set;}

	public World (int width = 20, int height = 20) {
		Width = width;
		Height = height;
        // Create this world as specfied by the width and height attribute.
        tiles = new Tile[Width, Height];
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
            }
        }
	}
    public void SetTilesAtRandom()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (Random.Range(1, 2) == 0)
                    tiles[x, y].Type = Tile.TileType.Empty;
                else
                    tiles[x, y].Type = Tile.TileType.Terrian;
            }
        }
    }

    public Tile GetTileAt(int x, int y)
    {
        if (x > Width || x  < 0 || y > Height || y < 0)
        {
            Debug.Log("Check your coordinates, you requested a tile that is out of bound.");
        }
        return tiles[x, y];
    }
}
