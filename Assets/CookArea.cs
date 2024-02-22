using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookArea : MonoBehaviour
{
    [SerializeField] IndiCollectable cookedFood;
    [SerializeField] IndiCollectable[] foodIngre;
    [SerializeField] Transform cookFoodSpawn;

    ItemEquipManager itemEquipManager;

    // Start is called before the first frame update
    void Start()
    {
        itemEquipManager = gameObject.GetComponent<ItemEquipManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (itemEquipManager == null || PlayerManager.instance == null || TutorialManager.instance == null) return;
        if (TutorialManager.instance.BlockArea(TutorialStage.Cook_At_Pot)) return;
        if(Input.GetKeyDown(KeyCode.E) && PlayerManager.instance.objectInfrontOfPlayer.CompareTag("Cook Area") && itemEquipManager.currItemOnHand.transform.parent.name.Contains("Food"))
        {
            foreach(IndiCollectable food in foodIngre)
            {
                if (itemEquipManager.currItemOnHand.name == food.collectablePref.name)
                {
                    TutorialManager.instance.UpdateStage(TutorialStage.Cook_At_Pot);
                    itemEquipManager.Unequip();

                    GameObject newCookedFood = Instantiate(cookedFood.collectablePref);
                    newCookedFood.name = cookedFood.collectablePref.name;
                    newCookedFood.transform.position = cookFoodSpawn.transform.position;
                    food.quanity--;
                    break;
                }
            }
        }
    }
}
