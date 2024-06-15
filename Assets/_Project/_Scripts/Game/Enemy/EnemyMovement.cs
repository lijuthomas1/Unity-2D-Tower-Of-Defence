using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerOfDefence.Game
{
    public class EnemyMovement : MonoBehaviour
    {
        private Transform target;
        private int targetIndex = 0;
        private float distatance = 0;
        private Vector3 direction = Vector3.zero;
        private void Start()
        {
            ResetPathTarget();
        }

        private void SetCurrentTargetPoint()
        {
            if (LevelManager.Instance.PathPoints.Count > targetIndex)
            {
                target = LevelManager.Instance.PathPoints[targetIndex];
            }
            else
            {
                target = null;
            }
        }

        private void FindNextTarget()
        {
            targetIndex++;
            SetCurrentTargetPoint();
        }
        private void ResetPathTarget()
        {
            targetIndex = 0;
            SetCurrentTargetPoint();
        }

        private void Update()
        {
            if (target == null) return;
            if (Vector3.Distance(transform.position, target.position) < 0.01) FindNextTarget();

            if (target == null) return;
            direction = (target.position - this.transform.position).normalized; 
            
            this.transform.position += direction * Time.deltaTime;
        }
    }
}
