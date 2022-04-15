using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PredictMotion : MonoBehaviour
{
    public LineRenderer lr;
    private int i;
    public bool useSliders;
    public Vector2 InitialVelocity;
    private Vector2 CurrentPos;
    public int steps;
    private Vector2 V;
    private float StepLength;
    public float G;

    public Slider Y;
    public Slider X;

    public float fps;
    public List<Vector2> Points;
    [SerializeField]
    private float D;
    [SerializeField]
    private bool moveEnabled;
    private void start()
    {
        lr = GetComponent<LineRenderer>();
        moveEnabled = false;
        i = 0;
        
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;

    }
    
    private void Addvelocity()
    {
        moveEnabled = true;
        GetComponent<Rigidbody2D>().AddForce(InitialVelocity);
    }

    private void FixedUpdate()
    {
        fps = 50;
        lr.positionCount = Points.Count;
        if(useSliders == true) 
        {
            InitialVelocity.x = X.value;
            InitialVelocity.y = Y.value;
        }
        else if(i == 0)
        {
            i++;
            Addvelocity();
        }
        
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

        if (moveEnabled == false)
        {
            Points.Clear();
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
                //Gizmos.DrawSphere(CurrentPos, 0.1f);
                Points.Add(CurrentPos);
                
                if (D <= 0.9f)
                {
                    break;
                }

            }
            for (int x = 0; x < Points.Count; x++)
            {
                if(x < lr.positionCount)
                {
                    lr.SetPosition(x, Points[x]);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
