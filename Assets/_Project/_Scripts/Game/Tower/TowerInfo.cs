using UnityEngine;
namespace TowerOfDefence.Game
{

    [System.Serializable]
    public struct TowerInfo 
    {
        public string name;
        public int price;
        public TowerInfo(string name, int price)
        {
            this.name = name;
            this.price = price;
        }
    }
}
