using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictMotion : MonoBehaviour
{
    
    public Vector2 InitialVelocity;
    private Vector2 CurrentPos;
    public int steps;
    private Vector2 V;
    private float StepLength;
    public float G;

    public float fps;

    [SerializeField]
    private float D;
    [SerializeField]
    private bool moveEnabled;
    private void start()
    {
        moveEnabled = false;
    }
    
    private void Addvelocity()
    {
        moveEnabled = true;
        GetComponent<Rigidbody2D>().AddForce(InitialVelocity);
    }

   
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && moveEnabled == false)
        {
            Addvelocity();
        }
        if (moveEnabled == true) 
        {
            GameObject[] attractors = GameObject.FindGameObjectsWithTag("Attract");
            foreach (GameObject Attractor in attractors)
            {
                if (Attractor != this)
                {
                    Vector2 direction = (new Vector2(Attractor.transform.position.x, Attractor.transform.position.y) - new Vector2(transform.position.x, transform.position.y));
                    

                    float dist = direction.magnitude;
                    float ForceMagnitude = (Attractor.GetComponent<Rigidbody2D>().mass - GetComponent<Rigidbody2D>().mass) / Mathf.Pow(dist, 2) * G;
                    GetComponent<Rigidbody2D>().AddForce(ForceMagnitude * direction.normalized);
                    
                }
            }
        }
        
    }
   
    private void OnDrawGizmos()
    {if(moveEnabled == false) 
        {
            D = 100000f;

            GameObject[] attractors = GameObject.FindGameObjectsWithTag("Attract");
            CurrentPos = transform.position;
            V = InitialVelocity;
            for (int i = 0; i < steps; i++)
            {


                foreach (GameObject Attractor in attractors)
                {
                    if (Attractor != this)
                    {
                        Vector2 direction = (new Vector2(Attractor.transform.position.x, Attractor.transform.position.y) - CurrentPos);
                        float dist = direction.magnitude;

                        float ForceMagnitude = (Attractor.GetComponent<Rigidbody2D>().mass - GetComponent<Rigidbody2D>().mass) / Mathf.Pow(dist, 2) * G;
                        V += ForceMagnitude * direction.normalized;

                        if (D >= dist)
                        {
                            D = dist;

                        }
                    }
                }
                StepLength = (V.magnitude * (Time.fixedDeltaTime / fps)) / GetComponent<Rigidbody2D>().mass;
                CurrentPos += V.normalized * StepLength;
                Gizmos.DrawLine(CurrentPos, CurrentPos += V.normalized * StepLength);

                if (D <= 0.9f)
                {
                    break;
                }

            }
        }
    }
}