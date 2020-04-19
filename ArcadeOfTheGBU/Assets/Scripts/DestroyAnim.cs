using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnim : MonoBehaviour
{

    public float delay = 0f;

    private void Start()
    {
        if (GetComponent<Animator>() != null)
            Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
        else
            Destroy(gameObject, delay);
    }

}
