using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    bool attacking = false;
    PlayerManager playerManager;
    [SerializeField] Transform weaponHolder;
    Transform activeWeapon;
    Animator weaponAnim;
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }
    private void Update()
    {
        foreach (Transform child in weaponHolder)
        {
            if (child != null)
            {
                activeWeapon = child;
                break;
            }
        }
        if (activeWeapon == null) return;
        weaponAnim = activeWeapon.GetComponent<Animator>();
        if (weaponAnim == null) return;

        if (!attacking)
        {
            if (Input.GetAxis("Horizontal") + Input.GetAxis("Vertical") != 0)
            {
                weaponAnim.Play("Walking");
            }
            else
            {
                weaponAnim.Play("New State");
            }

            if (Input.GetMouseButtonDown(0) && playerManager.playerState == PlayerState.None)
            {
                weaponAnim.Play("Attack");
                attacking = true;
                StartCoroutine(DisableAttack(GetAttackTime()));

            }
        }
    }

    private float GetAttackTime()
    {

        AnimationClip[] clips = weaponAnim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Attack")
            {
                return clip.length;
            }

        }
        return 0;
    }
    IEnumerator DisableAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        attacking = false;
    }
    // Update is called once per frame
    void Attack(DamageManager damageManager)
    {

    }
}


