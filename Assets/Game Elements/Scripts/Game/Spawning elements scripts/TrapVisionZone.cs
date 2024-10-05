using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapVisionZone : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip animationClip;
    [SerializeField] string animationID = "fish";
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.activeInHierarchy)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(animationCoroutine(animator, animationID));
            }
        }
    }

    private IEnumerator animationCoroutine(Animator animator, string animationID)
    {
        animator.enabled = true;
        animator.Play(animationID);
        yield return new WaitForSeconds(animationClip.length);
        animator.Rebind();
        animator.enabled = false;
    }

    private void OnDisable()
    {
        animator.Rebind();
        animator.enabled = false;
    }


}
