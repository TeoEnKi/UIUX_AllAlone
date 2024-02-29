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
    [SerializeField] Image opt1Img;
    [SerializeField] Image opt2Img;
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

                foreach (IndiCollectable collectable in collectableGroup.collectables)
                {
                    if (collectable.collectablePref.name != name)
                    {
                        Debug.Log(name+" "+collectable.name);
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
                opt1Img.gameObject.SetActive(true);
                opt2Img.gameObject.SetActive(true);
                option1.text = "Equip";
                option2.text = "Drop";
                break;
            case "Health":
                opt1Img.gameObject.SetActive(true);
                opt2Img.gameObject.SetActive(false);
                option1.text = "Heal";
                option2.text = "";
                break;
            case "RawMaterial":
                opt1Img.gameObject.SetActive(false);
                opt2Img.gameObject.SetActive(false);
                option1.text = "";
                option2.text = "";
                break;
            case "Food":
                opt1Img.gameObject.SetActive(true);
                opt2Img.gameObject.SetActive(false);
                option1.text = "Equip";
                option2.text = "";
                break;
            case "Toy":
                opt1Img.gameObject.SetActive(true);
                opt2Img.gameObject.SetActive(false);
                option1.text = "Equip";
                option2.text = "";
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
