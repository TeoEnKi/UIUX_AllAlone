using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private void Update()
    {
        if (!TooltipManager.instance.NearPlayer(transform.position))
        {
            RectTransform[] rectTransforms = GetComponentsInChildren<RectTransform>();
            foreach (RectTransform rect in rectTransforms)
            {
                Image rectImg = rect.GetComponent<Image>();
                if (rectImg == null) continue;

                rectImg.enabled = false;
                return;
            }
        }

    }
    public void OnMouseEnter()
    {
        if (PlayerManager.instance.playerState == PlayerState.None && TooltipManager.instance.NearPlayer(transform.position))
        {
            Debug.Log("daughter");
            TooltipManager.instance.SetAndShowTooltip(gameObject);
        }
    }
    public void OnMouseExit()
    {
        TooltipManager.instance.HideTooltip();
    }
}
