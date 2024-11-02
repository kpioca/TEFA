using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoad : MonoBehaviour
{
    [SerializeField] private SkinDatabase skinDatabase;
    [SerializeField] Transform parent;

    Vector3 localPositionSpawn = new Vector3(0.009f, -3.354131f, 3.339984f);
    Quaternion localRotation = Quaternion.Euler(new Vector3(-21.412f, 0, 0));

    private CatSkin selectedSkin;

    private GameObject Prefab => selectedSkin.PrefabGame;


    public void Inititialize(IPersistentData persistentData, out ContentPlayer contentPlayer)
    {
        selectedSkin = GetSelectedSkin(persistentData.saveData.SelectedCatSkinType);

        GameObject player = KhtPool.GetObject(Prefab);
        player.transform.parent = parent;
        player.transform.localPosition = localPositionSpawn;
        player.transform.localRotation = localRotation;
        player.SetActive(true);
        contentPlayer = player.GetComponent<ContentPlayer>();

    }

    public CatSkin GetSelectedSkin(CatSkinsEnum catSkin)
    {
        return skinDatabase.CatSkins.Find(skin => skin.SkinType == catSkin);
    }
}
