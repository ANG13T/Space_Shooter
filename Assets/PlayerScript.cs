using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public GameObject shooter;
    public GameObject beam;
    public Transform target;
    public GameObject playerClouds;
    public float bulletMagnitude;
    private Vector2 screenBounds;

    public GameObject explosion;
    public float speed = 5f;

    public float rotateSpeed = 200f;
    private Vector3 mousePos;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.S)) //shoot bullet
        {

            GameObject bulletInstance = Instantiate(beam, shooter.gameObject.transform.position, this.gameObject.transform.rotation);
            bulletInstance.GetComponent<Rigidbody2D>().velocity = transform.forward * bulletMagnitude;
        }

        if (Input.GetMouseButtonDown(0))
        {
            speed = 5;
            playerClouds.GetComponent<ParticleSystem>().Emit(1);
        }
        else
        {
            playerClouds.GetComponent<ParticleSystem>().Stop();
            speed = 3;
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = (Vector2)mousePos - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.up * speed;

        if(rb.position.x < -screenBounds.x)
        {
            rb.transform.position = new Vector3(screenBounds.x, rb.transform.position.y, -8);
        }

        if (rb.position.x > screenBounds.x)
        {
            rb.transform.position = new Vector3(-screenBounds.x, rb.transform.position.y, -8);
        }

        if (rb.position.y < -screenBounds.y)
        {
            rb.transform.position = new Vector3(rb.transform.position.x, screenBounds.y, -8);
        }

        if (rb.position.y > screenBounds.y)
        {
            rb.transform.position = new Vector3(rb.transform.position.x, -screenBounds.y, -8);
        }
    }

    public void gameOver()
    {
        GameObject explosive = Instantiate(explosion, transform.position, Quaternion.identity);
        explosive.GetComponent<ParticleSystem>().Play();
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Asteroid")
        {
            gameOver();
            Destroy(this.gameObject);
        }
       
    }
}
