using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool journalOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (journalOpen)
            {
                journalOpen = false;
                HideJournal();
            }
            else
            {
                journalOpen = true;
                DisplayJournal();
            }
        }
        if (journalOpen)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void DisplayJournal()
    {
        throw new NotImplementedException();
    }

    private void HideJournal()
    {
        throw new NotImplementedException();
    }
}
