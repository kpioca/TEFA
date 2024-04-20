using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSpellInfo : ScriptableObject
{
    [SerializeField] private protected string spellName = "";
    public string SpellName => spellName;

    [SerializeField] private protected string spellId = "";
    public string SpellId => spellId;

    [SerializeField] private protected int levelOfCoolness;
    public int LevelOfCoolness => levelOfCoolness;

    public virtual void ActivateSpell(GameManager gameManager, GameObject player, InfoPieceOfPath infoPieceOfPath)
    {
        
    }

}
