using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] RectTransform dialogueParent;
    [SerializeField] Button[] opts;
    [SerializeField] RectTransform namePlaceholder;
    [SerializeField] RectTransform messagePlaceholder;

    [SerializeField] Message currMessage;
    [SerializeField] List<Message> currMessageOpts;

    [SerializeField] Dialogues[] dialogues;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerManager.instance.objectInfrontOfPlayer.CompareTag("Daughter") && currMessage == null)
        {
            Debug.LogWarning("eff");
            PlayerManager.instance.playerState = PlayerState.Dialogue;
            SetUpCurrMessage(Person.Daughter);
            DisplayCurrMessage();
        }
        else if (Input.anyKeyDown && currMessage != null)
        {
            ContinueDialogue();
        }
    }
    void ContinueDialogue()
    {
        if (currMessage.nextMessage.Length == 0)
        {
            currMessage = null;
            HideMessage();
        }
        else if (currMessage.nextMessage[0].person == Person.Player)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            DisplayBtns();
        }
        else if (currMessage.nextMessage[0].person == Person.Daughter)
        {
            currMessage = currMessage.nextMessage[0];
            DisplayCurrMessage();
        }
    }
    private void SetUpCurrMessage(Person playerTalkingTo)
    {
        Debug.LogWarning("eff");

        int currDayid = (int)PlayerManager.instance.currDay;

        switch (playerTalkingTo)
        {
            case Person.Daughter:
                foreach (StartingMessage startingMessage in dialogues[currDayid].startingMessages_Daughter)
                {
                    if (startingMessage.keepDisplaying)
                    {
                        currMessage = startingMessage.startingMessage;
                        Debug.Log(currMessage == null);
                        break;
                    }
                }
                break;
        }
    }

    private void HideMessage()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerManager.instance.playerState = PlayerState.None;
        dialogueParent.gameObject.SetActive(false);
    }

    public void DisplayCurrMessage()
    {
        if (PlayerManager.instance == null) return;

        PlayerManager.instance.playerState = PlayerState.Dialogue;

        dialogueParent.gameObject.SetActive(true);

        namePlaceholder.gameObject.SetActive(true);
        messagePlaceholder.gameObject.SetActive(true);
        namePlaceholder.GetComponent<TMP_Text>().text = "";
        messagePlaceholder.GetComponent<TMP_Text>().text = "";

        foreach (Button opt in opts)
        {
            if (opt.gameObject.activeSelf)
            {
                opt.gameObject.SetActive(false);
            }
        }
        Debug.Log(currMessage.person.ToString());
        Debug.Log(namePlaceholder.GetComponent<TMP_Text>() == null);
        namePlaceholder.GetComponent<TMP_Text>().text = currMessage.person.ToString();
        StartCoroutine(DisplayLetters());
    }
    public void DisplayBtns()
    {
        currMessageOpts = new List<Message>(currMessage.nextMessage);
        messagePlaceholder.gameObject.SetActive(false);
        namePlaceholder.gameObject.SetActive(false);

        for (int i = 0; i < currMessage.nextMessage.Length; i++)
        {
            Debug.Log(i);
            opts[i].gameObject.SetActive(true);
            opts[i].GetComponentInChildren<TMP_Text>().text = currMessage.nextMessage[i].text;
        }

    }
    public void NextMessageOnClick(Button button)
    {
        Message selectedOpt = null;
        foreach (char letter in button.name)
        {
            if (char.IsDigit(letter))
            {
                selectedOpt = currMessageOpts[(int)letter];
                break;
            }
        }
        currMessage = selectedOpt.nextMessage[0];


        ContinueDialogue();

    }

    IEnumerator DisplayLetters()
    {
        List<char> chars = currMessage.text.ToList();
        int letterID = 0;
        while (chars.Count > messagePlaceholder.GetComponent<TMP_Text>().text.Length)
        {
            messagePlaceholder.GetComponent<TMP_Text>().text += chars[letterID];
            letterID++;
            yield return new WaitForSeconds(0.1f);
        }
    }

}
