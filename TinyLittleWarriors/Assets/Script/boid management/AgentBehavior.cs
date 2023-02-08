using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehavior : MonoBehaviour
{
    public Agent target;
    public Animator animator;

    public bool moving = false;
    public bool attacking = false;

    bool target_is_dead = false;

    public enum AgentFSM
    {
        Attack,
        Seek,
        Wander,
        Idle,
    }
    [SerializeField] private AgentFSM state;
    [SerializeField] private Transform enemyBase;
    [SerializeField] private double viewDistance = 10.0;
    private Seek seekScript;
    private float nextAttackTime;

    public float test;

    // Start is called before the first frame update
    void Start()
    {
        seekScript = GetComponent<Seek>();
        //state = AgentFSM.Seek;
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
                    if (enemiesSeen == 0)
                    {
                        state = AgentFSM.Wander;
                        moving = true;
                        animator.SetBool("moving", moving);
                    }

                    else
                    {
                        GetNearestEnemy(enemies);
                        seekScript.setTarget(target.transform);
                    }

                }
                else
                {
                    Transform seekDest = checkForObstacle(target.transform);
                    seekScript.setTarget(seekDest.transform);
                    seekScript.seeking();
                }


                break;

            case AgentFSM.Attack:
                moving = false;
                attacking = true;
                animator.SetBool("attacking", attacking);
                animator.SetBool("moving", false);
                CharacterStats stats = GetComponent<CharacterStats>();
                if (target != null) { 
                    CharacterStats targetStats = target.GetComponent<CharacterStats>();
                    if(targetStats.getLife() < 0)
                    {
                        target_is_dead = true;
                        target.GetComponent<AgentBehavior>().enabled = false;
                        Destroy(target.GetComponent<Rigidbody>());
                    }
                }
                int range = (int)stats.getRange();
                int atk = (int)stats.getAttack();
                float atkSpeed = 1.0f / stats.getAttackSpeed();
           
                if (target == null || target_is_dead || (Vector3.Distance(target.transform.position, transform.position) > range + seekScript.offset))
                {
                    state = AgentFSM.Seek;
                    moving = true;
                    animator.SetBool("moving", moving);
                    animator.SetBool("attacking", false);

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
                Transform wanderDest;
                wanderDest = checkForObstacle(enemyBase);
                transform.LookAt(wanderDest);
                transform.position = Vector3.MoveTowards(transform.position, wanderDest.position, GetComponent<CharacterStats>().getSpeed() * Time.deltaTime);
                List<Agent> inSight = new List<Agent>();
                int seen = EnnemiesInView(inSight);
                if (seen != 0) state = AgentFSM.Seek;

                moving = true;
                animator.SetBool("moving", moving);
                animator.SetBool("attacking", false);

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

    Transform checkForObstacle(Transform pose)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);
        Transform dest = pose;
        test = hitData.distance;
        if(hitData.distance <= 5.0)
        {
            dest.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
            transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
        }
        return(dest);
    }
}
