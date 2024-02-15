using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] Collectables[] collectableGroups;
    [SerializeField] Image[] weaponPlaceholders;
    [SerializeField] Image[] otherPlaceholders;

    //show collectable img
    private void OnEnable()
    {
        foreach (Collectables collectableGroup in collectableGroups)
        {
            if (collectableGroup.type == CollectableType.Weapon)
            {
                foreach (Collectable collectable in collectableGroup.collectables)
                {
                    if (collectable.quanity == 0) break;
                    foreach(Image weaponPhs in weaponPlaceholders)
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
                    if (collectable.quanity == 0) break;
                    foreach (Image otherPhs in otherPlaceholders)
                    {
                        if (otherPhs.IsActive()) continue;
                        otherPhs.gameObject.SetActive(true);
                        otherPhs.sprite = collectable.collectablePref.GetComponent<SpriteRenderer>().sprite;
                        break;
                    }
                }
            }
        }
    }
}
