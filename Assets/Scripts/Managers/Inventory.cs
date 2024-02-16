using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Collectables[] collectableGroups;
    [SerializeField] Image[] weaponPlaceholders;
    [SerializeField] Image[] otherPlaceholders;


    public bool hoverOnItem = false;
    public string selectedItem;

    //show collectable img
    private void OnEnable()
    {
        foreach (Collectables collectableGroup in collectableGroups)
        {
            if (collectableGroup.type == CollectableType.Weapon)
            {
                foreach (Collectable collectable in collectableGroup.collectables)
                {
                    if (collectable.quanity == 0) continue;
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
                foreach (Collectable collectable in collectableGroup.collectables)
                {
                    if (collectable.quanity == 0) continue;
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
            Debug.Log("nulled");

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
            Debug.Log("nulled");

            if (otherPhs.IsActive())
            {
                Debug.Log("nulled");
                otherPhs.sprite = null;
                otherPhs.gameObject.SetActive(false);
            }
            else
            {
                break;
            }
        }
    }

}
