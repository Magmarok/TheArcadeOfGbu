using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public int health = 2;


    public void TakeDamage(int damageTaken)
    {

        health -= damageTaken;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
}
