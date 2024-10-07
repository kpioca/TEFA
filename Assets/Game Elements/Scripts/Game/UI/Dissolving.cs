using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DissolvingStartWindow : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private TMP_Text text_info_level;
    Material material;
    [SerializeField] float duration;
    [SerializeField] float smoothness = 0.01f;
    void Start()
    {
        material = GetComponent<Image>().material;

        material.SetFloat("_alphaClip", 0);
    }

    void Update()
    {
        
    }

    public void dissolveAnimation()
    {
        material = GetComponent<Image>().material;
        StartCoroutine(appearance(material, smoothness, duration));
    }

    IEnumerator appearance(Material material, float alphaAdd, float duration)
    {
        float alpha = 0f;

        material.SetFloat("_alphaClip", 0);
        text_info_level.enabled = true;

        int n = (int)(1 / alphaAdd);


        for (int i = 0; i < n; i++)
        {
            alpha += alphaAdd;
            material.SetFloat("_alphaClip", alpha);
            yield return new WaitForSeconds(duration / n);
        }
        material.SetFloat("_alphaClip", 0);
        gameManager.allEnable();
        gameObject.SetActive(false);
        //changeStateObjects(UIobjectsToDisable, true);

    }

    public void changeStateObjects(GameObject[] objects, bool state)
    {
        int n = objects.Length;

        for(int i = 0; i < n; i++)
            objects[i].SetActive(state);
    }
}
