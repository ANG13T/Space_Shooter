using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject explosion;
    public float speed = 10.0f;
    public bool isClickable = false;
    private Rigidbody2D rb;
    public GameObject cam;
    public string type = "left";
    private Vector2 screenBounds;
  
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        switch (type)
        {
            case "left":
                rb.velocity = new Vector2(-speed, 0);
                break;

            case "right":
                rb.velocity = new Vector2(speed, 0);
                break;

            case "top":
                rb.velocity = new Vector2(0, speed);
                break;

            case "bottom":
                rb.velocity = new Vector2(0, -speed);
                break;
        }
        
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && isClickable)
        {
            Debug.Log("Clicked on Asteroid");
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 300))
            {
                // whatever tag you are looking for on your game object
                if (hit.collider.tag == "Asteroid")
                {
                    GameObject explosive = Instantiate(explosion, transform.position, Quaternion.identity);
                    explosive.GetComponent<ParticleSystem>().Play();
                    Destroy(this.gameObject);
                }
            }
            
        }

        if (rb.position.x < -20)
        {
            Destroy(this.gameObject);
        }

        if (rb.position.x > screenBounds.x + 20)
        {
            Destroy(this.gameObject);
        }

        if (rb.position.y < -20)
        {
            Destroy(this.gameObject);
        }

        if (rb.position.y > screenBounds.y + 20)
        {
            Destroy(this.gameObject);
        }

    }


    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Beam")
        {
            GameObject explosive = Instantiate(explosion, transform.position, Quaternion.identity);
            explosive.GetComponent<ParticleSystem>().Play();
            cam.GetComponent<PlayManager>().addPoints(5);
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }

    }




}
