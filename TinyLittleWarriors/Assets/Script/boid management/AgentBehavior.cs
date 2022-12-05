using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehavior : MonoBehaviour
{
    private Agent target;
    public enum AgentFSM
    {
        Attack,
        Seek,
        Idle
    }
    [SerializeField] private AgentFSM state;
    private seek seekScript;
    // Start is called before the first frame update
    void Start()
    {
        seekScript = GetComponent<seek>();
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
                seekScript.setTarget(target.transform);
                seekScript.seeking();
                break;

            case AgentFSM.Attack:
                print("ATTAK");
                int range = (int)GetComponent<CharacterStats>().getRange();
                if (Vector3.Distance(target.transform.position, transform.position) > range)
                {
                    state = AgentFSM.Seek;
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
            if (potentialTarget.getFaction() == Agent.Faction.Enemy)
            {
                Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
        }

        target = bestTarget;
    }
    public void setState(AgentFSM etat)
    {
        state = etat;
    }
}
