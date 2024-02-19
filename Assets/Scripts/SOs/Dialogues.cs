using System.Collections.Generic;
using UnityEngine;
public enum DialogueType { Daughter }

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogueScriptableObject", order = 5)]
public class Dialogues : ScriptableObject
{
    public DialogueType type;

    public List<Dialogue> dialogues;
}
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogueScriptableObject", order = 5)]
public class Dialogue
{
    public string name;
    [TextArea(3, 10)]
    public string text;

    //the option text will be found in the first dialogue object of the list
    public Dialogues[] nextDialogues;
}
