using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialoguesScriptableObject", order = 6)]
public class Dialogues : ScriptableObject
{
    public Day day;

    public List<StartingMessage> startingMessages_Daughter;
    public List<StartingMessage> startingMessages_Survivor;
}
[System.Serializable]
public class StartingMessage
{
    public Message startingMessage;
    public bool keepDisplaying;
}
