using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class planeFollowingWP : MonoBehaviour
{
    //for each node
    [SerializeField]
    bool _planeWaiting;

    //time we wait
    [SerializeField]
    float _totalWaitTime = 1f;

    //list of all waypoint nodes to visit
    [SerializeField]
    List<Waypoint> _planePoints;

    //Private variables for behavior
    NavMeshAgent _navMeshAgent;
    int _currentPlaneIndex;
    bool _travelling;
    bool _waiting;
    bool _planeForward;
    float _waitTimer;


    public void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            Debug.LogError("The Nav Mesh Agent is not attached to " + gameObject.name);
        }
        else
        {
            if (_planePoints != null && _planePoints.Count >= 2)
            {
                _currentPlaneIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Insufficient waypoint for basic moving behavior");
            }
        }
    }

        // Update is called once per frame
    public void Update()
     {
        //check if we're close to the waypoint
        if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
            {
                _travelling = false;
                //if we're going to wait, then wait.
                if (_planeWaiting)
                {
                    _waiting = true;
                    _waitTimer = 0f;
                }
                else
                {
                    ChangePlanePoint();
                    SetDestination();
                }
            }

        //Instead if we're waiting
        if (_waiting)
            {
                _waitTimer += Time.deltaTime;
                if (_waitTimer >= _totalWaitTime)
                {
                    _waiting = false;

                    ChangePlanePoint();
                    SetDestination();
                }
            }
        }

    private void SetDestination()
    {
        if (_planePoints != null)
        {
            Vector3 targetVector = _planePoints[_currentPlaneIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _travelling = true;
        }
    }
    private void ChangePlanePoint()
    {
        if (_planeForward)
        {
            _currentPlaneIndex = (_currentPlaneIndex + 1) % _planePoints.Count;
        }
        else
        {
            _waiting = true;
        }
    }
}
