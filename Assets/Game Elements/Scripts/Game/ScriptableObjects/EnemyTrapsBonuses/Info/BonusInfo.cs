using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusInfo : SpawnElementInfo
{
    [Header("General bonus characteristics")]
    [SerializeField] private protected int durationSec = 10;

    public int DurationSec => durationSec;

    [SerializeField] private protected bool haveEffect = true;

    public bool HaveEffect => haveEffect;

    public virtual void Action(GameManager gameManager, ContentPlayer contentPlayer) { }
}
