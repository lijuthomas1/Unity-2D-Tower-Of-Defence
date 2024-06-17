using UnityEngine;
namespace TowerOfDefence.Game
{

    [System.Serializable]
    public struct TowerInfo 
    {
        public string name;
        public int price;
        public GameObject prefab;
        public TowerInfo(string name, int price, GameObject prefab)
        {
            this.name = name;
            this.price = price;
            this.prefab = prefab;
        }
    }
}
