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
    [SerializeField] private bool hasIdleAnimation = false;
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
            if (gameObject.transform.position.z > contentEnemy.getEnemy().AttackStopZ)
            {
                if(!hasIdleAnimation)
                StartCoroutine(AttackCoroutine(other.gameObject));
                else StartCoroutine(AttackCoroutineWithIdle(other.gameObject));

            }
        }
    }

    
    private IEnumerator AttackCoroutine(GameObject player)
    {
        string animationAttackName = "Attack";
        if (cannon == null)
        {
            cannon = contentEnemy.getCannon();
            float value;

            if (contentEnemy.NumParameters != null)
            {
                if (contentEnemy.NumParameters.TryGetValue("n_shots", out value))
                    amount_AttackAnimations = (int)value;

                if (contentEnemy.NumParameters.TryGetValue("intervalBetweenShots", out value))
                    intervalBetweenShots = value;
            }
        }
        if (animator != null)
        {
            animator.enabled = true;
            animator.Play(animationAttackName);
        }
        cannon.Attack(marksGun, game_Manager.getPlatformSpeedForBullets(), player.transform.position, game_Manager);
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
        cannon = null;
        isAttacked = true;
        animator.enabled = false;
    }

    private IEnumerator AttackCoroutineWithIdle(GameObject player)
    {
        if (cannon == null)
        {
            cannon = contentEnemy.getCannon();
            float value;

            if (contentEnemy.NumParameters != null)
            {
                if (contentEnemy.NumParameters.TryGetValue("n_shots", out value))
                    amount_AttackAnimations = (int)value;

                if (contentEnemy.NumParameters.TryGetValue("intervalBetweenShots", out value))
                    intervalBetweenShots = value;
            }
        }
        if (animator != null)
        {
            animator.SetBool("isAttack", true);
        }
        cannon.Attack(marksGun, game_Manager.getPlatformSpeedForBullets(), player.transform.position, game_Manager);
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
        else if (animator != null && amount_AttackAnimations == 1)
            yield return new WaitForSeconds(attackAnimationClip.length);
        animator.SetBool("isAttack", false);
    }

    private void OnDisable()
    {
        animator.Rebind();
        cannon = null;
    }


}
