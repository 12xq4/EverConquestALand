using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
    // ----------------------------------------- Visual component ------------------------------------------------------------
    public GameObject hex_tile;

    // ----------------------------------------- Logics component ------------------------------------------------------------
    // Keep a public instance of map here in case if other classes needs to refer to the map data.
    public static Map Map_Instance { get; protected set; } 
    // This is the world we use to represent our conceptual game world
    // The controller here populate our conceputal world with data and visual objects.
    public World World { get; protected set; }

	float width_offset = 1.8f; // a float offset to consider the width of each hexigon, to place them in the correct position.
	float x_offset = 0.9f; // this is only applied once for every two rows
	float z_offset = 1.57f; // this will be applied for every row.

	// Use this for initialization
	void Start () {
        World = new World();

		for (int x = 0; x < World.Width; x++) {
			for (int z = 0; z < World.Height; z++) {
                Tile tile_data = World.GetTileAt(x, z);
                GameObject hex_current = new GameObject();
                hex_current.name = "Hex (" + x + "," + z + ")";
                if (z % 2 == 1)
					hex_current.transform.position = new Vector3(x * width_offset + x_offset,0,z * z_offset);
				else
                    hex_current.transform.position = new Vector3(x * width_offset,0,z * z_offset);

                hex_current.isStatic = true;
                hex_current.transform.SetParent(this.transform, true);

                // Register the callback function for tiles here, using the redisplay method in Map.
                tile_data.RegisterTileRedisplayCallback((tile)=> { OnHexRedisplay(tile, hex_current); });
            }
		}
        World.SetTilesAtRandom();
	}
	
	// Update is called once per frame
	void Update () {
			
	}

    void OnHexRedisplay(Tile tile_data, GameObject tile_current)
    {
        if (tile_data.Type == Tile.TileType.Empty)
        {
            tile_current.transform.FindChild(hex_tile.name).gameObject.SetActive(false);
        }
            
        else if (tile_data.Type == Tile.TileType.Terrian)
        {
            // Just do this right now as we only have 1 type of terrian. Later on we could add more types of terrian tiles, and
            // change the MeshRender depending on the type of terrian that is presented.
            if (tile_current.transform.FindChild(hex_tile.name) == null)
            {
                GameObject hex = (GameObject)Instantiate(hex_tile, tile_current.transform.position, Quaternion.identity);
                hex.transform.SetParent(tile_current.transform, true);
            }
        }

        else
            Debug.Log("There is an error in the tile type");
    }
}
	