using System.Collections.Generic;
using UnityEngine;
namespace TowerOfDefence.Game
{
    public class TowerManager : MonoBehaviour
    {
        private static TowerManager instance;
        public static TowerManager Instance
        {
            get { return instance; }
        }
        [SerializeField]
        private GameObject[] towerPrefabs;

        private int selectedTowerIndex = 0;

        private void Awake()
        {
             instance = this;
        }

        public GameObject SelectedGameObject()
        {
            if (selectedTowerIndex < 0 || selectedTowerIndex >= towerPrefabs.Length) return null;
            return towerPrefabs[selectedTowerIndex];
        }
    }
}
