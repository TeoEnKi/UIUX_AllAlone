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
    List<Message> currMessageOpts;

    [SerializeField] Dialogues[] dialogues;

    bool textStillPrinting = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerManager.instance.objectInfrontOfPlayer != null && currMessage == null)
        {        if (!PlayerManager.instance.objectInfrontOfPlayer.CompareTag("Daughter")) return;

            TutorialManager.instance.UpdateStage(TutorialStage.Talk_To_Daughter);
            TutorialManager.instance.UpdateStage(TutorialStage.Talk_To_Daughter_Again);
            PlayerManager.instance.playerState = PlayerState.Dialogue;
            SetUpCurrMessage(Person.Daughter);
            if (currMessage == null) return;
            DisplayCurrMessage();
        }
        else if (Input.anyKeyDown && currMessage != null && !textStillPrinting)
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
        int currDayid = (int)PlayerManager.instance.currDay;

        switch (playerTalkingTo)
        {
            case Person.Daughter:
                foreach (StartingMessage startingMessage in dialogues[currDayid].startingMessages_Daughter)
                {
                    if (startingMessage.keepDisplaying)
                    {
                        currMessage = startingMessage.startingMessage;
                        break;
                    }
                }
                break;
        }
    }

    private void HideMessage()
    {
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
        namePlaceholder.GetComponent<TMP_Text>().text = currMessage.person.ToString();
        AudioManager.instance.PlayTalkingNoise();
        StartCoroutine(DisplayLetters());
    }
    public void DisplayBtns()
    {
        currMessageOpts = new List<Message>();
        messagePlaceholder.gameObject.SetActive(false);
        namePlaceholder.gameObject.SetActive(false);

        for (int i = 0; i < currMessage.nextMessage.Length; i++)
        {
            Debug.Log(currMessage.nextMessage.Length + "" + i);
            opts[i].gameObject.SetActive(true);
            opts[i].GetComponentInChildren<TMP_Text>().text = currMessage.nextMessage[i].text;
            currMessageOpts.Add(currMessage.nextMessage[i]);
        }

    }
    public void NextMessageOnClick(Button button)
    {
        Message selectedOpt = null;
        for (int i = 0; i < currMessageOpts.Count; i++)
        {
            Debug.Log(i);
            if (opts[i] == button)
            {
                selectedOpt = currMessageOpts[i];
                break;
            }
        }
        currMessage = selectedOpt;
        ContinueDialogue();

    }

    IEnumerator DisplayLetters()
    {
        textStillPrinting = true;
        List<char> chars = currMessage.text.ToList();
        int letterID = 0;
        while (chars.Count > messagePlaceholder.GetComponent<TMP_Text>().text.Length)
        {
            messagePlaceholder.GetComponent<TMP_Text>().text += chars[letterID];
            letterID++;
            if (chars.Count <= messagePlaceholder.GetComponent<TMP_Text>().text.Length)
            {
                AudioManager.instance.StopTalkingNoise();
                textStillPrinting = false;
            }
            yield return new WaitForSeconds(0.1f);
        }

    }

}
