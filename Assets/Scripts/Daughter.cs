using UnityEngine;

public class Daughter : MonoBehaviour
{
    ItemEquipManager itemEquipManager;
    [SerializeField] IndiCollectable cookedFood;
    [SerializeField] Collectables toys;
    private void Start()
    {
        itemEquipManager = GetComponent<ItemEquipManager>();
    }
    void Update()
    {
        if (PlayerManager.instance.currDay != Day.Tutorial || PlayerManager.instance.objectInfrontOfPlayer == null) return;

        if (!PlayerManager.instance.objectInfrontOfPlayer.CompareTag("Daughter")) return;

        if (Input.GetKeyDown(KeyCode.F) && !TutorialManager.instance.BlockArea(TutorialStage.Pick_Up_And_Equip_Cooked_Food_And_Feed_Daughter) && itemEquipManager.currItemOnHand.name == cookedFood.collectablePref.name)
        {
            TutorialManager.instance.UpdateStage(TutorialStage.Pick_Up_And_Equip_Cooked_Food_And_Feed_Daughter);
            itemEquipManager.currItemOnHand.SetActive(false);
            cookedFood.quanity--;
        }
        if (Input.GetKeyDown(KeyCode.T) && TutorialManager.instance.currStage == TutorialStage.Equip_And_Give_Toy && itemEquipManager.currItemOnHand.transform.parent.name.Contains("Toy"))
        {
            foreach (IndiCollectable toy in toys.collectables)
            {
                if (toy.name == itemEquipManager.currItemOnHand.name)
                {
                    TutorialManager.instance.UpdateStage(TutorialStage.Equip_And_Give_Toy);
                    toy.quanity--;
                    itemEquipManager.currItemOnHand.SetActive(false);
                    break;
                }
            }
        }
    }
}