using UnityEngine;

public class NextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialManager.instance != null)
        {
            if (TutorialManager.instance.BlockArea(TutorialStage.Go_Out_And_Kill_It)) return;
        }
        if (PlayerManager.instance.objectInfrontOfPlayer == null) return;
        if(Input.GetKeyDown(KeyCode.E)&& PlayerManager.instance.objectInfrontOfPlayer.CompareTag("Door"))
        {
            BackgroundAudioManager.instance.StopStaticNoise();
            PlayerManager.instance.MoveToNextScene();
        }
    }
}
