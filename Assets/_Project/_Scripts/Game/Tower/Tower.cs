using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float towerRange = 2f;

    private Transform targetEnemy;
    private void FixedUpdate()
    {
        if (targetEnemy != null)
        {
            if (!targetEnemy.gameObject.activeInHierarchy)
            {
                FindTargetEnemy();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position,transform.forward,towerRange);
    }
    private void FindTargetEnemy()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position,transform.forward,towerRange);
        if(hits.Length > 0) targetEnemy = hits[0].transform;
        // print("target " + targetEnemy.name);
        
    }
}
