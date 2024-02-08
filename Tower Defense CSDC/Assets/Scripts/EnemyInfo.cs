using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyInfo : MonoBehaviour
{
    [SerializeField] public virtual int health { get; set; } = 100;
    [SerializeField] public virtual int speed { get; set; } = 10;
    [SerializeField] public virtual int cost { get; set; } = 1;

}
