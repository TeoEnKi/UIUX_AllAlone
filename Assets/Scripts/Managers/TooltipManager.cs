using TMPro;
using UnityEngine;


public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;
    [SerializeField] RectTransform[] toolTipPanels;
    public DailyObjectives[] dailyObjectives;
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
        PlayerManager.instance.objectInfrontOfPlayer = gameObject;
        if (gameObject.tag == "Collectable")
        {
            toolTipPanels[0].gameObject.SetActive(true);
            if (toolTipPanels[0].GetComponentInChildren<TMP_Text>() == null) { Debug.Log("null"); }
            toolTipPanels[0].GetComponentInChildren<TMP_Text>().text = "[E] Pick Up";
        }
        else if (gameObject.tag == "Daughter")
        {
            int daughterInteractions = 1;
            foreach (Objective obj in dailyObjectives[(int)PlayerManager.instance.currDay].objectives)
            {
                if (obj.objType == ObjType.Daughter && !obj.complete && !obj.objTitle.ToLower().Contains("talk"))
                {
                    daughterInteractions++;
                }
            }
            if (daughterInteractions <= 0) return;

            toolTipPanels[daughterInteractions - 1].gameObject.SetActive(true);
            TMP_Text actionTxt = toolTipPanels[daughterInteractions - 1].GetComponentInChildren<TMP_Text>();
            if (toolTipPanels[daughterInteractions - 1] == null) { Debug.Log("null"); }
            Debug.Log(toolTipPanels[daughterInteractions - 1].name);
            if (toolTipPanels[daughterInteractions - 1].GetComponentInChildren<TMP_Text>() == null) { Debug.Log("null"); }

            string actions = "";
            switch (daughterInteractions - 1)
            {
                case 2:
                    actions = "[E] Talk\n[F] Feed\n[T] Give Toy";
                    if (actionTxt != null)
                    {
                        actionTxt.text = actions;
                    }
                    break;
                case 1:
                    foreach (Objective obj in dailyObjectives[(int)PlayerManager.instance.currDay].objectives)
                    {
                        if (obj.objType == ObjType.Daughter && !obj.complete && obj.objTitle.ToLower().Contains("toy"))
                        {
                            actions = "[E] Talk\n[T] Give Toy";
                            if (actionTxt != null)
                            {
                                actionTxt.text = actions;
                            }
                            return;
                        }
                    }
                    actions = "[E] Talk\n[F] Feed";
                    if (actionTxt != null)
                    {
                        actionTxt.text = actions;
                    }
                    break;
                default:
                    actions = "[E] Talk";
                    if (actionTxt != null)
                    {
                        actionTxt.text = actions;
                    }
                    break;
            }

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

    public bool NearPlayer(Vector3 spherePos)
    {
        Collider[] colliders;
        colliders = Physics.OverlapSphere(spherePos, 30);
        foreach (Collider collider in colliders)
        {
            if (collider.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
}
