using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	void Start () {
        World = new World();

		for (int x = 0; x < World.Width; x++) {
			for (int z = 0; z < World.Height; z++) {
                Tile tile_data = World.GetTileAt(x, z);
				Vector3 pos;
                if (z % 2 == 1)
					pos = new Vector3(x * width_offset + x_offset,0,z * z_offset);
				else
					pos = new Vector3(x * width_offset,0,z * z_offset);
				GameObject hex_current = (GameObject) Instantiate(hex_tile, pos, Quaternion.identity);
				hex_current.name = "Hex (" + x + "," + z + ")";
                hex_current.isStatic = true;
				// Add a hex component for each hex tile at run time.
				// For ease of reference relativity.
				hex_current.AddComponent<Hex> ();
				hex_current.GetComponent<Hex> ().tile_rep = tile_data;
				// Arrange in a clean hierachy, set parent of all hex tiles to the map.
                hex_current.transform.SetParent(this.transform, true);

                // Register the callback function for tiles here, using the redisplay method in Map.
                tile_data.RegisterTileRedisplayCallback((tile)=> { OnHexRedisplay(tile, hex_current); });
            }
		}
        World.SetTilesAtRandom();

		// for testing below, generate a player pawn.
		GameObject player = GameObject.Find("Pawn (0,0)");
		player.GetComponent<Character> ().Body = new Creature (World, 0, 0, 40, Creature.ArmorType.Cloth, Creature.WeaponType.Light, 8, 4, 0, 3, 3);
		GameObject o_pawn = GameObject.Find ("Hex (" + 0 + "," + 0 + ")");
		player.transform.position = o_pawn.transform.position;

		GameObject enemy = GameObject.Find("Pawn (4,4)");
		enemy.GetComponent<Character> ().Body = new Creature (World, 4, 4, 60, Creature.ArmorType.Mail, Creature.WeaponType.Piercing, 10, 1, 0, 2);
		GameObject o_pawn2 = GameObject.Find ("Hex (" + 4 + "," + 4 + ")");
		enemy.transform.position = o_pawn2.transform.position;

		GameObject structure = GameObject.Find("Pawn (8,8)");
		structure.GetComponent<Character> ().Body = new Structure (World, 8, 8, 60, 8, 5, 1, 2);
		GameObject o_pawn3 = GameObject.Find ("Hex (" + 8 + "," + 8 + ")");
		structure.transform.position = o_pawn3.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
			
	}

    void OnHexRedisplay(Tile tile_data, GameObject tile_current)
    {
		if (tile_data.Type == Tile.TileType.Empty) {
			tile_current.GetComponentInChildren<MeshRenderer> ().enabled = false;
		} else if (tile_data.Type == Tile.TileType.Terrian) {
			// Just do this right now as we only have 1 type of terrian. Later on we could add more types of terrian tiles, and
			// change the MeshRender depending on the type of terrian that is presented.
			tile_current.GetComponentInChildren<MeshRenderer> ().enabled = true;
		}
        else
            Debug.Log("There is an error in the tile type");
    }
}
	