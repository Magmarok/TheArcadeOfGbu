using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    [Header("Components")]
    private CircleCollider2D gunColl;
    public GameObject projectile;
    public GameObject muzzleFlash;

    public GameObject Smoke;

    public GameObject targetGunPos; // the position the gun goes to.
    public GameObject ownerOfGun;

    public GameObject ragdollVersion;


    [Header("Variables")]
    public float fireRate = 0.1f;
    private float originalFireRate;
    private float fireRateCooldown;

    public int ammunition = 1;
    private bool outOfAmmo = false;

    public float projectileSpeed = 10;
    public float cooldown;

    public float holdTime;

    public float colliderRadius;

    private bool isTaken = false;

    public bool hasBeenPickedUp = false;

    public string gunNamePos;
    public float travelSpeed;

    public bool TriggerButtonDown = false;

    [Header("Gun properties")]
    public int burstAmmount = 1;

    public bool isAtomatic = false;

    public bool hasSpinnup;
    public float spinnupSpeed = 1;
    public float maxSpinSpeed;


    // Start is called before the first frame update
    void Start()
    {
        gunColl = GetComponent<CircleCollider2D>();
        gunColl.radius = colliderRadius;

        originalFireRate = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        fireRateCooldown -= Time.deltaTime;

        if (ammunition <= 0 && !outOfAmmo)
        {
            reload();
        }

        if (hasBeenPickedUp)
        {
            GoToPickup();
            FlipToRotation();
        }


        FireMethod();


    }

    public virtual void FireMethod()// The way the weapon fires
    {
        if (TriggerButtonDown == true)
        {

            if (!isAtomatic)
                TriggerButtonDown = false;


            if (hasSpinnup)
            {
                if (fireRate > maxSpinSpeed)
                    fireRate -= spinnupSpeed * Time.deltaTime;
            }


            FireWeapon();


        }
        else if (!TriggerButtonDown)
        {
            fireRate = originalFireRate;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, colliderRadius);
    }

    void reload()
    {
        projectile = Smoke;
        outOfAmmo = true;
    }

    public void FireWeapon()
    {
        if (fireRateCooldown <= 0)
        {
            GameObject newProjectile;
            GameObject newFlash;

            newFlash = Instantiate(muzzleFlash, transform.Find("FirePoint").position, transform.rotation);
            newProjectile = Instantiate(projectile, transform.Find("FirePoint").position, transform.rotation);

            if (ammunition > 0)
                Physics2D.IgnoreCollision(newProjectile.GetComponent<Collider2D>(), ownerOfGun.GetComponent<Collider2D>());


            if (!outOfAmmo)
                newProjectile.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
            fireRateCooldown = fireRate;

            ammunition--;

        }

    }

    public void GoToPickup()
    {
        float step = travelSpeed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targetGunPos.transform.Find("Rotate_Point/" + gunNamePos + "").position, step);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetGunPos.transform.Find("Rotate_Point").rotation, step * 100);

    }
    private void FlipToRotation()
    {
        if (targetGunPos.transform.Find("Rotate_Point/" + gunNamePos + "").GetComponent<GunPoints>().facingRight)
        {
            transform.localScale = new Vector2(transform.localScale.x, -transform.localScale.x);
        }
        else
        {
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.x);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2") && targetGunPos == null) // Add more for more players
        {
            targetGunPos = collision.gameObject;
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && targetGunPos != null) // Add more for more players
        {
            ownerOfGun = null;
        }
    }


}
