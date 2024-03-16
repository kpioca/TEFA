using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhtPool : KhtSingleton<KhtPool>
{
    // Хак из-за отсутствия поддержки отображения Dictionary в инспекторе
    // Словарь с необходимыми объектами будет создаваться из листа с описанием префабов

    [System.Serializable]
    public class PrefabData  //вид объектов для пула
    {
        public GameObject prefab;
        public int initPoolSize = 0;
    }
    [SerializeField] private List<PrefabData> prefabDatas = null;  //список разных объектов, для которых нужны пулы

    // Привязка пула к идентификатору префаба
    private readonly Dictionary<int, Queue<GameObject>> _pools = new Dictionary<int, Queue<GameObject>>();  //словарь с пулами и идентификаторами их префабов
    // Привязка объекта к пулу по его идентификатору
    private readonly Dictionary<int, int> _objectToPoolDict = new Dictionary<int, int>();  

    private new void Awake()
    {
        // Настройка синглетона
        base.Awake();

        // При необходимости предварительно наполняем пулы объектов
        foreach (var prefabData in prefabDatas)
        {
            _pools.Add(prefabData.prefab.GetInstanceID(), new Queue<GameObject>());
            for (int i = 0; i < prefabData.initPoolSize; i++)
            {
                GameObject retObject = Instantiate(prefabData.prefab, Instance.transform, true);
                Instance._objectToPoolDict.Add(retObject.GetInstanceID(), prefabData.prefab.GetInstanceID());
                Instance._pools[prefabData.prefab.GetInstanceID()].Enqueue(retObject);
                retObject.SetActive(false);
            }
        }
        prefabDatas = null;
    }

    // Получение объекта из пула
    public static GameObject GetObject(GameObject prefab)
    {
        // В случае отсутствия синглетона просто создаём новый объект
        if (!Instance)
        {
            return Instantiate(prefab);
        }

        // Уникальный идентификатор префаба, по которому осуществляется привязка к пулу
        int prefabId = prefab.GetInstanceID();
        // Если пула для префаба не существует, то создаём новый
        if (!Instance._pools.ContainsKey(prefabId))
        {
            Instance._pools.Add(prefabId, new Queue<GameObject>());
        }

        // При наличии объекта в пуле возвращаем его
        if (Instance._pools[prefabId].Count > 0)
        {
            return Instance._pools[prefabId].Dequeue();
        }

        // В случае нехватки объектов создаём новый
        GameObject retObject = Instantiate(prefab);
        // Добавляем привязку объекта к пулу по его идентификатору
        Instance._objectToPoolDict.Add(retObject.GetInstanceID(), prefabId);

        return retObject;
    }

    // Возврат объекта в пул
    public static void ReturnObject(GameObject poolObject)
    {
        // В случае отсутствия синглетона просто уничтожаем объект
        if (!Instance)
        {
            Destroy(poolObject);
            return;
        }

        // Идентификатор объекта для определения пула
        int objectId = poolObject.GetInstanceID();

        // В случае отсутствия привязки объекта к пулу просто его уничтожаем
        if (!Instance._objectToPoolDict.TryGetValue(objectId, out int poolId))
        {
            Destroy(poolObject);
            return;
        }

        // Возвращаем объект в пул
        Instance._pools[poolId].Enqueue(poolObject);
        poolObject.transform.SetParent(Instance.transform);
        poolObject.SetActive(false);

    }
}
