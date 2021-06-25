using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    /// <summary>
    /// Anything that needs to be pooled, is added into the array.
    /// at start, it creates pools and adds them to the the dictionary, with the objects name being the key.
    /// it then generates 10 of the time. Another class can request and item by name and it will send it over. 
    /// </summary>
    #region Singleton Pattern
    private static ObjectPooler _instance;
    public static ObjectPooler Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("The ObjectPooler is Null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion
    [SerializeField]
    GameObject[] _poolObject;
    [SerializeField]
    GameObject _objectHolder;
    [SerializeField]
    private Dictionary<string, List<GameObject>> _objectPool = new Dictionary<string, List<GameObject>>();


    private void Start()
    {
       
        CreatePools();
    }
    public void CreatePools()
    {

        foreach (GameObject GO in _poolObject)
        {
            List<GameObject> list = new List<GameObject>();
            _objectPool.Add(GO.name,list );

            for (var i = 0; i <= 10; i++)
            {
                var newGameobject = Instantiate(GO);
                newGameobject.SetActive(false);
                newGameobject.transform.parent = _objectHolder.transform;
                list.Add(newGameobject);
            }
           
        }
      
    }

    public GameObject RequestGameobject(GameObject requestedObject)
    {
        var pool = _objectPool[requestedObject.name];
        var i = 0;
        foreach (GameObject obj in pool)
        {
            Debug.Log(i);
            Debug.Log("Name: " + obj.name);
         
            if (obj.activeSelf == false)
            {
                Debug.Log("we made it");
                obj.SetActive(true);
                return obj;
            }
            i++;
        }
        Debug.Log("all are active");
        var newObj = Instantiate(requestedObject,_objectHolder.transform);
        pool.Add(newObj);
        return newObj;
    }

}
