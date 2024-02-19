using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;
    [SerializeField] RectTransform[] toolTipPanels;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {

            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }
    public void SetAndShowTooltip(GameObject gameObject)
    {
        if (gameObject.tag == "Collectable")
        {        

            toolTipPanels[0].gameObject.SetActive(true);
            if(toolTipPanels[0].GetComponentInChildren<TMP_Text>() ==null) { Debug.Log("null"); }
            toolTipPanels[0].GetComponentInChildren<TMP_Text>().text = "[E] Pick Up";
            PlayerManager.instance.objectInfrontOfPlayer = gameObject;
        }

    }
    public void HideTooltip()
    {
        PlayerManager.instance.objectInfrontOfPlayer = null;
        foreach (RectTransform panel in toolTipPanels)
        {
            if (panel.gameObject.activeSelf) 
            {
                panel.gameObject.SetActive(false);
                break;
            }
        }

    }
}
