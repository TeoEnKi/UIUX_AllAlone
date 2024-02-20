using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Person { Player, Daughter, Survivor }

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MessageScriptableObject", order = 5)]
public class Message : ScriptableObject
{
    public Person person;
    [TextArea(3, 10)]
    public string text;

    //the option text will be found in the first dialogue object of the list
    public Message[] nextMessage;
}
