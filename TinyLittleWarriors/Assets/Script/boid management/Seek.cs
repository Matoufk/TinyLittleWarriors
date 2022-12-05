using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seek : MonoBehaviour
{
    Vector3 targetPos;
    
    public void setTarget(Transform target)
    {
        targetPos = target.position;
    }

    void seeking()
    {

    }
}
