using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timeUntilDestruction;
    
    void Update()
    {
        timeUntilDestruction -= Time.deltaTime;

        if (timeUntilDestruction <= 0)
            Destroy(gameObject);
    }
}
