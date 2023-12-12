using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //Walking Speed
    public float walkSpeed = 2;

    //Jumping Speed
    public float jumpForce = 2;

    //Coin playing sound
    public AudioSource coindSound;

    //RigidBody
    Rigidbody rb;

    //Collider
    Collider col;

    //Flag for Key Pressing
    private bool pressedJump = false;

    //Player Size
    Vector3 size;

    //camera distance Z
    public float cameraDistZ = 6;

    //Set GameManager
    GameManager gameManager;

    //Mininum Altitude
    private float minY = -2;

    // Start is called before the first frame update
    void Start()
    {
        //Set GameManager
        gameManager = GameManager.instance;

        //Grab Component
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        size = col.bounds.size;

        //Set Camera Position
        CameraFollowPlayer();
    }

    // Update is called once per frame 
    void FixedUpdate()
    {
        WalkHandler();
        JumpHandler();
        CameraFollowPlayer();
        CheckAbyss();
    }


    //Player Walking Movement
    void WalkHandler()
    {
        //Input on X
        float hAxis = Input.GetAxis("Horizontal");

        //Input on Z
        float vAxis = Input.GetAxis("Vertical");

        //Movement Vector
        Vector3 movement = new Vector3(hAxis * walkSpeed * Time.deltaTime, 0, vAxis * walkSpeed * Time.deltaTime);

        //Calculate new position
        Vector3 newPos = transform.position + movement;

        //Move
        rb.MovePosition(newPos);

        //Check that we are moving
        if(hAxis != 0 || vAxis != 0)
        {
            //Movement direction
            Vector3 direction = new Vector3(-hAxis, 0, -vAxis);

            //option 1: modify the transform
            //transform.forward = direction;
            
            //option 2: using our rigid body
            rb.rotation = Quaternion.LookRotation(direction);

        }
    }

    void CameraFollowPlayer()
    {
        //Get Camera position   
        Vector3 cameraPos = Camera.main.transform.position;

        //Change Camera Z position
        cameraPos.z = transform.position.z - cameraDistZ;

        //Update actual Camera position
        Camera.main.transform.position = cameraPos;
    }


    //Player Jumping Movement
    void JumpHandler()
    {
        //Input on Y
        float jumpAxis = Input.GetAxis("Jump");

        if (jumpAxis > 0)
        {
            bool isGrounded = CheckGrounded();
            if (!pressedJump && isGrounded)
            {
                pressedJump = true;
                Vector3 jumpMovement = new Vector3(0, jumpAxis * jumpForce, 0);
                rb.AddForce(jumpMovement, ForceMode.VelocityChange);
            }
        }
        else pressedJump = false;

    }

    void CheckAbyss()
    {
        if (transform.position.y <= minY) gameManager.GameOver();
    }

    bool CheckGrounded()
    {
        //location of all 4 corners
        Vector3 corner1 = transform.position + new Vector3(size.x / 2, -size.y / 2 + 0.01f, size.z / 2);
        Vector3 corner2 = transform.position + new Vector3(-size.x / 2, -size.y / 2 + 0.01f, size.z / 2);
        Vector3 corner3 = transform.position + new Vector3(size.x / 2, -size.y / 2 + 0.01f, -size.z / 2);
        Vector3 corner4 = transform.position + new Vector3(-size.x / 2, -size.y / 2 + 0.01f, -size.z / 2);

        //check if grounded
        bool grounded1 = Physics.Raycast(corner1, Vector3.down, 0.02f);
        bool grounded2 = Physics.Raycast(corner2, Vector3.down, 0.02f);
        bool grounded3 = Physics.Raycast(corner3, Vector3.down, 0.02f);
        bool grounded4 = Physics.Raycast(corner4, Vector3.down, 0.02f);

        return (grounded1 || grounded2 || grounded3 || grounded4) ;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            //Play Sound
            coindSound.Play();

            //Destroy Coin
            Destroy(other.gameObject);

            //Inscrease score
            gameManager.IncreaseScore(1);

        } else if (other.CompareTag("Enemy"))
        {
            //Restart Game
            gameManager.GameOver();
            
        }else if (other.CompareTag("Goal"))
        {
            if (gameManager.levelScore >= gameManager.coinTotalAmount)
            {
                //Load Next Level
                gameManager.NextLevel();
            }
            else
                print("PICK UP THE COINS");
            
        }else if (other.CompareTag("BottomGround"))
        {
            gameManager.GameOver();
        }
    }
}
