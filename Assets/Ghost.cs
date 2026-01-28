using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class ghostsketchfab : MonoBehaviour
{

    public NavMeshAgent agent;
    public float speed = 1;


    // Start is called before the first frame update
    void Start()
    {

       agent.updateRotation = false;

    }

    // Update is called once per frame
    void Update()
    {
        agent.updateRotation = false;

        Vector3 targetPosition = Camera.main.transform.position;

        agent.SetDestination(targetPosition);
        agent.speed = speed;

        
    }
}
