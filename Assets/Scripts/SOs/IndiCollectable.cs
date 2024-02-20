using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/IndividualCollectableScriptableObject", order = 2)]
public class IndiCollectable : ScriptableObject
{
    [TextArea(3, 10)]
    public string description;
    public int quanity;

    //will contain the sprite
    public GameObject collectablePref;

}
