using UnityEngine;
using System.Collections;

public class Creature : Moveable_Pawn {
	public enum ArmorType
	{
		Cloth = 1, Leather = 2, Wooden = 4, Mail = 5, Plate = 6 
	}

	public enum WeaponType
	{
		// Use Energy only if you're a mage and are casting spell.
		// Mages get Energy attacks only.
		Light, Piercing, Heavy, Energy
	}

	// Initialized attributes:
	public float Hp { get; protected set;}
	public ArmorType Armor { get; protected set;}
	public WeaponType Weapon { get; protected set;} 
	public int Range { get; protected set;}
	// public int Units { get; protected set;} // Similar to Might & Magics, units is used to keep track of men in a squad. I will hold this off for now.

	// calculated attributes:
	public float Atk { get; protected set;}
	public float Def { get; protected set;}
	public int speed = 0;

	public int Speed {
		get {
			return speed;
		}
		set {
			speed = value;
		}
	}

	public Creature(World world, int location_x, int location_y, int area, int movement,
		float hp, ArmorType armor, WeaponType weapon,float atk, float def, int range = 1) : base(world, location_x, location_y, area, movement) {

		Hp = hp;
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
		if (other.GetType() == typeof(Creature)) {
			Creature otherCreature = other as Creature;
			float dmg = 0f;
			if (Weapon == WeaponType.Piercing) {
				dmg = Atk * (Random.value + 0.2f);
				if (otherCreature.Armor == ArmorType.Leather)
					dmg *= 1.3f;
				else if (otherCreature.Armor == ArmorType.Plate)
					dmg *= 0.7f;
			}

			if (Weapon == WeaponType.Light) {
				dmg = Atk * (Random.value + 0.3f);
				if (otherCreature.Armor == ArmorType.Cloth)
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
				else if (otherCreature.Armor == ArmorType.Cloth)
					dmg *= 0.7f;
			}

			otherCreature.TakDmg (dmg);
		} else if (other.GetType() == typeof(Structure)) {
			// Haven't decided for structures yet.
		} else {
			Debug.Log ("Can't attack this Object.");
		}
	}

	public void TakDmg (float dmg) {
		Hp -= dmg;
		if (Hp <= 0) {
			Hp = 0;
			Debug.Log ("Object is dead.");
		}
	}
}
