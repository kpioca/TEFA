using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Enemy
{
    [Header("Spell Pull")]
    [SerializeField] MageSpellInfo[] spellPull;
    public MageSpellInfo[] SpellPull => spellPull;

    public Mage(MageInfo info, GameObject instance) : base(info, instance)
    {
        spellPull = info.SpellPull;
    }

    public virtual void Attack(GameObject player, InfoPieceOfPath infoPieceOfPath, Animator animator, AnimationClip attackAnimationClip, GameManager gameManager, GameObject markGun = null)
    {
        gameManager.StartCoroutine(AttackCoroutine(player, infoPieceOfPath, animator, attackAnimationClip, gameManager));
    }

    public virtual IEnumerator AttackCoroutine(GameObject player, InfoPieceOfPath infoPieceOfPath, Animator animator, AnimationClip attackAnimationClip, GameManager gameManager)
    {
        string animationAttackName = "Attack";
        int n = spellPull.Length;
        MageSpellInfo spell = getRandomElementFromList(spellPull);

        if (animator != null)
        {
            animator.enabled = true;
            animator.Play(animationAttackName);
        }

        yield return new WaitForSeconds((float)attackAnimationClip.length / 2);

        spell.ActivateSpell(gameManager, player, infoPieceOfPath);

        yield return new WaitForSeconds((float)attackAnimationClip.length / 2);
        animator.enabled = false;
    }

    private float[] getChances(MageSpellInfo[] listItems)
    {
        
        int n = listItems.Length;
        float[] chances = new float[n];

        if (n == 0) return null;
        else
        {
            int sum = 0;
            float medium;
            float chance = 0;

            List<int> levelOfCoolnessItems = new List<int>();
            for (int i = 0; i < n; i++)
            {
                levelOfCoolnessItems.Add(listItems[i].LevelOfCoolness);
                sum += listItems[i].LevelOfCoolness;
            }

            List<int> rateItems = new List<int>(levelOfCoolnessItems);
            rateItems.Reverse();

            medium = (float)sum / n;

            for (int i = 0; i < n; i++)
            {
                chance = rateItems[i] / (float)sum;
                chances[i] = chance;
            }
        }
        return chances;
    }

    private MageSpellInfo getRandomElementFromList(MageSpellInfo[] list)
    {
        float total_probability = 0;

        float[] chances = getChances(list);

        int numInList;

        foreach (float elem in chances)
        {
            total_probability += elem;
        }

        float randomPoint = UnityEngine.Random.value * total_probability;

        for (int i = 0; i < list.Length; i++)
        {
            if (randomPoint < chances[i])
            {
                numInList = i;
                return list[numInList];
            }
            else
            {
                randomPoint -= chances[i];
            }
        }
        numInList = list.Length - 1;
        return list[numInList];
    }


}
