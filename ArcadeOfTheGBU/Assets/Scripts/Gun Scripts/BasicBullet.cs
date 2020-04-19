using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rigBody;
    public GameObject hitBulletEffect;
    private BasePlayer enemyBase;
    public int damage = 1;

    [Header("Variables")]
    private float decayTime = 5;
    public float timeUntilSlowdown = 0.5f;
    public float bulletDeceleration = 10;

    private bool startSlowingdown = false;

    private void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        StartCoroutine(bulletDecay());
    }

    private void FixedUpdate()
    {
        decayTime -= Time.deltaTime;

        if (decayTime <= 0)
            Destroy(gameObject);

        if (rigBody.velocity.magnitude <= 0)
            Destroy(gameObject);

        if (startSlowingdown)
            rigBody.velocity = Vector2.MoveTowards(rigBody.velocity, Vector2.zero, bulletDeceleration * Time.deltaTime);

    }

    IEnumerator bulletDecay()
    {
        yield return new WaitForSeconds(timeUntilSlowdown);
        startSlowingdown = true;   
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2") // Add more for more players
        {
            enemyBase = collision.gameObject.GetComponent<BasePlayer>();
            enemyBase.TakeDamage(damage);
        }

        GameObject newDestroyEffect;
        newDestroyEffect = Instantiate(hitBulletEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }



}
