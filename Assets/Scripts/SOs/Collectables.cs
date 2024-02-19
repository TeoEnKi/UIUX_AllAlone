using System.Collections.Generic;
using UnityEngine;
public enum CollectableType { Weapon, Health, RawMaterial, Food, Toy }

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CollectableScriptableObject", order = 3)]
public class Collectables : ScriptableObject
{
    public CollectableType type;

    public List<IndiCollectable> collectables;
}
