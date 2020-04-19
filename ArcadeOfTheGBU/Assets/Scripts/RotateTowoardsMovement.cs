using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowoardsMovement : MonoBehaviour
{

    [Header("Components")]
    private PlayerMovement playerMov;
    private Vector2 originPosition;
    public Vector2 direction;
    public Transform crosshair;

    [Header("Variable")]
    public float rotationSpeed;

    public float maxDistance = 1.8f;
    public float sphareRadius = 0.15f;
    private float currentHitDistance;

    public LayerMask layerMask;
    private bool isHittingSomething = false;

    private void Start()
    {
        playerMov = GetComponentInParent<PlayerMovement>();
    }


    private void FixedUpdate()
    {
        Rotate();
        CrosshairLocation();
    }

    void Rotate()
    {

        Vector2 playerInputAmmount = playerMov.movement;

        if(playerInputAmmount.magnitude > 0)
        {
            playerInputAmmount.Normalize();

            float angle = Mathf.Atan2(playerInputAmmount.y, playerInputAmmount.x) * Mathf.Rad2Deg;
            Quaternion angleInDir = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, angleInDir, rotationSpeed * Time.deltaTime);

        }

    }

    void CrosshairLocation()
    {

        originPosition = transform.position;
        direction = transform.TransformDirection(Vector2.right);
        RaycastHit2D hit = Physics2D.CircleCast(originPosition, sphareRadius, direction, maxDistance ,layerMask);



        if(hit.collider != null) // If the sphare hits something.
        {
            // Can add to see what it hits bu using "currentHitObject = hit.transform.gameObject;"
            currentHitDistance = hit.distance;

            Debug.DrawRay(transform.position, direction * hit.distance, Color.green);

            isHittingSomething = true;


        }
        else if(hit.collider == null)// If it dosn't hit anything
        {
            // nullify the hit object if needed
            currentHitDistance = maxDistance;

            Debug.DrawRay(transform.position, direction * maxDistance, Color.red);

            isHittingSomething = false;

        }

        crosshair.position = originPosition + direction * currentHitDistance;
    } // This is mostly for the effect of the crosshair bumping something.

    private void OnDrawGizmos()
    {
        if (isHittingSomething)
            Gizmos.color = Color.green;

        else if (!isHittingSomething)
            Gizmos.color = Color.red;


        Gizmos.DrawWireSphere(originPosition + direction * currentHitDistance, sphareRadius);
    }// ^^ This as well. ^^

}
