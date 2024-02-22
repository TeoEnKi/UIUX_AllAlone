using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataReset : MonoBehaviour
{
    [SerializeField] DailyObjectives dailyObjs;
    [SerializeField] Dialogues dialogues;
    [SerializeField] IndiCollectable axe;
    [SerializeField] IndiCollectable[] food;
    [SerializeField] IndiCollectable[] toyMats;
    [SerializeField] IndiCollectable[] axeMats;
    [SerializeField] IndiCollectable[] healthCollects;

    private void Start()
    {
        axe.quanity = 0;
        foreach (IndiCollectable mat in axeMats)
        {
            mat.quanity = 0;
        }
        foreach (StartingMessage startingmess in dialogues.startingMessages_Daughter)
        {
            startingmess.keepDisplaying = true;
        }
        foreach(Objective obj in dailyObjs.objectives)
        {
            obj.complete = false;
        }
        foreach (IndiCollectable foodind in food)
        {
            foodind.quanity = 0;
        }
        foreach (IndiCollectable mat in toyMats)
        {
            mat.quanity = 2;
        }
        foreach (IndiCollectable health in healthCollects)
        {
            health.quanity = 0;
        }
        if (PlayerManager.instance != null)
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
