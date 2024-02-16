using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public bool journalOpen = false;
    public bool inventoryOpen = false;

    [Header("Health")]
    [Range(0, 100)] public int physicalHealthLvl = 100;
    int currPhysHealthLvl;
    [SerializeField] Image phyStateImg;
    [SerializeField] Sprite[] allPhySts;
    string phyHealthKey = "phyHealth";

    [Range(0, 80)] public int mentalHealthLvl = 80;
    int currMentHealthLvl;
    [SerializeField] Image mentalStateImg;
    [SerializeField] Sprite[] allMenSts;
    string mentHealthKey = "menHealth";


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(phyHealthKey))
        {
            currPhysHealthLvl = PlayerPrefs.GetInt(phyHealthKey);
        }
        else
        {
            currPhysHealthLvl = physicalHealthLvl;
        }
        if (PlayerPrefs.HasKey(mentHealthKey))
        {
            currMentHealthLvl = PlayerPrefs.GetInt(mentHealthKey);
        }
        else
        {
            currMentHealthLvl = mentalHealthLvl;
        }
    }
    private void Update()
    {
    }

    //either -ve or +ve number
    public void ChangeMentState(int changeAmt)
    {
        currMentHealthLvl += changeAmt;
        if (currMentHealthLvl < 0)
        {
            Die();
            return;
        }
        else if (currMentHealthLvl > mentalHealthLvl)
        {
            currMentHealthLvl = mentalHealthLvl;
        }
        mentalStateImg.sprite = allMenSts[Mathf.CeilToInt((float)currMentHealthLvl / 20)];
        PlayerPrefs.SetInt(mentHealthKey, currMentHealthLvl);
    }

    //either -ve or +ve number
    public void ChangePhyState(int changeAmt)
    {
        currPhysHealthLvl += changeAmt;
        if (currPhysHealthLvl < 0)
        {
            Die();
            return;
        }
        else if (currPhysHealthLvl > physicalHealthLvl)
        {
            currPhysHealthLvl = physicalHealthLvl;
        }

        phyStateImg.sprite = allPhySts[Mathf.CeilToInt((float)currPhysHealthLvl / 20)];
        PlayerPrefs.SetInt(phyHealthKey,currPhysHealthLvl);
    }

    private void Die()
    {
        throw new NotImplementedException();
    }
}
