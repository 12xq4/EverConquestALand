/**
 * The biggest problem regarding this class now is to have the units rebalanced
 * Right now the armor bonus is way to powerful.
 *  - one solution is to take away armor bouns. Could be good if this kind of balancing is left to the game design person.
 * 	implement such bonus makes balancing occur at the fundamental logic level instead of at the data level.
 */ 


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Creature : Moveable_Pawn {
	public enum ArmorType
	{
		Cloth = 0, Leather = 1, Wooden = 2, Mail = 3, Plate = 4 
	}

	public enum WeaponType
	{
		// Use Energy only if you're a mage and are casting spell.
		// Mages get Energy attacks only.
		Light, Piercing, Heavy, Energy
	}

	// Initialized attributes:
	// public float Hp { get; protected set;}
	public ArmorType Armor { get; protected set;}
	public WeaponType Weapon { get; protected set;} 
	public int Range { get; protected set;}
	// public int Units { get; protected set;} // Similar to Might & Magics, units is used to keep track of men in a squad. I will hold this off for now.

	// calculated attributes:
	public float Atk { get; protected set;}
	public float Def { get; protected set;}
	// public int speed = 0;

	public Creature(World world, int location_x, int location_y, float hp, ArmorType armor, WeaponType weapon,
		float atk, float def, int area, int movement, int range = 1) : base(world, location_x, location_y, hp, area, movement) {
		Armor = armor;
		Weapon = weapon;
		Range = range;
		// Units = unit;

		// Here speed is treated as priority for attacking. The party has higher speed attacks first.
		// Here we calcuate speed bonus for Armors.
		if (Armor == ArmorType.Cloth)
			Speed += 10;
		else if (Armor == ArmorType.Leather)
			Speed += 8;
		else if (Armor == ArmorType.Wooden)
			Speed += 6;
		else if (Armor == ArmorType.Mail)
			Speed += 4;
		else if (Armor == ArmorType.Plate)
			Speed += 3;

		// Here we calcuate speed bonus for Weapons.
		if (Weapon == WeaponType.Light)
			Speed += 6;
		else if (Weapon == WeaponType.Piercing)
			Speed += 4;
		else if (Weapon == WeaponType.Heavy)
			Speed += 2;
		else if (Weapon == WeaponType.Energy)
			Speed += 3;

		Def = def + (int)Armor;
		Atk = atk;
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
	 * Weapon type and armor type will have a damage modifer.
	 * Initial damage is randomized base on attack and a weapon type modifier
	 * Piercing = 0.2
	 * Light = 0.3
	 * Heavy = 0.5
	 * Energy = 0.4
	 * Modifer goes both ways -> could both amplify and mitigate attack.
	 * For Weapon that counters enemy's Armor, damage * 1.3
	 * For Weapon that are countered by enemy's Armor, damage * 07
	 */

	public void Attack (Pawn other) {
		// Check if the target is in range
		if (CheckReach(other)) {
			if (other.GetType () == typeof(Creature)) {
				Creature otherCreature = other as Creature;
				float dmg = 0f;
				if (Weapon == WeaponType.Piercing) {
					dmg = Atk * (Random.value + 0.2f);
					if (otherCreature.Armor == ArmorType.Wooden)
						dmg *= 1.3f;
					else if (otherCreature.Armor == ArmorType.Plate)
						dmg *= 0.7f;
				}

				if (Weapon == WeaponType.Light) {
					dmg = Atk * (Random.value + 0.3f);
					if (otherCreature.Armor == ArmorType.Leather)
						dmg *= 1.3f;
					else if (otherCreature.Armor == ArmorType.Mail)
						dmg *= 0.7f;
				}

				if (Weapon == WeaponType.Heavy) {
					dmg = Atk * (Random.value + 0.2f);
					if (otherCreature.Armor == ArmorType.Plate)
						dmg *= 1.3f;
					else if (otherCreature.Armor == ArmorType.Leather)
						dmg *= 0.7f;
				}

				if (Weapon == WeaponType.Energy) {
					dmg = Atk * (Random.value + 0.2f);
					if (otherCreature.Armor == ArmorType.Mail)
						dmg *= 1.3f;
					else if (otherCreature.Armor == ArmorType.Wooden)
						dmg *= 0.7f;
				}
				otherCreature.TakDmg (dmg);
			} else if (other.GetType () == typeof(Structure)) {
				// Haven't decided for structures yet.
				Structure otherStruct = other as Structure;
				float dmg = 0f;
				if (Weapon == WeaponType.Piercing) {
					dmg = Atk * (Random.value + 0.2f);
				}

				if (Weapon == WeaponType.Light) {
					dmg = Atk * (Random.value + 0.3f);
				}

				if (Weapon == WeaponType.Heavy) {
					dmg = Atk * (Random.value + 0.2f);
				}

				if (Weapon == WeaponType.Energy) {
					dmg = Atk * (Random.value + 0.2f);
				}
				otherStruct.TakDmg (dmg);
			} else {
				Debug.Log ("Can't attack this Object.");
			}
		} else
			Debug.Log ("Invalid target.");
	}

	public void TakDmg (float dmg) {
		float rand = (float)(Random.value + 0.5);
		
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
}
