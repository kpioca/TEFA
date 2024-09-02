using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [SerializeField] private ContentEnemy contentEnemy;
    private Cannon cannon;
    [SerializeField] private GameObject[] marksGun;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    public Animator animatorProperty => animator;
    [SerializeField] private AnimationClip attackAnimationClip;
    private int amount_AttackAnimations = 1;
    private float intervalBetweenShots;

    private bool isAttacked = false;
    public bool IsAttacked
    {
        get { return isAttacked; }
        set { isAttacked = value; }
    }

    GameManager gameManager;
    public GameManager game_Manager
    {
        get { return gameManager; }
        set { gameManager = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isAttacked)
        {
            StartCoroutine(AttackCoroutine(other.gameObject));
        }
    }

    private IEnumerator AttackCoroutine(GameObject player)
    {
        string animationAttackName = "Attack";
        if (cannon == null)
        {
            cannon = contentEnemy.getCannon();
            amount_AttackAnimations = contentEnemy.N_shots;
            intervalBetweenShots = contentEnemy.IntervalBetweenShots;
        }
        if (animator != null)
        {
            animator.enabled = true;
            animator.Play(animationAttackName);
        }
        cannon.Attack(marksGun, game_Manager.SpeedRouteMovement, player.transform.position, game_Manager);
        if (animator != null && amount_AttackAnimations > 1)
        {
            if (amount_AttackAnimations > 1)
            {
                int n = -1;
                yield return new WaitForSeconds(intervalBetweenShots);
                n = 1;
                animator.SetInteger("n_attack", n);

                for (int i = 0; i < amount_AttackAnimations - 2; i++)
                {
                    yield return new WaitForSeconds(intervalBetweenShots);
                    n++;
                    animator.SetInteger("n_attack", n);
                }
                yield return new WaitForSeconds(attackAnimationClip.length);
            }
        }
        else if(animator != null && amount_AttackAnimations == 1)
            yield return new WaitForSeconds(attackAnimationClip.length);
        animator.Rebind();
        animator.enabled = false;
        cannon = null;
        isAttacked = true;
    }


}
