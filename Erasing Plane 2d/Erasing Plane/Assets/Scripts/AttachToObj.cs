using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToObj : MonoBehaviour
{
   [SerializeField] private Transform target;
   [SerializeField] private Vector3 offset; 

    void Update()
    {
        if (target != null)
        {           
            transform.position = target.position + offset;            
            transform.rotation = target.rotation; 
        }
    }
}
