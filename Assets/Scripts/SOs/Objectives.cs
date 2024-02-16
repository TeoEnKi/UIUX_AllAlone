using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Day { Tutorial, Day1, Day2, Day3, Day4 }

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectiveScriptableObject", order = 1)]
public class DailyObjectives : ScriptableObject
{
    public Day day;

    public Objective[] objectives;
}
public enum ObjType {Tutorial, Daughter, Cure, Final}

[System.Serializable]
public class Objective
{
    public ObjType objType;
    [TextArea(3, 10)]
    public string objTitle;
    public bool complete;


}
