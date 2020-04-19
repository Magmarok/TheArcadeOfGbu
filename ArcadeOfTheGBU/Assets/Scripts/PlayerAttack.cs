using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Components")]
    private BoxCollider2D boxColl;
    [SerializeField] public GameObject currentGun;
    public HandMovement[] handMov;


    [Header("Variables")]
    public float holdTime;
    public float holdCountdown;
    private bool doCountdown;
    public float throwSpeed = 6f;

    [Header("")]
    public string pickupButton;
    public string fireButton;
    public string handOwned;



    private int totalAmmountOfGuns = 0;

    [SerializeField] private bool isCloseToGun = false;
    public bool hasTakenGun = false;

    void Start()
    {
        boxColl = GetComponent<BoxCollider2D>();

        holdCountdown = holdTime;

        GameObject[] hands = GameObject.FindGameObjectsWithTag(handOwned);
        handMov = new HandMovement[hands.Length];

        for (int i = 0; i < hands.Length; i++)
        {
            handMov[i] = hands[i].GetComponent<HandMovement>();
        }

    }

    void Update()
    {
        if (Input.GetButtonDown(fireButton) && hasTakenGun)
            currentGun.GetComponent<BaseGun>().TriggerButtonDown = true;

        else if (Input.GetButtonUp(fireButton))
            currentGun.GetComponent<BaseGun>().TriggerButtonDown = false;


        HoldToTake();


    }


    void HoldToTake()
    {

        // If you hold the button enugh and has no weapon on hand and is close to it. Pick it up
        if (holdCountdown <= 0 && !hasTakenGun && isCloseToGun)
        {
            for (int i = 0; i < handMov.Length; i++)
            {
                handMov[i].FindTargetPosition();
            }

            isCloseToGun = false; // No need for it being "close"
            hasTakenGun = true; // You have taken the gun
            currentGun.GetComponent<BaseGun>().hasBeenPickedUp = true; // You have taken the gun in other script
            currentGun.GetComponent<BaseGun>().ownerOfGun = this.gameObject; // You are the owner of the gun

        }

        if (!hasTakenGun) // If you don't have a weapon. Then pick one up!
        {
            if (Input.GetButtonDown(pickupButton) && isCloseToGun)
                doCountdown = true;

            else if (Input.GetButtonUp(pickupButton) || !isCloseToGun) // Hey! gotta hold that shit (or be close ;/)
                doCountdown = false;
        }
        else //If you have a weapon and want to pick up another one. Just first throw away your current one ;)
        {
            ThrowGunAway();
        }


        if (doCountdown)
        {
            holdCountdown -= Time.deltaTime;
        }
        else
        {
            holdCountdown = holdTime;
        }


    }

    void ThrowGunAway()
    {
        if(Input.GetButtonDown(pickupButton))
        {
            // Creates the Object. Sets it's position. Adds force in looking direction. Adds rotation. Makes so it ignores the inisial throwers collider.
            GameObject gunRagdoll;
            gunRagdoll = Instantiate(currentGun.GetComponent<BaseGun>().ragdollVersion, currentGun.transform.position, currentGun.transform.rotation);
            gunRagdoll.transform.localScale = currentGun.transform.localScale;
            gunRagdoll.GetComponent<Rigidbody2D>().velocity = currentGun.transform.right * throwSpeed;
            gunRagdoll.GetComponent<Rigidbody2D>().AddTorque(throwSpeed);
            Physics2D.IgnoreCollision(gunRagdoll.GetComponent<Collider2D>(), GetComponent<Collider2D>());

            for (int a = 0; a < handMov.Length; a++)
            {
                handMov[a].targetObject = null;
            }
            hasTakenGun = false;

            Destroy(currentGun);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gun" && !hasTakenGun)
        {
            totalAmmountOfGuns++;

            currentGun = collision.gameObject;
            holdTime = currentGun.GetComponent<BaseGun>().holdTime;
            isCloseToGun = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gun" && !hasTakenGun)
        {
            totalAmmountOfGuns--;
            if (totalAmmountOfGuns <= 0)
            {
                currentGun = null;
                isCloseToGun = false;
                totalAmmountOfGuns = 0;
            }
        }
    }
}
