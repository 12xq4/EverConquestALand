using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	public GameObject hextile;

	// size of the map in terms of number of hex tiles
	// not representitive of the amount of world space taken up.
	// i.e. tile might be more or less than 1 Unity world unit.
	int width = 20;
	int height = 20;

	float width_offset = 1.8f; // a float offset to consider the width of each hexigon, to place them in the correct position.
	float x_offset = 0.9f; // this is only applied once for every two rows
	float z_offset = 1.57f; // this will be applied for every row.

	// Use this for initialization
	void Start () {
		for (int x = 0; x < width; x++) {
			for (int z = 0; z < height; z++) {
				if (z % 2 == 1)
					Instantiate (hextile, new Vector3(x * width_offset + x_offset,0,z * z_offset), Quaternion.identity);
				else 
					Instantiate (hextile, new Vector3(x * width_offset,0,z * z_offset), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
			
	}
}
	