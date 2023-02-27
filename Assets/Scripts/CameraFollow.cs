using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _Target;
    private Vector3 offset_target;
    void Start()
    {
        offset_target = transform.position - _Target.position;
    }
    
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _Target.position + offset_target, .125f);
    }
}
