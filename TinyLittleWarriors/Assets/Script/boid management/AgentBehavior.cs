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
    private Rigidbody m_Rigidbody;
    private CharacterStats stats;

    public float test;

    // Start is called before the first frame update
    void Start()
    {
        seekScript = GetComponent<Seek>();
        //state = AgentFSM.Seek;
        target = null;
        nextAttackTime = -1.0f;
        m_Rigidbody = GetComponent<Rigidbody>();
        stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        stats = GetComponent<CharacterStats>();
        if (Time.time % 3 == 0)
        {
            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.angularVelocity = Vector3.zero;
        }
        switch (state)
        {
            case AgentFSM.Seek:
                moving = true;
                animator.SetBool("moving", moving);
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
                   
                    seekScript.setTarget(target.transform);
                    checkForObstacle();
                    seekScript.seeking();
                }


                break;

            case AgentFSM.Attack:
                moving = false;
                attacking = true;
                animator.SetBool("attacking", attacking);
                animator.SetBool("moving", false);

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
           
                if (target_is_dead || (Vector3.Distance(target.transform.position, transform.position) > range + seekScript.offset))
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
                        if (dmg <= 0) dmg = 1;
                        target.GetComponent<UnitHealth>().TakeDamage((int)dmg);
                        nextAttackTime = Time.time + atkSpeed;
                        //print("degats : " + dmg);
                    }
                }

                break;

            case AgentFSM.Wander:
                transform.LookAt(enemyBase);
                checkForObstacle();
                transform.position = Vector3.MoveTowards(transform.position, enemyBase.position, GetComponent<CharacterStats>().getSpeed() * Time.deltaTime);
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
        target_is_dead = false;
        Agent[] enemies = FindObjectsOfType<Agent>();
        target_is_dead = false;
        int nbViewed = 0;
        foreach(Agent e in enemies)
        {
            if (gameObject.GetComponent<Agent>().getFaction() == Agent.Faction.Ally)
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
            else
            {
                if (e.getFaction() == Agent.Faction.Ally)
                {
                    if (Vector3.Distance(transform.position, e.transform.position) <= viewDistance)
                    {
                        enemiesInView.Add(e);
                        nbViewed += 1;
                    }
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

    void checkForObstacle()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);
        if (hitData.collider != null)
        {
            Vector3 hitPose = hitData.collider.ClosestPoint(transform.position);
            float dir = Vector3.Dot(hitPose, transform.right);
            test = dir;
            if(hitData.distance <= 3.0)
            {
                if (dir >= 0)
                {
                    m_Rigidbody.AddForce(transform.right * 1.2f);
                }
                else
                {
                    m_Rigidbody.AddForce(-transform.right * 1.2f);
                }
            }
        }
    }
}
