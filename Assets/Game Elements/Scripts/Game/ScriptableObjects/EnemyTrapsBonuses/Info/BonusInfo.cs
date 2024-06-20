using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusInfo : SpawnElementInfo
{
    [Header("Icon")]
    [SerializeField] private protected Sprite icon;
    public Sprite Icon => icon;

    [Header("General bonus characteristics")]
    [SerializeField] private protected int durationSec = 10;

    public int DurationSec => durationSec;

    [SerializeField] private protected bool haveEffect = true;

    public bool HaveEffect => haveEffect;

    public virtual void Action(GameManager gameManager, ContentPlayer contentPlayer) { }
}
