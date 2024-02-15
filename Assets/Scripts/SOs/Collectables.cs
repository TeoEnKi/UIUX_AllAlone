using System.Collections.Generic;
using UnityEngine;
public enum CollectableType { Weapon, Health, RawMaterial, Food, Toy }

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CollectableScriptableObject", order = 1)]
public class Collectables:ScriptableObject
{
    public CollectableType type;

    public List<Collectable> collectables;
}
[System.Serializable]
public class Collectable
{
    public string name;
    [TextArea(3, 10)]
    public string description;
    public int quanity;

    //will contain the sprite
    public GameObject collectablePref;

}
