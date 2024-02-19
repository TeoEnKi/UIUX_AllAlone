using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public void OnMouseEnter()
    {
        TooltipManager.instance.SetAndShowTooltip(gameObject);
    }
    public void OnMouseExit()
    {

        TooltipManager.instance.HideTooltip();
    }
}
