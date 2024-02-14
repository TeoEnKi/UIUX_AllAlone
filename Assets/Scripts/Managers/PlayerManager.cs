using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public bool journalOpen = false;
    public bool inventoryOpen = false;

    [Header("Health")]
    [Range(0, 100)] public int physicalHealthLvl = 100;
    [SerializeField] Image phyStateImg;
    [SerializeField] Sprite[] allPhySts;

    [Range(0, 80)] public int mentalHealthLvl = 80;
    [SerializeField] Image mentalStateImg;
    [SerializeField] Sprite[] allMenSts;


    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
    }

    //either -ve or +ve number
    public void ChangeMentState(int changeAmt)
    {
        mentalHealthLvl += changeAmt;
        if (mentalHealthLvl < 0)
        {
            Die();
            return;
        }
        mentalStateImg.sprite = allMenSts[Mathf.CeilToInt((float)mentalHealthLvl / 20)];
    }

    //either -ve or +ve number
    public void ChangePhyState(int changeAmt)
    {
        physicalHealthLvl += changeAmt;
        if (physicalHealthLvl < 0)
        {
            Die();
            return;
        }
        phyStateImg.sprite = allPhySts[Mathf.CeilToInt((float)physicalHealthLvl / 20)];
    }

    private void Die()
    {
        throw new NotImplementedException();
    }
}
