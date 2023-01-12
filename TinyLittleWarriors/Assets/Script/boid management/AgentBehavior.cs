using System;
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
        Wander,
        Idle
    }
    [SerializeField] private AgentFSM state;
    [SerializeField] private Transform enemyBase;
    [SerializeField] private double viewDistance = 10.0;
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
                    List<Agent> enemies = new List<Agent>();
                    int enemiesSeen = EnnemiesInView(enemies);
                    if (enemiesSeen == 0) state = AgentFSM.Wander;
                    else
                    {
                        GetNearestEnemy(enemies);
                        seekScript.setTarget(target.transform);
                    }

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
                int atk = stats.getAttack();
                float atkSpeed = 1.0f / stats.getAttackSpeed();
                if (target == null || Vector3.Distance(target.transform.position, transform.position) > range + seekScript.offset)
                {
                    state = AgentFSM.Seek;
                }
                else
                {
                    CharacterStats foeStats = target.GetComponent<CharacterStats>();
                    if(nextAttackTime == -1.0f || Time.time >= nextAttackTime)
                    {
                        double dmg = Math.Floor(atk * (1 - (foeStats.getDefense()/100.0f)));
                        if (dmg < 0) dmg = 0;
                        target.GetComponent<UnitHealth>().TakeDamage((int)dmg);
                        nextAttackTime = Time.time + atkSpeed;
                        print("degats : " + dmg);
                    }
                }
                break;

            case AgentFSM.Wander:
                transform.LookAt(enemyBase);
                transform.position = Vector3.MoveTowards(transform.position, enemyBase.position, GetComponent<CharacterStats>().getSpeed() * Time.deltaTime);
                List<Agent> inSight = new List<Agent>();
                int seen = EnnemiesInView(inSight);
                if (seen != 0) state = AgentFSM.Seek;
                break;
        }
    }

    int EnnemiesInView(List<Agent> enemiesInView)
    {
        Agent[] enemies = FindObjectsOfType<Agent>();
        int nbViewed = 0;
        foreach(Agent e in enemies)
        {
            if (e.getFaction() == Agent.Faction.Enemy)
            {
                if (Vector3.Distance(transform.position, e.transform.position) <= viewDistance)
                {
                    enemiesInView.Add(e);
                    nbViewed += 1;
                }
            }
        }
        return (nbViewed);
    }

    void GetNearestEnemy(List<Agent> enemies)
    {
        Agent bestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Agent potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistance)
            {
                closestDistance = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        target = bestTarget;
    }
    public void setState(AgentFSM etat)
    {
        state = etat;
    }
}
