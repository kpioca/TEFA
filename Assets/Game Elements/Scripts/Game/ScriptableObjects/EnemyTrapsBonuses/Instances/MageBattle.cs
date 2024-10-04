using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBattle : Mage
{
    [Header("Projectile Reference")]
    [SerializeField] private protected BulletInfo bullet_Info;
    public BulletInfo bulletInfo => bullet_Info;

    [Header("Projectile Pooling")]
    [SerializeField] private protected int sizeOfProjectilePool = 8;
    public int SizeOfProjectilePool => sizeOfProjectilePool;

    private protected ContentBullet contentBullet;
    public MageBattle(MageBattleInfo info, GameObject instance) : base(info, instance)
    {
        bullet_Info = info.bulletInfo;
        sizeOfObjectPool = info.SizeOfProjectilePool;
    }

    public override void Attack(GameObject player, InfoPieceOfPath infoPieceOfPath, Animator animator, AnimationClip attackAnimationClip, GameManager gameManager, GameObject markGun = null)
    {
        gameManager.StartCoroutine(AttackCoroutine(player, infoPieceOfPath, animator, attackAnimationClip, gameManager, markGun));
    }

    public virtual IEnumerator AttackCoroutine(GameObject player, InfoPieceOfPath infoPieceOfPath, Animator animator, AnimationClip attackAnimationClip, GameManager gameManager, GameObject markGun)
    {
        string animationAttackName = "Attack";

        if (animator != null)
        {
            animator.enabled = true;
            animator.Play(animationAttackName);
        }

        yield return new WaitForSeconds((float)attackAnimationClip.length * 2/5);

        SpellPull[0].ActivateSpell(gameManager, player, infoPieceOfPath);
        yield return new WaitForSeconds((float)attackAnimationClip.length * 1 / 5);
        shoot(markGun, gameManager.getPlatformSpeedForBullets(), player.transform.position, gameManager);

        yield return new WaitForSeconds((float)attackAnimationClip.length * 2/5);
        animator.enabled = false;
    }

    public virtual void shoot(GameObject markGun, float platformsSpeed, Vector3 target, MonoBehaviour toUseCoroutines = null)
    {
        GameObject bullet;
        bullet = bulletInfo.spawnBullet(bulletInfo.Prefab, markGun, null, stamp, out contentBullet);

        MoveToPos(toUseCoroutines, bullet, target + bullet.transform.forward * 2, bulletInfo, platformsSpeed, contentBullet);
    }

    public virtual void MoveToPos(MonoBehaviour toUseCoroutines, GameObject obj, Vector3 target, BulletInfo bulletInfo, float platformsSpeed, ContentBullet contentBullet)
    {
        toUseCoroutines.StartCoroutine(MovementCoroutine(obj, target, bulletInfo.Speed + platformsSpeed, contentBullet));

        //projectile_rb = obj.GetComponent<Rigidbody>();
        //projectile_rb.velocity = obj.transform.forward * (bulletInfo.Speed + platformsSpeed);
    }

    private protected virtual IEnumerator MovementCoroutine(GameObject obj, Vector3 target, float speed, ContentBullet contentBullet)
    {
        Vector3 start_pos = obj.transform.position;

        float runningTime = 0;
        float totalRunningTime = Vector3.Distance(start_pos, target) / speed;

        while (runningTime < totalRunningTime)
        {
            runningTime += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(start_pos, target, runningTime / totalRunningTime);
            yield return 1;
        }
        if (contentBullet.gameObject.activeInHierarchy)
            contentBullet.Remove();
    }


}
