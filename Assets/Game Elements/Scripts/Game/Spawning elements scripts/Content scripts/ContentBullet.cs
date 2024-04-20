using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBullet : MonoBehaviour
{
    [SerializeField] private BulletInfo bullet_Info;
    public BulletInfo bulletInfo => bullet_Info;
}
