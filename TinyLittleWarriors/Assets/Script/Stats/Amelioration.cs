using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amelioration : MonoBehaviour
{

    /// To apply an amelioration on a character, you call Amelioration.apply...(GameObject character)
    /// Then you can add a value val (float, don't forget the 'f'), or default value is 1.5 if nothing is given


    /// Speedboost

    // Apply a Speedboost of 1.5 to the gameobject go
    void applySpeedBoost(GameObject go)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setSpeed(stat.getSpeed() * 1.5f);
    }
    // Apply a Speedboost of val to the gameobject go
    void applySpeedBoost(GameObject go, float val)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setSpeed(stat.getSpeed() * val);
    }


    /// Stun (assomer)

    // Apply a Stun of *0.2 to the gameobject go
    void applyStun(GameObject go)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setAttackSpeed(stat.getAttackSpeed() * 0.2f);
    }
    // Apply a Speedboost of val to the gameobject go
    void applyStun(GameObject go, float val)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setAttackSpeed(stat.getAttackSpeed() * val);
    }


    /// Attackboost

    // Apply a Attackboost of 1.5 to the gameobject go
    void applyAttackBoost(GameObject go)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setSpeed(stat.getAttack() * 1.5f);
    }
    // Apply a Attackboost of val to the gameobject go
    void applyAttackBoost(GameObject go, int val)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setAttack(stat.getAttack() * val);
    }


    /// AttackSpeedboost

    // Apply a AttackSpeedboost of 1.5 to the gameobject go
    void applyAttackSpeedBoost(GameObject go)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setAttackSpeed(stat.getAttackSpeed() * 1.5f);
    }
    // Apply a AttackSpeedboost of val to the gameobject go
    void applyAttackSpeedBoost(GameObject go, float val)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setAttackSpeed(stat.getAttackSpeed() * val);
    }


    /// DefenseBoost

    // Apply a DefenseBoost of 2 to the gameobject go
    void applyDefenseBoost(GameObject go)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setDefense(stat.getDefense() * 2);
    }
    // Apply a DefenseBoost of val to the gameobject go
    void applyDefenseBoost(GameObject go, int val)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setDefense(stat.getDefense() * val);
    }


    /// RangeBoost

    // Apply a RangeBoost of 2 to the gameobject go
    void applyRangeBoost(GameObject go)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setRange(stat.getRange() * 2);
    }
    // Apply a RangeBoost of val to the gameobject go
    void applyRangeBoost(GameObject go, int val)
    {
        CharacterStats stat = go.GetComponent<CharacterStats>();
        stat.setRange(stat.getRange() * val);
    }

}
