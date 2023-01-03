using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables:
    //Public or private reference
    //Data type: int, float, bool, string
    //Has a name
    //(Optional) value assigned
    [SerializeField] //[SerializeField] makes the private variable editable inside the editor.
    private float _speed = 7;
    [SerializeField]
    private GameObject _LaserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1.0f;
    [SerializeField]
    private float _lives = 3f;
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {

        //Take the current position = new position (0,0,0)
        //To reach into the "Transform" component we use transform.
        //To reach into "Position" component of the "Transform",
        //we use (transform.position)
        //Position is a Vector3 object,
        //so we should use Vector3 to change it.
        transform.position = new Vector3(0, -4, 0);

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)

        {
            Debug.LogError("The Spawn Manager cannot be reached.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        Shooting();

    }

    void Shooting()
    {
        float x = transform.position.x;
        float y = transform.position.y + 1;
        float z = transform.position.z;
        Vector3 newPos = new Vector3(x, y, z);
        //When space pressed, we instantiate the prefab which we will choose inside the editor.
        if (Input.GetKey("space") && Time.time > _canFire) //Time.time is the time passed from the start of the game.
        {
            //We created _canFire as -1f, since Time.time should be bigger at start.
            //Then we add the _fireRate (0.5) to Time.time, so now _canFire = 0.5,
            //until the Time.time = 0.6, our object cannot fire another laser.
            _canFire = Time.time + _fireRate;
            Instantiate(_LaserPrefab, newPos, transform.rotation);

        }
    }

    public void Damage()
    {
        _lives--;

        if (_lives <= 0)
        {
            GameObject.Destroy(this.gameObject);
            GameObject.Destroy(GameObject.FindWithTag("Enemy"));
            _spawnManager.OnPlayerDeath();

        }
        else if (_lives > 0)
        {
            StartCoroutine(Blink());
        }
    }
    public IEnumerator Blink()
    {
        SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();
        Debug.Log("heyhey");
        for (int i = 0; i < 5; i++)
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(0.2f);
        }
        renderer.enabled = true;


    }
    //We should create methods (functions) to get a cleaner code, not implementing everything in the Update() method.
    void CalculateMovement()
    {
        //transform.Translate(Vector3.right); //Same as: transform.Translate(new Vector3(1, 0 , 0)); 
        //Above code moves per frame, but we need to move per second to move smoothly. So:
        /* if (Input.GetKey(KeyCode.D))
        { transform.Translate(Vector3.right * Time.deltaTime * _speed); }
        else if (Input.GetKey(KeyCode.A))
        { transform.Translate(Vector3.left * Time.deltaTime * _speed); }
        else if (Input.GetKey(KeyCode.W))
        { transform.Translate(Vector3.up * Time.deltaTime * _speed); }
        else if (Input.GetKey(KeyCode.S))
        { transform.Translate(Vector3.down * Time.deltaTime * _speed); }
        else if (Input.GetKey(KeyCode.Space))
        { transform.position = new Vector3(0, 0, 0); } */

        //or we can use the Input access in Unity > Edit > Project Settings
        //With this usage, Unity helps us to minimize the code and use the WASD as well as the arrow keys


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        /*
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        */

        //To be more precise we can do:
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        //If we want to make the object restrained we can use:

        /*
        if(transform.position.x >= 9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
        else if(transform.position.x <= -9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
        if(transform.position.y >= 6)
        {
            transform.position = new Vector3(transform.position.x, 6, 0);
        }
        else if(transform.position.y <= -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
        */

        //We can also use Mathf.Clamp to write this in one line:
        //  transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 6), 0);
        //  transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9, 9), transform.position.y, 0);



        //If we want to make the object wrap to the opposite side when out of bounds:
        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
        if (transform.position.y >= 8)
        {
            transform.position = new Vector3(transform.position.x, -6, 0);
        }
        else if (transform.position.y <= -6)
        {
            transform.position = new Vector3(transform.position.x, 8, 0);
        }
    }



}
