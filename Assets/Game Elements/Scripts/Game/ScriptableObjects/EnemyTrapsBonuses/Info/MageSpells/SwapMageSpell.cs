using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "SwapMageSpellInfo", menuName = "LevelProperties/Enemy/Mage Spells/New SwapMageSpellInfo")]
public class SwapMageSpellInfo : MageSpellInfo
{
    public override void ActivateSpell(GameManager gameManager, GameObject player, InfoPieceOfPath infoPieceOfPath)
    {
        List<SpawnPlace> enemies = infoPieceOfPath.Enemies;
        int n = enemies.Count;
        int k1 = 0, k2 = 0;
        k1 = Random.Range(0, n / 2);
        k2 = Random.Range(n / 2, n);

        GameObject movable1 = enemies[k1].obj.GetComponent<ContentEnemy>().visionZone.MovablePart;
        GameObject movable2 = enemies[k2].obj.GetComponent<ContentEnemy>().visionZone.MovablePart;

        Transform transform1 = enemies[k1].obj.transform;
        Transform transform2 = enemies[k2].obj.transform;

        Vector3 position1 = transform1.position;
        Vector3 position2 = transform2.position;

        if (position1 != position2)
        {
            Mark mark1 = enemies[k1].mark;
            Mark mark2 = enemies[k2].mark;

            enemies[k1].obj.transform.position = position2;
            enemies[k2].obj.transform.position = position1;
            enemies[k1].mark = mark2;
            enemies[k2].mark = mark1;

            Vector3 direction1 = player.transform.position + new Vector3(0, 0, 0.3f) - movable1.transform.position;
            Quaternion rotation1 = Quaternion.LookRotation(direction1);

            Vector3 direction2 = player.transform.position + new Vector3(0, 0, 0.3f) - movable2.transform.position;
            Quaternion rotation2 = Quaternion.LookRotation(direction2);

            movable1.transform.rotation = rotation1;
            movable2.transform.rotation = rotation2;

            SpawnParticles(transform2, gameManager);
        }
        SpawnParticles(transform1, gameManager);
    }
}
