using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] List<Color> colors;

    RaycastHit wall;
    Rigidbody playerRb;

    public GameObject player;
    Color color;
    Color playerColor;

    Vector2 firstPosition;
    Vector2 lastPosition;
    Vector2 moveDirection;



    bool isMovementForward;
    bool isVerticalMovementChecked;
    bool isHorizontalMovementChecked;
    bool doubleClicked;
    bool isMouseClicked;


    public int countPaintedFloor;

    public Button[] button;

    // Start is called before the first frame update
    void Start()
    {
        
        playerRb = GetComponent<Rigidbody>();
        GameManager.Instance.player = GetComponent<PlayerMovement>();

        SetamountOfFloor();

        //Setting a random color for player...
        color = colors[UnityEngine.Random.Range(0,colors.Count)];
        player.GetComponent<Renderer>().material.color = color;
        playerColor = color;
    }

    // Update is called once per frame
    void Update()
    {

        //Control the player with mouse inputs
        //MouseController();
        Movement();


    }


    public void SetamountOfFloor()
    {
        GameManager.Instance.amountOfFloor = GameObject.FindGameObjectsWithTag("Floor").Length;

    }



    //Paint the Floor And Keep Track Of Painted Floors

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.GetComponent<MeshRenderer>().material.color != playerColor && other.gameObject.CompareTag("Floor"))
    //    {

    //        other.gameObject.GetComponent<MeshRenderer>().material.color = playerColor;
    //        countPaintedFloor++;


    //    }

    //}
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<MeshRenderer>().material.color != playerColor && other.gameObject.CompareTag("Floor"))
        {

            other.gameObject.GetComponent<MeshRenderer>().material.color = playerColor;
            countPaintedFloor++;


        }
    }





    //void MouseController()
    //{
    //    if (GameManager.Instance.isButtonClicked == true && GameManager.Instance.isLevelCompleted == false)
    //    {

    //        if (Input.GetMouseButtonDown(0) && doubleClicked == false)
    //        {
    //            firstPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //            isMouseClicked = true;
    //            doubleClicked = true;

    //        }
    //        else if (Input.GetMouseButtonUp(0))
    //        {
    //            isMouseClicked = false;
    //        }

    //        if (isMouseClicked == true)
    //        {
    //            lastPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);


    //            moveDirection = new Vector2(lastPosition.x - firstPosition.x, lastPosition.y - firstPosition.y).normalized;

    //            isMovementForward = true;



    //        }




    //        if (isVerticalMovementChecked == false)
    //        {

    //            if (moveDirection.x > 0 && moveDirection.y > -0.5f && moveDirection.y < 0.5f)
    //            {
    //                if (Physics.Raycast(transform.position, transform.right, out wall, 0.5f))
    //                {
    //                    if (wall.collider.gameObject.tag == "Wall")
    //                    {
    //                        isMovementForward = false;
    //                        isHorizontalMovementChecked = false;
    //                        doubleClicked = false;

    //                    }


    //                }
    //                if (isMovementForward == true)
    //                {
    //                    transform.Translate(Vector3.right * speed * Time.deltaTime);
    //                    isHorizontalMovementChecked = true;
    //                }
    //            }
    //        }

    //        if (isVerticalMovementChecked == false)
    //        {

    //            if (moveDirection.x < 0 && moveDirection.y > -0.5f && moveDirection.y < 0.5f)
    //            {
    //                if (Physics.Raycast(transform.position, -transform.right, out wall, 0.5f))
    //                {
    //                    if (wall.collider.gameObject.tag == "Wall")
    //                    {
    //                        isMovementForward = false;
    //                        isHorizontalMovementChecked = false;
    //                        doubleClicked = false;
    //                    }


    //                }
    //                if (isMovementForward == true)
    //                {
    //                    isHorizontalMovementChecked = true;
    //                    transform.Translate(Vector3.left * speed * Time.deltaTime);
    //                }
    //            }
    //        }

    //        if (isHorizontalMovementChecked == false)
    //        {

    //            if (moveDirection.y > 0 && moveDirection.x > -0.5f && moveDirection.x < 0.5f)
    //            {
    //                if (Physics.Raycast(transform.position, transform.forward, out wall, 0.5f))
    //                {
    //                    if (wall.collider.gameObject.tag == "Wall")
    //                    {
    //                        isMovementForward = false;
    //                        isVerticalMovementChecked = false;
    //                        doubleClicked = false;
    //                        Debug.Log("Checked");
    //                    }


    //                }
    //                if (isMovementForward == true)
    //                {
    //                    isVerticalMovementChecked = true;
    //                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
    //                }
    //            }
    //        }
    //        if (isHorizontalMovementChecked == false)
    //        {

    //            if (moveDirection.y < 0 && moveDirection.x > -0.5f && moveDirection.x < 0.5f)
    //            {
    //                if (Physics.Raycast(transform.position, -transform.forward, out wall, 0.5f))
    //                {
    //                    if (wall.collider.gameObject.tag == "Wall")
    //                    {
    //                        isMovementForward = false;
    //                        isVerticalMovementChecked = false;
    //                        doubleClicked = false;
    //                    }


    //                }
    //                if (isMovementForward == true)
    //                {
    //                    isVerticalMovementChecked = true;
    //                    transform.Translate(Vector3.back * speed * Time.deltaTime);
    //                }
    //            }

    //        }
    //    }

    //}
    Vector3 currentDirection;
    private void Movement()
    {
        if (GameManager.Instance.isButtonClicked == true && GameManager.Instance.isLevelCompleted == false)
        {

            if (Input.GetMouseButtonDown(0))
            {
                firstPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                isMouseClicked = true;
            }
            else if (Input.GetMouseButton(0))
            {
                lastPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                moveDirection = new Vector2(lastPosition.x - firstPosition.x, lastPosition.y - firstPosition.y).normalized;

                if (!isMovementForward && (MathF.Abs(moveDirection.y) > 0.3f || Mathf.Abs(moveDirection.x) > 0.3f))
                {
                    Debug.Log(MathF.Abs(moveDirection.y));
                    isMovementForward = true;

                    if (isVerticalMovementChecked == false)
                    {
                        if (moveDirection.x > 0 && moveDirection.y > -0.5f && moveDirection.y < 0.5f)
                        {
                            currentDirection = transform.right;
                        }
                        else if (moveDirection.x < 0 && moveDirection.y > -0.5f && moveDirection.y < 0.5f)
                        {
                            currentDirection = -transform.right;
                        }
                    }
                    if (isHorizontalMovementChecked == false)
                    {
                        if (moveDirection.y > 0 && moveDirection.x > -0.5f && moveDirection.x < 0.5f)
                        {
                            currentDirection = transform.forward;
                        }
                        else if (moveDirection.y < 0 && moveDirection.x > -0.5f && moveDirection.x < 0.5f)
                        {
                            currentDirection = -transform.forward;
                        }
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isMouseClicked = false;
            }


            if (isMovementForward == true)
            {
                playerRb.velocity = currentDirection * speed;

                if (currentDirection.x != 0)
                {
                    isHorizontalMovementChecked = true;
                }
                else if (currentDirection.y != 0)
                {
                    isVerticalMovementChecked = true;
                }

                if (Physics.Raycast(transform.position, currentDirection, out wall, 0.45f))
                {
                    if (wall.collider.gameObject.tag == "Wall")
                    {
                        
                        isMovementForward = false;
                        isVerticalMovementChecked = false;
                        isHorizontalMovementChecked = false;

                    }



                }

            }

        }
    }

}


















