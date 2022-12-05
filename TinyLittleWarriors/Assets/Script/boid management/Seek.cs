using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : AgentBehavior
{
    //move towards a target
    public override steering GetSteering()
    {
        steering steer = new steering();
        Vector3 relativePos = target.transform.position - transform.position;
        steer.linear = relativePos;
        steer.linear.Normalize();
        steer.linear = steer.linear * agent.maxAccel;
        steer.angular = Vector3.Angle(relativePos, transform.forward);
        return steer;
    }
}
