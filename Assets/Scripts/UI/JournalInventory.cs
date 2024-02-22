using System.Collections;
using UnityEngine;

public class JournalInventory : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    //Journal
    [SerializeField] RectTransform journal;
    Animator journalAnimator;

    //Inventory
    [SerializeField] RectTransform inventory;
    Animator inventoryAnimator;

    private void Start()
    {
        journalAnimator = journal.GetComponent<Animator>();
        inventoryAnimator = inventory.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerManager.playerState = PlayerState.Journal;
            if (!journal.gameObject.activeSelf)
            {
                AudioManager.instance.PlayOpenJournal();

                HideInventory();
                journal.gameObject.SetActive(true);
                journalAnimator.Play("Open");
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerManager.playerState = PlayerState.Inventory;

            if (!inventory.gameObject.activeSelf)
            {
                AudioManager.instance.PlayOpenInventory();

                HideJournal();
                inventory.gameObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (playerManager.playerState == PlayerState.Journal)
            {
                playerManager.playerState = PlayerState.None;
                HideJournal();
            }
            if (playerManager.playerState == PlayerState.Inventory)
            {
                playerManager.playerState = PlayerState.None;
                HideInventory();
            }
        }

    }


    public void HideJournal()
    {
        if (!journal.gameObject.activeSelf) return;
        journalAnimator.Play("Close");
        StartCoroutine(DisableJournal());

    }

    IEnumerator DisableJournal()
    {
        yield return new WaitForSeconds(journalAnimator.GetCurrentAnimatorClipInfo(0).Length);
        journal.gameObject.SetActive(false);
    }
    public void HideInventory()
    {
        if (!inventory.gameObject.activeSelf) return;
        inventoryAnimator.Play("Close");
        StartCoroutine(DisableInventory());

    }

    IEnumerator DisableInventory()
    {
        yield return new WaitForSeconds(inventoryAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        inventory.gameObject.SetActive(false);
    }
}
