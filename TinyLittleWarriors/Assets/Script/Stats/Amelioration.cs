using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amelioration : MonoBehaviour
{

    /// To apply an amelioration on a character, you call Amelioration.apply...(GameObject character)
    /// Then you can add a value val (float, don't forget the 'f'), or default value is 1.5 if nothing is given

    public static GameObject[] allies;

    // MANUALLY APPLY TAG Ally ON EVERY ALLIES !
    public static void GetAllies()
    {
        allies = GameObject.FindGameObjectsWithTag("Ally");
    }

    /// Speedboost

    // Apply a Speedboost of 1.5 to the gameobject go
    public static void applySpeedBoost(GameObject go)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setSpeed(stat.getSpeed() * 2f);
    }
    // Apply a Speedboost of val to the gameobject go
    public static void applySpeedBoost(GameObject go, float val)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setSpeed(stat.getSpeed() * val);
    }
    public static void applySpeedBoostGlobal()
    {
        GetAllies();
        foreach (GameObject ally in allies)
        {
            applySpeedBoost(ally);
        }
    }
    public static void applySpeedBoostGlobal(float val)
    {
        GetAllies();
        foreach (GameObject ally in allies)
        {
            applySpeedBoost(ally, val);
        }
    }




    /// Stun (assomer)

    // Apply a Stun of *0.2 to the gameobject go
    public static void applyStun(GameObject go)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setAttackSpeed(stat.getAttackSpeed() * 0.2f);
    }
    // Apply a Speedboost of val to the gameobject go
    public static void applyStun(GameObject go, float val)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setAttackSpeed(stat.getAttackSpeed() * val);
    }
    public static void applyStunGlobal()
    {
        GetAllies();
        foreach (GameObject ally in allies)
        {
            applyStun(ally);
        }
    }
    public static void applyStunGlobal(float val)
    {
        GetAllies();
        foreach (GameObject ally in allies)
        {
            applyStun(ally, val);
        }
    }


    /// Attackboost

    // Apply a Attackboost of 1.5 to the gameobject go
    public static void applyAttackBoost(GameObject go)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setAttack(stat.getAttack() * 2);
    }
    // Apply a Attackboost of val to the gameobject go
    public static void applyAttackBoost(GameObject go, float val)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setAttack(stat.getAttack() * val);
    }
    public static void applyAttackBoostGlobal()
    {
        GetAllies();
        foreach (GameObject ally in allies)
        {
            applyAttackBoost(ally);
        }
    }
    public static void applyAttackBoostGlobal(float val)
    {
        GetAllies();
        foreach (GameObject ally in allies)
        {
            applyAttackBoost(ally, val);
        }
    }


    /// AttackSpeedboost

    // Apply a AttackAttackSpeedboost of 1.5 to the gameobject go
    public static void applyAttackSpeedBoost(GameObject go)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setAttackSpeed(stat.getAttackSpeed() * 1.5f);
    }
    // Apply a AttackSpeedboost of val to the gameobject go
    public static void applyAttackSpeedBoost(GameObject go, float val)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setAttackSpeed(stat.getAttackSpeed() * val);
    }
    public static void applyAttackSpeedBoostGlobal()
    {
        GetAllies();
        foreach (GameObject ally in allies)
        {
            applyAttackSpeedBoost(ally);
        }
    }
    public static void applyAttackSpeedBoostGlobal(float val)
    {
        GetAllies();
        foreach (GameObject ally in allies)
        {
            applyAttackSpeedBoost(ally, val);
        }
    }


    /// DefenseBoost

    // Apply a DefenseBoost of 2 to the gameobject go
    public static void applyDefenseBoost(GameObject go)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setDefense(stat.getDefense() * 2);
    }
    // Apply a DefenseBoost of val to the gameobject go
    public static void applyDefenseBoost(GameObject go, float val)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setDefense(stat.getDefense() * val);
    }
    public static void applyDefenseBoostGlobal()
    {
        GetAllies();
        foreach (GameObject ally in allies)
        {
            applyDefenseBoost(ally);
        }
    }
    public static void applyDefenseBoostGlobal(float val)
    {
        GetAllies();
        foreach (GameObject ally in allies)
        {
            applyDefenseBoost(ally, val);
        }
    }


    /// RangeBoost

    // Apply a RangeBoost of 2 to the gameobject go
    public static void applyRangeBoost(GameObject go)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setRange(stat.getRange() * 2);
    }
    // Apply a RangeBoost of val to the gameobject go
    public static void applyRangeBoost(GameObject go, float val)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setRange(stat.getRange() * val);
    }
    public static void applyRangeBoostGlobal()
    {
        GetAllies();
        foreach (GameObject ally in allies)
        {
            applyRangeBoost(ally);
        }
    }
    public static void applyRangeBoostGlobal(float val)
    {
        GetAllies();
        foreach (GameObject ally in allies)
        {
            applyRangeBoost(ally, val);
        }
    }

}
