using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectInfo : ScriptableObject
{
    [SerializeField] private protected string effectName = "";
    public string EffectName => effectName;

    [SerializeField] private protected string effectId = "";
    public string EffectId => effectId;

    [Header("Shader")]
    [SerializeField] private protected Material effectMaterial;
    public Material EffectMaterial => effectMaterial;

    [Header("Characteristics")]
    [SerializeField] private protected int durationSec = 10;

    public int DurationSec => durationSec;

    [SerializeField] private protected bool haveTimer = false;

    public bool HaveTimer => haveTimer;


    public virtual void ApplyEffect(GameManager gameManager, ContentPlayer contentPlayer, out int duration)
    {
        duration = 0;
    }

    public virtual IEnumerator EffectCoroutine(GameManager gameManager, ContentPlayer contentPlayer, int duration)
    {
        yield return null;
    }
}
