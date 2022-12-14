using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    [SerializeField] private int hits;
    [SerializeField] private int points;

    // Being set in the GUI
    [SerializeField] private Material hitMaterial;

    private Vector3 rotator;
    private Renderer _renderer;
    private Material _orgMaterial;

    private int _hits;

    // Start is called before the first frame update
    void Start()
    {
        _hits = hits;
        setBrickVariation();
        setBrickMaterial();
    }
    // Update is called once per frame
    void Update()
    {
        brickRotation();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _hits--;
        // Score some points
        if(_hits <= 0)
        {
            breakBrick();
        }

        brickHit();
    }

    private void setBrickVariation()
    {
        rotator = new Vector3(15, 0, 0);
        // Adds variation to the starting rotation for the Bricks
        transform.Rotate(rotator * (transform.position.x + transform.position.y) *0.1f);
    }

    private void brickRotation()
    {

        rotator = new Vector3(45, 0, 0);
        // Time.deltaTime is consitent across frame rates
        transform.Rotate(rotator * Time.deltaTime);

    }

    private void setBrickMaterial()
    {
        // The renderer is responsible for Rendering the Object
        // Diffrent types, in the case of the Brick, it's a Mesh Renderer
        _renderer = GetComponent<Renderer>();

        // The orignal material of the Object can be accessed with sharedMaterial
        _orgMaterial = _renderer.sharedMaterial;

    }

    public void breakBrick()
    {
        GameManager.Instance.Score += points;
        //gameObject is always a predefined variable that accesses that Object
        //the script is attached too.
        Destroy(gameObject);
    }

    private void brickHit()
    {
        _renderer.sharedMaterial = hitMaterial;

        // Unity calls this method of 1 second. An async call
        Invoke("restoreMaterial", 0.10f);
    }

    private void restoreMaterial()
    {
        _renderer.sharedMaterial = _orgMaterial;
    }
}
