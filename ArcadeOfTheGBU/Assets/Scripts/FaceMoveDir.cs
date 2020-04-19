using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMoveDir : MonoBehaviour
{

    private Transform rotationPoint;
    private float rotationOfPoint;
    private SpriteRenderer spritRend;

    private float currentDegrees;
    private int selectedSpite;

    enum MovingState { Down, Left, Up, Right }
    [Header("States")]
    [SerializeField] MovingState currentMovingState;

    //public Sprite faceDown;
    //public Sprite faceLeft;
    //public Sprite faceUp;
    //public Sprite faceRight;

    public Sprite[] FaceDir;




    private void Start()
    {
        rotationPoint = GetComponent<Transform>().transform.Find("Rotate_Point");
        spritRend = GetComponent<SpriteRenderer>();
        selectedSpite = 0;


    }

    private void FixedUpdate()
    {
        if (rotationPoint != null)
        {
            rotationOfPoint = rotationPoint.eulerAngles.z;
        }



        rotateSprite();

    }

    void rotateSprite()
    {
        currentDegrees = 22.5f;
        selectedSpite = 0;


        if (FaceDir.Length > 0)
        {

            if (rotationOfPoint > 22.5f && rotationOfPoint < 337.5f)
            {

                for (int i = 1; i < FaceDir.Length; i++) // Check if the current degree is more than 22.5 and less than 22.5 + 45 if no keep adding +45 to 22.5 and repeat process
                {
                    selectedSpite++;

                    if (rotationOfPoint > currentDegrees && rotationOfPoint < currentDegrees + 45)
                    {
                        spritRend.sprite = FaceDir[selectedSpite];
                        break;
                    }
                    currentDegrees += 45;
                }
            }
            else
            {
                spritRend.sprite = FaceDir[0];
            }
        }



        //if (rotationOfPoint > 225 && rotationOfPoint < 315)
        //{
        //    spritRend.sprite = faceDown;
        //    currentMovingState = MovingState.Down;
        //}
        //else if (rotationOfPoint > 135 && rotationOfPoint < 225)
        //{
        //    spritRend.sprite = faceLeft;
        //    currentMovingState = MovingState.Left;
        //}
        //else if (rotationOfPoint > 45 && rotationOfPoint < 135)
        //{
        //    spritRend.sprite = faceUp;
        //    currentMovingState = MovingState.Up;

        //}
        //else
        //{
        //    spritRend.sprite = faceRight;
        //    currentMovingState = MovingState.Right;

        //    Debug.Log(rotationOfPoint);

        //}

    }

}
