using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinPlacement : MonoBehaviour
{
    private const string RenderLayer = "SkinRender";
    [field: SerializeField] public Rotator _rotator { get; private set; }
    private GameObject _currentModel;
    public void InstantiateModel(GameObject model, Vector3 position)
    {
        if(_currentModel != null)
            KhtPool.ReturnObject(_currentModel.gameObject);

        _rotator.ResetRotation();

        _currentModel = KhtPool.GetObject(model);
        //position, Quaternion.identity, this.transform
        _currentModel.SetActive(true);
        _currentModel.transform.position = position;
        _currentModel.transform.eulerAngles = new Vector3(0, 180, 0);
        _currentModel.transform.SetParent(transform);

        Transform[] childrens = _currentModel.GetComponentsInChildren<Transform>();

        foreach(var item in childrens)
        {
            item.gameObject.layer = LayerMask.NameToLayer(RenderLayer);
        }
    }
}
