using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace TowerOfDefence.Game
{
    public class ObjectPoolManager : MonoBehaviour
    {
       
        private static ObjectPoolManager instance;
        public static ObjectPoolManager Instance { get { return instance; } }


        [SerializeField]
        private List<Pool> prefabList= new List<Pool>();
        private Dictionary<string,GameObject> prefabDictionary = new Dictionary<string, GameObject>();
        public List<Pool> GetPoolsInfo() { return prefabList; }

        private Dictionary<string, List<GameObject>> poolDictionary = new Dictionary<string, List<GameObject>>();
        private void Awake()
        {
            if (instance == null) instance = this;
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            foreach (Pool pool in prefabList)
            {
                prefabDictionary[pool.tag] = pool.prefab;
            }
        }

        private GameObject InstantiateObject(GameObject obj,Vector3 position, Quaternion rotation)
        {
            if (obj!= null) return Instantiate(obj,position,rotation);
            return null;
        }

        public GameObject SpawnObjectFromPool(string tag,Vector3 position,Quaternion rotation)
        {
            if(!prefabDictionary.ContainsKey(tag)) return null;
            if (!poolDictionary.ContainsKey(tag))
            {
                GameObject obj = InstantiateObject(prefabDictionary[tag],position,rotation);
                List <GameObject> list = new List<GameObject>();
                list.Add(obj);
                poolDictionary[tag] =list;
            }
            else
            {   
                foreach(GameObject obj in poolDictionary[tag])
                {
                    if (!obj.activeInHierarchy)
                    {
                        obj.transform.position = position;
                        obj.transform.rotation = rotation;
                        obj.SetActive(true);
                        return  obj;
                    }
                }
                GameObject newObj = InstantiateObject(prefabDictionary[tag], position, rotation);
                poolDictionary[tag].Add(newObj);
                return newObj;
            }
            return null;
        }
    }
}
