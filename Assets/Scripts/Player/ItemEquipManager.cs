using UnityEngine;

public class ItemEquipManager : MonoBehaviour
{
    [Header("EquipableCollectables")]
    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject[] toys;
    [SerializeField] GameObject[] foods;

    public GameObject currItemOnHand;

    //use in inventory
    public bool Equip(string type, string itemName)
    {
        if (currItemOnHand == null)
        {
            currItemOnHand = new GameObject();
        }
        if (currItemOnHand.name.Contains(itemName)) return true;

        currItemOnHand.SetActive(false);

        if (type == CollectableType.Weapon.ToString())
        {
            foreach (GameObject weapon in weapons)
            {
                if (itemName == weapon.name)
                {
                    if (weapon.name.ToLower().Contains("axe"))
                    {
                        TutorialManager.instance.UpdateStage(TutorialStage.Equip_Axe);
                    }
                    weapon.SetActive(true);
                    currItemOnHand = weapon;
                    return true;
                }
            }
        }
        else if (type == CollectableType.Toy.ToString())
        {
            foreach (GameObject toy in toys)
            {
                if (itemName == toy.name)
                {
                    toy.SetActive(true);
                    currItemOnHand = toy;
                    return true;
                }
            }
        }
        else if (type == CollectableType.Food.ToString())
        {
            foreach (GameObject food in foods)
            {
                if (itemName == food.name)
                {
                    if (!food.name.ToLower().Contains("cooked"))
                    {
                        TutorialManager.instance.UpdateStage(TutorialStage.Press_Tab_Key_To_Open_Inventory_And_Equip_Ingredient);
                    }
                    food.SetActive(true);
                    currItemOnHand = food;
                    return true;
                }
            }
        }
        Debug.LogWarning("no match");
        return false;

    }
    public void Unequip()
    {
        currItemOnHand.SetActive(false);
        currItemOnHand =null;
    }
}
