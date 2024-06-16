using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerOfDefence.Game
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float bulletSpeed = 3f;
        [SerializeField]
        private LayerMask targetMask;
        private Transform target;
        private bool isTargetAssigned = false;

        private void OnEnable()
        {
            isTargetAssigned = false;
        }
        public void SetTarget(Transform target)
        {
            this.target = target;
            isTargetAssigned = true;
        } 
        private void Update()
        {
            if (!isTargetAssigned) return;
            if (target == null) this.gameObject.SetActive(false);
            FollowTarget();
        }
        private void    FollowTarget()
        {
            if (target == null) return;
            Vector3 direction = (target.position -transform.position).normalized;
            transform.position += direction * Time.deltaTime * bulletSpeed;
        }

    }
}
