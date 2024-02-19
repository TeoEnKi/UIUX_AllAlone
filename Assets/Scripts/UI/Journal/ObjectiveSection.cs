using System.Drawing;
using TMPro;
using UnityEngine;

public class ObjectiveSection : MonoBehaviour
{
    PlayerManager playerManager;
    [SerializeField] TMP_Text listOfObjs;
    [SerializeField] TMP_Text foodPercentage;
    [SerializeField] TMP_Text defensePercentage;
    string objText = "";
    ObjType prevObjType = ObjType.Tutorial;
    [SerializeField] DailyObjectives[] dailyObjectives;

    private void Awake()
    {
        playerManager = FindAnyObjectByType<PlayerManager>();
    }
    private void OnEnable()
    {
        objText = "";
        foreach (Objective obj in dailyObjectives[0].objectives)
        {
            if (objText == "" && prevObjType == obj.objType) 
            {
                objText = objText + "<b> " + prevObjType + " </b> ";
            }
            else if (prevObjType != obj.objType)
            {
                prevObjType = obj.objType;
                objText = objText + "\n<b>" + prevObjType + "</b>";
            }
            string objDisplayText;

            if (obj.complete)
            {
                objDisplayText = "<s><size=75%>" + obj.objTitle + "</s>";
            }
            else
            {
                objDisplayText = "<size=75%>" + obj.objTitle;
            }

            objText = objText + "\n- " + objDisplayText;
        }
        listOfObjs.text = objText;

        if (playerManager == null) return;
        foodPercentage.text = PlayerPrefs.GetInt(playerManager.meatFedPercentKey).ToString() + "<size=60%>%";
        defensePercentage.text = PlayerPrefs.GetInt(playerManager.defensePercentKey).ToString() + "<size=75%>%";
    }
}
