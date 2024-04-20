using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dissolving : MonoBehaviour
{
    [SerializeField] RawImage rawImage;
    [SerializeField] private TMP_Text text_info_level;
    Material material;
    [SerializeField] float duration;
    [SerializeField] float smoothness = 0.01f;
    void Start()
    {
        material = GetComponent<RawImage>().material;
        StartCoroutine(appearance(material, smoothness, duration));
    }

    void Update()
    {
        
    }

    IEnumerator appearance(Material material, float alphaAdd, float duration)
    {
        float alpha = 0f;

        material.SetFloat("_alphaClip", 0);
        text_info_level.enabled = true;

        int n = (int)(1 / alphaAdd);
        float num_disable_text = 2*n/3;


        for (int i = 0; i < n; i++)
        {
            alpha += alphaAdd;
            material.SetFloat("_alphaClip", alpha);
            if (i == (int)num_disable_text)
            {
                text_info_level.enabled = false;
                gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(duration / n);
        }
        material.SetFloat("_alphaClip", 0);
        
    }
}
