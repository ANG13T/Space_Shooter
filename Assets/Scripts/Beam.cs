using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private Vector2 screenBounds;
    private Rigidbody2D rb;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.x < -screenBounds.x)
        {
            Destroy(this);
        }

        if (transform.position.x > screenBounds.x)
        {
            Destroy(this);
        }

        if (transform.position.y < -screenBounds.y)
        {
            Destroy(this);
        }

        if (transform.position.y > screenBounds.y)
        {
            Destroy(this);
        }
    }

}
