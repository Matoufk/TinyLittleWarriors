using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    //public Unit unite;
    public GameObject unit;

    public HealthBar healthBar;

    public CharacterStats stats;

    public Animator animator;

    public bool dead = false;

    // Update is called once per frame
    void Update()
    {

        //For testing purpose
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (stats.getLife() > 0) TakeDamage(20);
        }


        //Determine if the unit is dead or not and delete it if it is
        if (stats.getLife() <= 0 && stats.getAlive())
        {
            stats.setAlive(false);

            Debug.Log("The unit : " + this.tag + " is dead");
            dead = true;
            if (animator != null)
            {
                animator.SetBool("dead", dead);
                GetComponent<AgentBehavior>().enabled = false;
            }
            
            Invoke("death", 1.0f);

        }


    }

    //Just the method to make the current unit taking an certain amout of damage 
    public void TakeDamage(int damage)
    {
        int life = stats.getLife();
        stats.setLife(life -= damage);
        healthBar.SetHealth(stats.getLife());
    }

    public void death()
    {
        Destroy(unit.gameObject);
    }





}
