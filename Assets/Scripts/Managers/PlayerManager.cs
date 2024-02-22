using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum PlayerState { None, NewScene, Dialogue, Journal, Inventory };

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public Day currDay = 0;
    public PlayerState playerState = PlayerState.None;

    public GameObject objectInfrontOfPlayer = null;

    public string survivorsKilledKey = "survivors Killed";

    public string meatFedPercentKey = "meatFed";
    [Range(0, 100)] public int meatFedPercent = 100;
    int currMeatFedPercent;

    public string defensePercentKey = "defense";
    [Range(0, 100)] public int defensePercent = 100;
    int currDefensePercent;

    [Header("Health")]
    [Range(0, 100)] public int physicalHealthLvl = 100;
    [SerializeField] int currPhysHealthLvl;
    [SerializeField] Image phyStateImg;
    [SerializeField] Sprite[] allPhySts;
    string phyHealthKey = "phyHealth";

    [Range(0, 80)] public int mentalHealthLvl = 80;
    [SerializeField] int currMentHealthLvl;
    [SerializeField] Image mentalStateImg;
    [SerializeField] Sprite[] allMenSts;
    public string mentHealthKey = "menHealth";

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        if (PlayerPrefs.HasKey(meatFedPercentKey))
        {
            currMeatFedPercent = PlayerPrefs.GetInt(meatFedPercentKey);
        }
        else
        {
            currMeatFedPercent = meatFedPercent;
            PlayerPrefs.SetInt(meatFedPercentKey, currMeatFedPercent);
        }

        if (PlayerPrefs.HasKey(defensePercentKey))
        {
            currDefensePercent = PlayerPrefs.GetInt(defensePercentKey);
        }
        else
        {
            currDefensePercent = defensePercent;
            PlayerPrefs.SetInt(defensePercentKey, currDefensePercent);
        }

        if (PlayerPrefs.HasKey(phyHealthKey))
        {
            currPhysHealthLvl = PlayerPrefs.GetInt(phyHealthKey);
            ChangePhyState(100);

        }
        else
        {
            currPhysHealthLvl = physicalHealthLvl;
        }

        if (PlayerPrefs.HasKey(mentHealthKey))
        {
            currMentHealthLvl = PlayerPrefs.GetInt(mentHealthKey);
            ChangeMentState(100);

        }
        else
        {
            currMentHealthLvl = mentalHealthLvl;

        }
    }
    private void Update()
    {
        RevealNearbyCollectables();
        HandleCursor();
    }

    private void RevealNearbyCollectables()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10);
        {
            if (colliders.Length > 0)
                foreach (Collider collider in colliders)
                {
                    if (collider.transform.parent == null) continue;
                    if (collider.transform.parent.CompareTag("Collectable"))
                    {
                        int siblingId = collider.transform.GetSiblingIndex();

                        RectTransform[] rectTransforms = collider.transform.parent.GetComponentsInChildren<RectTransform>();
                        foreach (RectTransform rect in rectTransforms)
                        {
                            Image rectImg = rect.GetComponent<Image>();
                            if (rectImg == null) continue;
                            rectImg.enabled = true;
                            break;

                        }

                    }
                }
        }


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
        PlayerPrefs.SetInt(phyHealthKey, currPhysHealthLvl);
    }

    private void Die()
    {
        
    }
    private void HandleCursor()
    {
        if (playerState != PlayerState.None)
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
    public void MoveToNextScene()
    {
        //camera black out?
        playerState = PlayerState.NewScene;
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        //move to spawn

    }
}
