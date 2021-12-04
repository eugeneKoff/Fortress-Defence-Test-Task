using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Model", menuName = "Model")]
public class Model: ScriptableObject
{
    public int maxHealth;
    

    public int speed;

    public int damage;

    public float attackRate;

    public float detectionDistance;

}
