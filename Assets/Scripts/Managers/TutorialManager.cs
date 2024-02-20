using UnityEngine;
public enum TutorialStage { Move, Feed, GiveToy, Die};

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    public TutorialStage currStage;
    public Objective feedobjective;
    public Objective toyobjective;

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
    }
    private void Start()
    {
        currStage = TutorialStage.Move;
    }
    public void NextStage()
    {
        currStage = (TutorialStage) ((int)currStage + 1);
    }
}
