using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boing : MonoBehaviour
{
    public Vector2 initialV;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(initialV);
    }

}
