using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverOverItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Inventory inventory;

    [Header("Information Display")]
    [SerializeField] RectTransform informationCard;
    [SerializeField] TMP_Text nameInfo;
    [SerializeField] TMP_Text descInfo;
    [SerializeField] TMP_Text qtyInfo;
    [SerializeField] Image imgInfo;
    [SerializeField] TMP_Text option1;
    [SerializeField] TMP_Text option2;

    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayInformation();
        HoverOnItemCheck();
    }
    public void OnPointerExit(PointerEventData eventData) { HoverOffItemCheck(); }
    public void DisplayInformation()
    {
        informationCard.gameObject.SetActive(true);
        inventory.selectedItem = transform.GetComponent<Image>().sprite.name;
        string[] type_name = transform.GetComponent<Image>().sprite.name.Split('_');
        string type = type_name[0];
        string name = type_name[1];

        foreach (Collectables collectableGroup in inventory.collectableGroups)
        {
            if (collectableGroup.type.ToString() == type)
            {

                foreach (Collectable collectable in collectableGroup.collectables)
                {
                    if (collectable.name != name)
                    {
                        continue;
                    }
                    else
                    {
                        nameInfo.text = collectable.name;
                        qtyInfo.text = "x" + collectable.quanity.ToString();
                        descInfo.text = collectable.description;
                        imgInfo.sprite = collectable.collectablePref.GetComponent<SpriteRenderer>().sprite;
                        break;
                    }

                }
                break;
            }

        }
        switch (type)
        {

            case "Weapon":
                option1.text = "Equip";
                option2.text = "Drop";
                break;
            case "Health":
                break;
            case "RawMaterial":
                break;
            case "Food":
                break;
            case "Toy":
                break;

        }
    }
    public void HideInformation()
    {
        if (!inventory.hoverOnItem)
        {
            informationCard.gameObject.SetActive(false);
        }
    }
    public void HoverOnItemCheck()
    {
        inventory.hoverOnItem = true;
    }
    public void HoverOffItemCheck()
    {
        inventory.hoverOnItem = false;
        HideInformation();
    }
}
