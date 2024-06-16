using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float towerRange = 2f;
    [SerializeField]
    private LayerMask enemyMask;
    private Transform targetEnemy;
    private void FixedUpdate()
    {
        if (targetEnemy != null)
        {
            if (!targetEnemy.gameObject.activeInHierarchy) targetEnemy = null;
            if (Vector3.Distance(transform.position,targetEnemy.position) > towerRange) targetEnemy = null;
        }

        if (targetEnemy == null) FindTargetEnemy();
    }
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position,transform.forward,towerRange);
    }
    private void FindTargetEnemy()
    {
        //print("FindTargetEnemy");
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position,towerRange,transform.forward,towerRange,enemyMask);
        if(hits.Length > 0) targetEnemy = hits[0].transform;
        if(targetEnemy != null) print("target " + targetEnemy.name);
        
    }
}
