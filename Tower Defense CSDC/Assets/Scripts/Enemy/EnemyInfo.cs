using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyInfo : MonoBehaviour
{
    public EnemyTypes.EnemyType type;
    public float health;
    public int speed;
    public int cost;
    public int damage;
    public GameObject model;
    public EnemyManager.propertyType propertyType;
    public int scale;
    public bool hasGravity;

}
