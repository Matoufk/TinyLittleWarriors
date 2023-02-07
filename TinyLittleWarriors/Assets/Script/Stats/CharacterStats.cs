using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    /// <summary>
    /// This script is to apply on every character in the scene. You can easily give them starting stats on the Unity right panel
    /// </summary>
    /// 

    public float attack;
    public int life;
    public int maxLife;
    public float speed;
    public float defense;
    public float attackSpeed;
    public float range;
    public bool isAlive = true;


    // Start is called before the first frame update
    void Start()
    {
        maxLife = life;
    }


    // Getters and Setters for the Attack
    public float getAttack()
    {
        return attack;
    }
    public void setAttack(float val)
    {
        attack = val;
    }

    // Getters and Setters for the Life
    public int getLife()
    {
        return life;
    }
    public void setLife(int val)
    {
        life = val;
    }

    // Getters for the MaxLife
    public int getMaxLife()
    {
        return maxLife;
    }

    // Getters and Setters for the Speed
    public float getSpeed()
    {
        return speed;
    }
    public void setSpeed(float val)
    {
        speed = val;
    }

    // Getters and Setters for the Defense
    public float getDefense()
    {
        return defense;
    }
    public void setDefense(float val)
    {
        defense = val;
    }

    // Getters and Setters for the attackSpeed
    public float getAttackSpeed()
    {
        return attackSpeed;
    }
    public void setAttackSpeed(float val)
    {
        attackSpeed = val;
    }

    // Getters and Setters for the Range
    public float getRange()
    {
        return range;
    }
    public void setRange(float val)
    {
        range = val;
    }

    // Boolean if alive
    public bool getAlive()
    {
        return isAlive;
    }
    public void setAlive(bool val)
    {
        isAlive = val;
    }
}
