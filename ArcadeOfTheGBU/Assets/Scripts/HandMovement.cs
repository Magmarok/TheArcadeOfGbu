using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{

    [Header("Variables")]
    [SerializeField] float speed;
    [SerializeField] string sideOfHand;
    [SerializeField] string ownerOfHand;

    [TextArea]
    public string HandNote = "SideOfHand should always be written/ Right_Position or Left_Position";


    private bool goToDefult;

    [Header("Components")]
    [SerializeField] private GameObject baseTransform;
    public GameObject targetObject;
    private PlayerAttack playerAttack;
    private SpriteRenderer spritRend; // This is for setting the layer after the gun is facing up


    private void Start()
    {
        baseTransform = GameObject.FindGameObjectWithTag(ownerOfHand).transform.Find(sideOfHand).gameObject;

        spritRend = GetComponent<SpriteRenderer>();

        if (baseTransform == null)
            Debug.LogError("MISSING BASE POSITION");
        //else
        //{
        //    transform.position = baseTransform.position;
        //    transform.rotation = baseTransform.rotation;
        //}        
    }

    private void Update()
    {
        if (targetObject == null)
        {
            goToDefult = true;
        }
        else
        {
            goToDefult = false;
        }

        if (baseTransform == null)
            DestroyOnNull();
    }

    private void FixedUpdate()
    {
        GoToDestenation();
    }


    public void FindTargetPosition()
    {
        playerAttack = GameObject.FindGameObjectWithTag(ownerOfHand).GetComponent<PlayerAttack>();

        targetObject = playerAttack.currentGun;


    }

    private void GoToDestenation()
    {
        float step = speed * Time.deltaTime;
        float fastStep = speed * 2;

        if (goToDefult)
        {
            transform.position = Vector2.MoveTowards(transform.position, baseTransform.transform.position, step);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, baseTransform.transform.rotation, fastStep);
        }
        else if (!goToDefult && targetObject != null )
        {
            if (targetObject.transform.Find(sideOfHand) != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetObject.transform.Find(sideOfHand).position, step);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetObject.transform.Find(sideOfHand).rotation, fastStep);

                if (transform.rotation.eulerAngles.z < 270 && transform.rotation.eulerAngles.z > 90)
                    spritRend.flipY = true;
                else
                    spritRend.flipY = false;
            }
            else
            {
                targetObject = null;
            }
        }
    }

    void DestroyOnNull()
    {
        Destroy(gameObject);
    }


}
