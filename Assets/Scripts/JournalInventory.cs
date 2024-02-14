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
            playerManager.journalOpen = true;

            if (!journal.gameObject.activeSelf)
            {
                journal.gameObject.SetActive(playerManager.journalOpen);
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerManager.inventoryOpen = true;

            if (!inventory.gameObject.activeSelf)
            {
                inventory.gameObject.SetActive(playerManager.inventoryOpen);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (playerManager.journalOpen)
            {
                playerManager.journalOpen = false;
                HideJournal();
            }
            if(playerManager.inventoryOpen)
            {
                playerManager.inventoryOpen = false;
                HideInventory();
            }
        }

        HandleCursor();

    }

    private void HandleCursor()
    {
        if (playerManager.journalOpen || playerManager.inventoryOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void HideJournal()
    {
        if (!journal.gameObject.activeSelf) return;
        journalAnimator.SetBool("JournalOpen", playerManager.journalOpen);
        StartCoroutine(DisableJournal());

    }

    IEnumerator DisableJournal()
    {
        yield return new WaitForSeconds(journalAnimator.GetCurrentAnimatorClipInfo(0).Length);
        journal.gameObject.SetActive(playerManager.journalOpen);
    }
    public void HideInventory()
    {
        if (!inventory.gameObject.activeSelf) return;
        inventoryAnimator.Play("Close");
        StartCoroutine(DisableInventory());

    }

    IEnumerator DisableInventory()
    {
        yield return new WaitForSeconds(inventoryAnimator.GetCurrentAnimatorClipInfo(0).Length);
        inventory.gameObject.SetActive(playerManager.inventoryOpen);
    }
}
