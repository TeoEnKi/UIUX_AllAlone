using TMPro;
using UnityEngine;
public enum TutorialStage
{
    Use_WASD_Keys_To_Move, 
    Talk_To_Daughter, Go_To_Kitchen_And_Pick_Up_Food_Ingredient, Press_Tab_Key_To_Open_Inventory_And_Equip_Ingredient, Cook_At_Pot, Pick_Up_And_Equip_Cooked_Food_And_Feed_Daughter,
    Talk_To_Daughter_Again, Press_Q_And_Open_Craft_Toys_Section, Craft_Toy, Equip_And_Give_Toy,
    Go_To_Exit, Press_Tab_To_Open_Inventory, Eat_Pills_To_Recover_Mental_State, Use_MedKit_To_Recover_Physical_State,
    Equip_Axe, Go_Out_And_Kill_It
};

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    public TutorialStage currStage;
    [SerializeField] DailyObjectives tutObjs;
    [HideInInspector] public Objective feedObj;
    [HideInInspector] public Objective toyObj;
    [SerializeField] Dialogues tutDia;
    [SerializeField] Message messFood;
    [SerializeField] Message messToy;
    [HideInInspector] public StartingMessage startMessFood;
    [HideInInspector] public StartingMessage startMessToy;

    [SerializeField] RectTransform panel;
    [SerializeField] TMP_Text instructions;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        foreach (Objective obj in tutObjs.objectives)
        {
            if (obj.objTitle.ToLower().Contains("feed"))
            {
                feedObj = obj;
                continue;
            }
            else if (obj.objTitle.ToLower().Contains("toy"))
            {
                toyObj = obj;
                continue;
            }
        }
        foreach (StartingMessage startMess in tutDia.startingMessages_Daughter)
        {
            if (startMess.startingMessage == messFood)
            {
                startMessFood = startMess;
                continue;
            }
            else if (startMess.startingMessage == messToy)
            {
                startMessToy = startMess;
                continue;
            }
        }
    }
    private void Start()
    {
        currStage = 0;
        DisplayInstuctions();

    }
    public void UpdateStage(TutorialStage stageToProgressFrom)
    {
        if (currStage != stageToProgressFrom) return;
        if (currStage == TutorialStage.Pick_Up_And_Equip_Cooked_Food_And_Feed_Daughter)
        {
            startMessFood.keepDisplaying = false;
            feedObj.complete = true;
        }
        else if (currStage == TutorialStage.Equip_And_Give_Toy)
        {
            toyObj.complete = true;
            startMessToy.keepDisplaying = false;
        }
        NextStage();

    }
    public void NextStage()
    {
        currStage = (TutorialStage)((int)currStage + 1);
        DisplayInstuctions();
    }
    void DisplayInstuctions()
    {
        panel.gameObject.SetActive(true);
        instructions.text = currStage.ToString().Replace("_", " ");
    }


    //if get component tutorialtrigger !=null
    //tutorialstage == this stage run function
    //If true break related function
    public bool BlockArea(TutorialStage tutorialStage)
    {
        //check if curr stage is infront of asked stage in enum
        if ((int)currStage < (int)tutorialStage)
        {
            return true;
        }
        else { return false; }
    }
}
