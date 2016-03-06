using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Structure : Pawn {

	public float Atk { get; protected set;}
	public int Range { get; protected set;}
	public float Def { get; protected set;}
	public HashSet<Creature> Spawns;
	public HashSet<Tile> Spawn_Locations;

	public Structure (World world, int location_x, int location_y, float hp, float atk, float def, int area, int range = 1) 
		: base(world, location_x, location_y, hp, area){

		Atk = atk;
		Def = def;
		Range = range;
		Speed = 0;
		SetTileUnderSolid ();
		SpawnLocation ();
	}

	public HashSet<Tile> GenerateRange() {
		// Generate a list of possible hex to move to
		Tile current_t = world.GetTileAt(Loc_X, Loc_Y);
		HashSet<Tile> range = current_t.GetNeighbours (Range + area);
		foreach (Tile t in Tiles_under)
			range.Remove (t);
		return range;
	}

	public bool CheckReach (Pawn other) {
		HashSet<Tile> reachable_tile = GenerateRange(); 
		bool reachable = false;
		foreach (Tile t in other.Tiles_under) {
			if (reachable_tile.Contains (t)) {
				reachable = true;
				break;
			}
		}
		return reachable;
	}

	/**
	 * This will calculate the damage dealt to a certain pawn
	 * Structure has a more straight forward damage calculation since they dont 
	 * have Weapon and Armor type.
	 */

	public void Attack (Pawn other) {
		// Check if the target is in range
		if (CheckReach(other)) {
			other.TakDmg (Atk);
		} else
			Debug.Log ("Invalid target.");
	}

	public void TakDmg (float dmg) {
		float rand = (float)(Random.value);

		dmg -= Def * rand;
		if (dmg < 0)
			dmg = 0;
		Hp -= dmg;
		Hp = Mathf.Round(Hp * 10f) / 10f;
		if (Hp <= 0) {
			Hp = 0;
			Debug.Log ("Object is dead.");
		}
	}

	void SetTileUnderSolid () {
		foreach (Tile t in Tiles_under) {
			if (t.Type == Tile.TileType.Empty)
				t.Type = Tile.TileType.Terrian;
		}
	}

	void SetSpawns(Creature c) {
		Spawns.Add (c);
	}

	HashSet<Tile> SpawnLocation() {
		HashSet<Tile> locations = new HashSet<Tile>();
		foreach (Tile t in Tiles_under) {
			locations.UnionWith (t.GetNeighbours());
		}
		locations.RemoveWhere (s => s.Owner != null);
		return locations;
	}

	void Spawn(Creature c, Tile t) {
		if (Spawns.Contains(c)){
			if (Spawn_Locations.Contains (t)) {
				t.Owner = c;
				if (c.Area > 0) {

				}
				else
					c.Move(t);
			}
		}
	}
}
