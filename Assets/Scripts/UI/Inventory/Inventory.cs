using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    JournalInventory jourInve;
    ItemEquipManager itemEquipManager;
    public Collectables[] collectableGroups;
    [SerializeField] Image[] weaponPlaceholders;
    [SerializeField] Image[] otherPlaceholders;

    [SerializeField] RectTransform infoCard;

    public bool hoverOnItem = false;
    public string selectedItem;

    private void Awake()
    {
        jourInve = GetComponentInParent<JournalInventory>();
        itemEquipManager = FindAnyObjectByType<ItemEquipManager>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && hoverOnItem && selectedItem != "")
        {
            if (selectedItem.Split('_')[0] != "Health" && selectedItem.Split('_')[0] != "RawMaterial")
            {
                bool equipped = itemEquipManager.Equip(selectedItem.Split('_')[0], selectedItem.Split('_')[1]);
                if (equipped)
                {
                    jourInve.HideInventory();
                }
            }
            else if (selectedItem.Split('_')[0] == "Health")
            {
                Heal(selectedItem.Split('_')[0], selectedItem.Split('_')[1]);
                DisplayItems();
            }
        }
        if (Input.GetMouseButtonDown(1) && hoverOnItem && selectedItem != "")
        {
            bool droppable;
            GameObject itemToDrop;
            (droppable, itemToDrop) = Drop(selectedItem.Split('_')[0], selectedItem.Split('_')[1]);
            if (droppable)
            {
                Debug.Log("drop");
                Vector3 dropPos = PlayerManager.instance.gameObject.transform.position + (PlayerManager.instance.gameObject.transform.forward * 10);
                GameObject currDroppedItem = Instantiate(itemToDrop);
                currDroppedItem.name = itemToDrop.name;
                currDroppedItem.transform.position = dropPos;
                currDroppedItem.transform.parent = null;
                jourInve.HideInventory();
            }
        }

    }
    private void OnEnable()
    {
        DisplayItems();
    }
    //show collectable img
    private void DisplayItems()
    {
        DisablePlaceholder();
        foreach (Collectables collectableGroup in collectableGroups)
        {
            if (collectableGroup.type == CollectableType.Weapon)
            {
                foreach (IndiCollectable collectable in collectableGroup.collectables)
                {
                    if (collectable.quanity <= 0) 
                    {
                        collectable.quanity = 0;
                        continue;
                    }
                    foreach (Image weaponPhs in weaponPlaceholders)
                    {
                        if (weaponPhs.IsActive()) continue;
                        weaponPhs.gameObject.SetActive(true);
                        weaponPhs.sprite = collectable.collectablePref.GetComponent<SpriteRenderer>().sprite;
                        break;
                    }
                }
            }
            else
            {
                foreach (IndiCollectable collectable in collectableGroup.collectables)
                {
                    if (collectable.quanity <= 0)
                    {
                        collectable.quanity = 0;
                        continue;
                    }
                    foreach (Image otherPhs in otherPlaceholders)
                    {
                        if (otherPhs.IsActive())
                        {
                            continue;

                        }
                        otherPhs.gameObject.SetActive(true);
                        otherPhs.sprite = collectable.collectablePref.GetComponent<SpriteRenderer>().sprite;
                        break;
                    }
                }
            }
        }
    }
    private void DisablePlaceholder()
    {

        foreach (Image weaponPhs in weaponPlaceholders)
        {

            if (weaponPhs.IsActive())
            {
                weaponPhs.sprite = null;
                weaponPhs.gameObject.SetActive(false);
            }
            else
            {
                break;
            }

        }
        foreach (Image otherPhs in otherPlaceholders)
        {

            if (otherPhs.IsActive())
            {
                otherPhs.sprite = null;
                otherPhs.gameObject.SetActive(false);
            }
            else
            {
                break;
            }
        }
    }

    (bool, GameObject) Drop(string type, string itemName)
    {
        if (type == CollectableType.Weapon.ToString())
        {
            foreach (Collectables colGrp in collectableGroups)
            {
                if (colGrp.type != CollectableType.Weapon) continue;
                foreach (IndiCollectable indiCol in colGrp.collectables)
                {
                    if (indiCol.name.Contains(itemName))
                    {
                        indiCol.quanity--;
                        return (true, indiCol.collectablePref);
                    }
                }
            }
        }
        return (false, null);
    }
    void Heal(string type, string itemName)
    {
        if (itemName == "Pills")
        {
            foreach (Collectables colGrp in collectableGroups)
            {
                if (colGrp.type != CollectableType.Health) continue;
                foreach (IndiCollectable indiCol in colGrp.collectables)
                {
                    if (indiCol.name.Contains(itemName))
                    {
                        indiCol.quanity--;
                        PlayerManager.instance.ChangeMentState(20);
                        return;
                    }
                }
            }
        }
        else if (itemName == "Med Kit")
        {
            foreach (Collectables colGrp in collectableGroups)
            {
                if (colGrp.type != CollectableType.Health) continue;
                foreach (IndiCollectable indiCol in colGrp.collectables)
                {
                    if (indiCol.name.Contains(itemName))
                    {
                        indiCol.quanity--;
                        PlayerManager.instance.ChangePhyState(20);
                        return;
                    }
                }
            }
        }
        return;
    }
    private void OnDisable()
    {
        infoCard.gameObject.SetActive(false);
    }
}
