using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Enemy Speed
    public float speed = 3f;

    //Initial Pos
    Vector3 initialPos;

    //Direction
    int direction = 1;

    //range
    public float rangeY = 2;    

    // Start is called before the first frame update
    void Start()
    {

        initialPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        float factor = (direction == -1) ? 2 : 1;

        float movementY = factor * speed * Time.deltaTime * direction;

        //new Pos y
        float newY = transform.position.y + movementY;


        //check Limit
        if (Mathf.Abs(newY - initialPos.y) > rangeY) {
            direction *= -1;
        }
        else transform.position += new Vector3(0, movementY, 0);
    }
}


