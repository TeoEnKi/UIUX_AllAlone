using UnityEngine;

public class GoToExit : MonoBehaviour
{
    [SerializeField] IndiCollectable[] healthCollects;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider collision)
    {
        if (PlayerManager.instance == null || TutorialManager.instance == null) return;
        if (collision.tag == "Player" && !TutorialManager.instance.BlockArea(TutorialStage.Investigate_Noise))
        {
            Debug.Log(TutorialManager.instance.BlockArea(TutorialStage.Investigate_Noise));
            PlayerManager.instance.ChangeMentState(-30);
            PlayerManager.instance.ChangePhyState(-40);
            TutorialManager.instance.UpdateStage(TutorialStage.Investigate_Noise);
            foreach (IndiCollectable health in healthCollects)
            {
                health.quanity = 1;
            }
        }
    }

}
