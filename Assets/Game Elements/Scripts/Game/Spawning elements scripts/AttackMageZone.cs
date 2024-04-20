using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMageZone : MonoBehaviour
{
    [SerializeField] private ContentEnemy contentEnemy;
    private Mage mage;
    private InfoPieceOfPath infoPieceOfPath;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    public Animator animatorProperty => animator;

    [SerializeField] private AnimationClip attackAnimationClip;

    [Header("For Battle mages")]
    [SerializeField] private GameObject markGun;

    private bool isAttacked = false;
    public bool IsAttacked
    {
        get { return isAttacked; }
        set {  isAttacked = value; }
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
            infoPieceOfPath = contentEnemy.infoPieceOfPath;
            if (mage == null)
            {
                mage = contentEnemy.getMage();
            }

            mage.Attack(other.gameObject, infoPieceOfPath, animator, attackAnimationClip, gameManager, markGun);
            isAttacked = true;
        }
    }

}
