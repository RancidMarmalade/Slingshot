using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Vector2 initialVelocity;
    private Vector2 direction;
    public int Steps;
    private Vector2 Startpos;

    private void FixedUpdate()
    {
      Gravity[] attractors = FindObjectsOfType<Gravity>();
        foreach (Gravity attractor in attractors)
        {
            if (attractor != this)
                Attract(attractor);
        }
    }
  
    public void Attract (Gravity objToAttract)
    {
        Vector2 v = initialVelocity;
        for (int i = 0; i < Steps; i++)
        {
            GameObject[] Attractors = GameObject.FindGameObjectsWithTag("attractor");
            foreach (GameObject Attractor in Attractors)
            {
                Vector2 direction = (new Vector2(Attractor.transform.position.x, Attractor.transform.position.y) - Startpos);
            }

            Gizmos.DrawSphere(Startpos, 0.1f);
            Startpos += v.normalized;
        }

    }
}
