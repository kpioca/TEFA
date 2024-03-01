using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : SpawnElement
{
    [Header("Bonus characteristics")]
    [SerializeField] private protected int durationSec = 10;

    public int DurationSec => durationSec;

    public virtual void Action() { }
}
