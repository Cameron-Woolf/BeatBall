using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddel : MonoBehaviour
{

    

    private Rigidbody _rigidBody;
    private float paddleAngle = 0;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] int ballNumber;

    // Start is called before the first frame update
    void Start()
    {
       
        _rigidBody = GetComponent<Rigidbody>();
        generateBalls();
    }

    // Update is called once per frame
    void Update()
    {
        setPaddleAngle(); 
    }

    // Called 100hz
    // Place all physics here
    private void FixedUpdate()
    {
        paddelControl();
      

    }

    private void paddelControl()
    {
        // Translating mouse movement (screenspace) to world space with ScreenToWorldPoint
        // Bellow allows for x, and y movement of the Paddle

        Vector3 xMovement = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, 0, -20));

        Vector3 yMovement = Camera.main.ScreenToWorldPoint(
            new Vector3(0, Input.mousePosition.y, -20));

        // Set yMovement to -Y to fix the paddle in place in the game.
        // Must match the initial position of the Paddel
        yMovement = new Vector3(0, -12, 0);

        // Just x movement for the tutorial.
        _rigidBody.MovePosition(new Vector3(xMovement.x, yMovement.y, 0));

        // Rotates to Paddle
        gameObject.transform.eulerAngles = new Vector3(0, 0, paddleAngle);

    }

    // Apprently KeyDown needs to be called from Update
    private void setPaddleAngle()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            paddleAngle += 15;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            paddleAngle -= 15;
        }
    }


    private void generateBalls()
    {
        for (int i = 0; i < ballNumber; i++)
        {
            Instantiate(ballPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }


}
