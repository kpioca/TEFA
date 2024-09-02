using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBullet : MonoBehaviour
{
    [SerializeField] private BulletInfo bullet_Info;
    public BulletInfo bulletInfo => bullet_Info;

    public Bullet bulletInstance { get; set; }

    [SerializeField] private GameObject bulletMainObject;
    public GameObject BulletMainObject => bulletMainObject;
}
