using UnityEngine;

public class PickUpCollectables : MonoBehaviour
{
    [SerializeField] Collectables[] collectableGrps;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerManager.instance.playerState == PlayerState.None)
        {
            if (PlayerManager.instance.objectInfrontOfPlayer == null) return;
            if (PlayerManager.instance.objectInfrontOfPlayer.CompareTag("Collectable") /*&& TooltipManager.instance.NearPlayer(PlayerManager.instance.objectInfrontOfPlayer.transform.position)*/)
            {
                PickUp();
            }
        }
    }
    void PickUp()
    {
        foreach (Collectables collectableGrp in collectableGrps)
        {
            foreach (IndiCollectable indiCol in collectableGrp.collectables)
            {
                if (indiCol.collectablePref.name.Contains(PlayerManager.instance.objectInfrontOfPlayer.name))
                {
                    if (collectableGrp.type == CollectableType.Food && !indiCol.collectablePref.name.ToLower().Contains("cooked"))
                    {
                        TutorialManager.instance.UpdateStage(TutorialStage.Go_To_Kitchen_And_Pick_Up_Food_Ingredient);
                    }

                    indiCol.quanity++;
                    Destroy(PlayerManager.instance.objectInfrontOfPlayer);
                    TooltipManager.instance.HideTooltip();
                    return;
                }
            }
        }
    }

}
