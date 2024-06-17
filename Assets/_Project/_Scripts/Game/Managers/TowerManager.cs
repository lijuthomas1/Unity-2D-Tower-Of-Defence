using System.Collections.Generic;
using UnityEngine;
namespace TowerOfDefence.Game
{
    public class TowerManager : MonoBehaviour
    {

        
        public TowerInfo[] towerInfos;
        private static TowerManager instance;
        public static TowerManager Instance
        {
            get { return instance; }
        }        

        private int selectedTowerIndex = 0;

        private void Awake()
        {
             instance = this;
        }

        public TowerInfo GetSelectedTower()
        {
            if (selectedTowerIndex < 0) selectedTowerIndex = 0;
            if (selectedTowerIndex >= towerInfos.Length) selectedTowerIndex = towerInfos.Length-1;
            return towerInfos[selectedTowerIndex];
        }
    }
}
