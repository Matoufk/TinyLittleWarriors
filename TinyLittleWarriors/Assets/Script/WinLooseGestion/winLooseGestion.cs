using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class winLooseGestion : MonoBehaviour
{
    public PlacementBehavior placement;
    public bool battle = false;

    public GameObject winScreen;
    public GameObject looseScreen;

    float time = 0.0F;


    GameObject[] Ally ;
    GameObject[] Ennemy;
    // Update is called once per frame
    void Update()
    {
        if (placement.battle)
        {
            battle = true;
            Time.timeScale = 1f;
            time += Time.deltaTime;
            Ennemy = GameObject.FindGameObjectsWithTag("Ennemy");
            foreach(GameObject ennemy in Ennemy)
            {
                ennemy.GetComponent<AgentBehavior>().setState(AgentBehavior.AgentFSM.Wander);
            }
        }
        if (battle && time > 5.0)
        {
            Ally = GameObject.FindGameObjectsWithTag("Ally");
            
            Ennemy = GameObject.FindGameObjectsWithTag("Ennemy");
            

            if (Ally.Length < 1 )
            {
                looseScreen.SetActive(true);
                Time.timeScale = 0;

            } else if(Ennemy.Length <1)
            {
                winScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
