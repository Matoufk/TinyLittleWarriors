using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehavior : MonoBehaviour
{
    private Agent target;
    private enum AgentFSM
    {
        Attack,
        Seek,
        Idle
    }
    private AgentFSM state;
    [SerializeField] private seek seekScript;
    // Start is called before the first frame update
    void Start()
    {
        state = AgentFSM.Seek;
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case AgentFSM.Seek:
                if(target == null)
                {
                    Agent[] enemies = GameObject.FindObjectsOfType<Agent>();
                    GetNearestEnemy(enemies);
                    seekScript.setTarget(target.transform);
                }

                break;
        }
    }

    void GetNearestEnemy(Agent[] enemies)
    {
        Agent bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Agent potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        target = bestTarget;
    }
}
