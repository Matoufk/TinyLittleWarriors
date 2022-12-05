using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] private Faction side;
    public enum Faction
    {
        Ally,
        Enemy
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setFaction(Faction camp)
    {
        side = camp;
    }

    public Faction getFaction()
    {
        return side;
    }
}
