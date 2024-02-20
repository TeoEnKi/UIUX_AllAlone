using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] bool stageComplete = false;
    [SerializeField] TutorialStage stageToComplete;
    [SerializeField] bool daughterFed = false;
    [SerializeField] bool gaveToy = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (stageToComplete != TutorialManager.instance.currStage) return;

        switch (stageToComplete)
        {
            case 0:
                if (PlayerManager.instance != null && Input.GetAxis("Horizontal") + Input.GetAxis("Vertical") != 0)
                {
                    stageComplete = true;
                    TutorialManager.instance.NextStage();
                }
                break;
            case (TutorialStage)1:
                if (daughterFed)
                {
                    stageComplete = true;
                    TutorialManager.instance.NextStage();
                }
                break;
            case (TutorialStage)2:
                if (gaveToy)
                {
                    stageComplete = true;
                    TutorialManager.instance.NextStage();
                }
                break;
            case (TutorialStage)3:
                if (PlayerPrefs.GetInt(PlayerManager.instance.mentHealthKey) < 0)
                {
                    stageComplete = true;
                    TutorialManager.instance.NextStage();
                    Debug.Log("player manager runs Die()");
                }
                break;
        }
    }
    public void UpdateObjective()
    {
        if (stageToComplete != TutorialManager.instance.currStage) return;

        if (stageToComplete == TutorialStage.Feed)
        {
            daughterFed = true;
            TutorialManager.instance.feedobjective.complete = true;
        }
        else if (stageToComplete == TutorialStage.GiveToy)
        {
            gaveToy = true;
            TutorialManager.instance.toyobjective.complete = true;
        }
    }
}
