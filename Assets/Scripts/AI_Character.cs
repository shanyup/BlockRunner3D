using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Character : MonoBehaviour
{
    public GameManager _GameManager;
    public Transform _Transform;
    private NavMeshAgent _NavMeshAgent;
    void Start()
    {
        _NavMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        _NavMeshAgent.SetDestination(_Transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cutter"))
        {
            Vector3 newTransform = new Vector3(transform.position.x, 0.23f, transform.position.z);
            _GameManager.AI_InstantiateDeathEffect(newTransform);
            gameObject.SetActive(false);
        }
    }
}
