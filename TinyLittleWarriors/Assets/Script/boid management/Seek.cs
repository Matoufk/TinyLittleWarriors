using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seek : MonoBehaviour
{
    private Vector3 targetPos;
    private int range;
    private float speed;
    private float maxSpeed; //vitesse du groupe
    private CharacterStats stats;
    [SerializeField] private int offset;

    void Start()
    {
        stats = GetComponent<CharacterStats>();
        range = (int)stats.getRange();
        speed = stats.getSpeed();
        offset = 7;
    }

    void Update()
    {
        range = (int)stats.getRange();
        speed = stats.getSpeed();
    }

    public void setTarget(Transform target)
    {
        targetPos = target.position;
    }

    public void seeking()
    {
        transform.LookAt(targetPos);
        Vector3 realTarget = targetPos;
        if(Vector3.Distance(transform.position, targetPos)<= range + offset)
        {
            realTarget = transform.position;
            GetComponent<AgentBehavior>().setState(AgentBehavior.AgentFSM.Attack);
        }
        transform.position = Vector3.MoveTowards(transform.position, realTarget, speed * Time.deltaTime);
    }
}
