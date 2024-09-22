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

    [Header("Particles")]
    [SerializeField] GameObject particles;
    public GameObject Particles => particles;

    public virtual void ActivateSpell(GameManager gameManager, GameObject player, InfoPieceOfPath infoPieceOfPath)
    {
        
    }

    protected void SpawnParticles(Transform transform, MonoBehaviour forCoroutine)
    {
        forCoroutine.StartCoroutine(particlesCoroutine(particles, transform));
    }
    protected IEnumerator particlesCoroutine(GameObject destructionParticlesPrefab, Transform transform)
    {
        GameObject destructionParticlesInstance;
        destructionParticlesInstance = KhtPool.GetObject(destructionParticlesPrefab);
        destructionParticlesInstance.SetActive(true);
        destructionParticlesInstance.transform.SetParent(null);
        destructionParticlesInstance.transform.position = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
        destructionParticlesInstance.transform.parent = transform;
        yield return new WaitForSeconds(2f);
        KhtPool.ReturnObject(destructionParticlesInstance);
    }

}
