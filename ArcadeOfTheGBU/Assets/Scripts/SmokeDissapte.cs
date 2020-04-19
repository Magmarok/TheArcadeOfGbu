using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeDissapte : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rigBody;

    [Header("Variables")]
    public int[] rotations;
    public float maxRotationSpeed;

    private float yOffset;
    private float xOffset;
    private float timeToDissapate;

    
    
    private void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        xOffset = Random.Range(3f, 7f);
        yOffset = Random.Range(-7f, 7f);


        timeToDissapate = Random.Range(2, 1f);

        rigBody.AddForce(new Vector2(0, yOffset));
        rigBody.AddForce(transform.right * xOffset);


        transform.rotation = Quaternion.Euler(0, 0,rotations [Random.Range(0, rotations.Length)]);
        rigBody.AddTorque(Random.Range(-maxRotationSpeed, maxRotationSpeed));


        StartCoroutine(WaitToDissapear());
    }

    IEnumerator WaitToDissapear()
    {
        yield return new WaitForSeconds(timeToDissapate);

        Destroy(this.gameObject);
    }

}
