using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPoints : MonoBehaviour
{

    [SerializeField] private float rotatePoint;
    public bool facingRight;

    public float transformOfX;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rotatePoint = GetComponentInParent<Transform>().rotation.eulerAngles.z;
        FlipAcordantlyToRotation();

    }

    void FlipAcordantlyToRotation()
    {
        if ((rotatePoint > 90 && rotatePoint < 270) && !facingRight)
        {
            facingRight = true;
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y * -1);
        }
        else if((rotatePoint < 90 || rotatePoint > 270) && facingRight)
        {
            facingRight = false;
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y * -1);

        }
    }

}
