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
            time += Time.deltaTime;
        }
        if (battle && time > 5.0)
        {
            Ally = GameObject.FindGameObjectsWithTag("Ally");
            
            Ennemy = GameObject.FindGameObjectsWithTag("Ennemy");
            

            if (Ally.Length < 1 )
            {
                looseScreen.SetActive(true);

            } else if(Ennemy.Length <1)
            {
                winScreen.SetActive(true);
            }
        }
    }
}
