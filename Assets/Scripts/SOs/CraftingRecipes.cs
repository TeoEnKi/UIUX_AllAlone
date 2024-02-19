using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RecipeType { Toy, Weapon }

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RecipeScriptableObject", order = 4)]
public class CraftingRecipes : ScriptableObject
{
    public RecipeType type;

    public GameObject recipeImg;

    public List<NeededMaterial> neededMaterials;

    public IndiCollectable result;

}

[System.Serializable]

public class NeededMaterial
{
    public int neededQty;
    public IndiCollectable collectable;
}