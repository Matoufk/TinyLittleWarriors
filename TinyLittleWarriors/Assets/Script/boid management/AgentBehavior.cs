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
    private Seek seekScript;
    private float nextAttackTime;
    // Start is called before the first frame update
    void Start()
    {
        seekScript = GetComponent<Seek>();
        state = AgentFSM.Seek;
        target = null;
        nextAttackTime = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case AgentFSM.Seek:
                if (target == null)
                {
                    Agent[] enemies = GameObject.FindObjectsOfType<Agent>();
                    GetNearestEnemy(enemies);
                    if (target == null) state = AgentFSM.Idle;
                    else seekScript.setTarget(target.transform);
                }
                else
                {
                    seekScript.setTarget(target.transform);
                    seekScript.seeking();
                }
                break;

            case AgentFSM.Attack:
                CharacterStats stats = GetComponent<CharacterStats>();
                int range = (int)stats.getRange();
                if (target == null || Vector3.Distance(target.transform.position, transform.position) > range + seekScript.offset)
                {
                    state = AgentFSM.Seek;
                }
                else
                {
                    float atkSpeed = 1.0f/stats.getAttackSpeed();
                    CharacterStats foeStats = target.GetComponent<CharacterStats>();
                    if(nextAttackTime == -1.0f || Time.time >= nextAttackTime)
                    {
                        int dmg = stats.getAttack() - foeStats.getDefense();
                        if (dmg < 0) dmg = 0;
                        target.GetComponent<UnitHealth>().TakeDamage(dmg);
                        print("degats : " + dmg);
                        nextAttackTime = Time.time + atkSpeed;
                    }
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
