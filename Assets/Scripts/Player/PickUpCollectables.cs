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
        foreach(Collectables collectableGrp in collectableGrps)
        {
            foreach(IndiCollectable indiCol in collectableGrp.collectables)
            {
                if (indiCol.collectablePref.name.Contains(PlayerManager.instance.objectInfrontOfPlayer.name))
                {
                    Debug.Log(indiCol.collectablePref.name +" "+PlayerManager.instance.objectInfrontOfPlayer.name);

                    indiCol.quanity++;
                    Destroy(PlayerManager.instance.objectInfrontOfPlayer);
                    TooltipManager.instance.HideTooltip();
                    return;
                }
            }
        }
    }

}
