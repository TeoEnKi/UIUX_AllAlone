using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CollectableScriptableObject", order = 1)]
public class Collectables
{
    private int _name;
    private GameObject _collectablePref;

    // Public property for accessing _myVariable (read-only)
    public int Name
    {
        // Getter allows external access to read the value
        get
        {
            return _name;
        }
        // No setter defined, making the property read-only
    }
    public GameObject CollectablePrefab
    {
        // Getter allows external access to read the value
        get
        {
            return _collectablePref;
        }
        // No setter defined, making the property read-only
    }

}