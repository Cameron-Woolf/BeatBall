using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball: MonoBehaviour, IBall
{

    private float _speed = 25f;
    private Rigidbody _rigidBody;
    private Vector3 _velocity;
    private BallBehavior _ballBehavior;

    [SerializeField] GameObject ballPrefab;

    // Start is called before the first frame update
    void Start()
    {
        setUpBall();


    }

    // Update is called once per frame
    // Whatever the frame rate is for the Game
    void Update()
    {

    }

    // Called 100hz
    // Use this for physics interactions
    private void FixedUpdate()
    {
        // Normalized just takes whatever velocity the Object has and sets it to 1
        // That way we can always control the velocity of the Object with _speed.
        _rigidBody.velocity = _rigidBody.velocity.normalized * _speed;
        _velocity = _rigidBody.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {

        string collisionObjectName = collision.gameObject.name;
        ballCollision(collision);
    }

    private void setUpBall()
    {
        _ballBehavior = new BallBehavior();
        _rigidBody = GetComponent<Rigidbody>();

        // Controls how fast the Ball "falls" down.
        // And the general speed.
        //_rigidBody.velocity = Vector3.up * _speed;
        Invoke("launchBall", 0.5f);

    }

    private void launchBall()
    {
        _rigidBody.velocity = Vector3.up * _speed;
    }

    public void ballCollision(Collision collision)
    {

        if (collision.gameObject.name == "Floor")
        {
            _ballBehavior.reduceBallCountByOne();
            Destroy(gameObject);
            Debug.Log("Ball hit the floor!");
        }
        else
        {
            // Handles the impact on the Paddle or walls.
            // collisions.contacts[0] just gets the first collision that occurs to the ball
            // The normal is the angle of reflection
            _rigidBody.velocity = Vector3.Reflect(_velocity, collision.contacts[0].normal);
        }
    }

    // Ball Game logic implemented as Humble Object
    public class BallBehavior {

        public void reduceBallCountByOne()
        {
            GameManager.Instance.Balls--;
            
        }

    }

  
}
