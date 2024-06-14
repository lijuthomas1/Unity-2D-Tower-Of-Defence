using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerOfDefence.Game
{
    public class EnemyMovement : MonoBehaviour
    {
        private Transform target;
        private int targetIndex = 0;
        private void Start()
        {
            SetFirstPathTarget();
        }

        // Update is called once per frame
        private void SetFirstPathTarget()
        {
            targetIndex = 0;
            target = LevelManager.Instance.PathPoints[targetIndex];
        }
    }
}
