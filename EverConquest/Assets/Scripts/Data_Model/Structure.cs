using UnityEngine;
using System.Collections;

public class Structure : Pawn {

	public enum ArmorType
	{
		
	}

	public enum WeaponType
	{
		
	}

	public float Hp { get; protected set;}
	public ArmorType Armor { get; protected set;}
	public float Atk { get; protected set;}
	public int Range { get; protected set;}
	public float Def { get; protected set;}

	public Structure (World world, int location_x, int location_y, int area) : base(world, location_x, location_y, area) {
		
	}
}
