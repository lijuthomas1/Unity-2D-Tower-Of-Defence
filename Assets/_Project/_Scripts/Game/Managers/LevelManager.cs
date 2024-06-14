using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerOfDefence.Game
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private List<Transform> pathPoint;
        private static LevelManager instance;
        public static LevelManager Instance => instance;
        public List<Transform> PathPoints {  get { return pathPoint; } }

        private void Awake ()
        {
            instance = this;
        }
        
    }
}
