using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Note : ScriptableObject, IComparable<Note>{

	public enum Currency {USD, RIELS};

	public float UsdValue;
	public float rielsValue;
	public Currency currency;
	public Sprite image;

	public int CompareTo(Note that)
    {
        return this.UsdValue.CompareTo(that.UsdValue);
    }
}
