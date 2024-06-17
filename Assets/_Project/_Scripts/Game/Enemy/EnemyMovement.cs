using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerOfDefence.Game
{
    public class EnemyMovement : MonoBehaviour
    {
        private Transform target;
        private int targetIndex = 0;
        private Vector3 direction = Vector3.zero;

        private void Start()
        {
            ResetPathTarget();
        }
        private void OnEnable()
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

        private void LookTowardsTarget()
        {
            if(target == null)  return;
            Vector3 look = transform.InverseTransformPoint(target.position);
            float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90 ;

            transform.Rotate(0,0, angle);
        }
        private void ResetPathTarget()
        {
            targetIndex = 0;
            SetCurrentTargetPoint();
            LookTowardsTarget();
        }

        private void Update()
        {
            if (target == null) return;
            if (Vector3.Distance(transform.position, target.position) < 0.1)
            {
                FindNextTarget();
                LookTowardsTarget();
            }

            if (target == null)
            {   this.gameObject.SetActive(false);
                return;
            }
                
            direction = (target.position - this.transform.position).normalized;
            
            this.transform.position += direction * Time.deltaTime;
        }
    }
}
