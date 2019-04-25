using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class planeAI : MonoBehaviour
{
    [SerializeField]
    public Transform _destination;
    private NavMeshAgent _agent;
    private Animation animations;

    // Start is called before the first frame update
    void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.LogError("The Nav Mesh Agent is not attached to " + gameObject.name);
        }
        else
        {
            SetDestination();
        }

        animations = gameObject.GetComponent<Animation>();
    }

    public void SetDestination()
    {
        if (_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            Debug.Log(targetVector);
            _agent.SetDestination(targetVector);
        }
    }
}
