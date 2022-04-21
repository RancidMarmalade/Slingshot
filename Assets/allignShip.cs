using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allignShip : MonoBehaviour
{
    public GameObject Ship;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Ship = GameObject.Find("Spaceship");
    }

    // Update is called once per frame
    void Update()
    {
        Ship.transform.LookAt(new Vector2(transform.position.x + rb.velocity.x,transform.position.y + rb.velocity.y));
       
    }
}
