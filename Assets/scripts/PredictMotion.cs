using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PredictMotion : MonoBehaviour
{
    public LineRenderer lr;
    public float G;
    private int i;
    public bool useSliders;
    public Vector2 InitialVelocity;
    private Vector2 CurrentPos;
    public int steps;
    private Vector2 V;
    private float StepLength;
    

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
    {/*
        dette er den funktion som opdaterer spillerens position og beregner spillerens trajektory og tegner den på skærmen

        jeg bruger fixedupdate fordi den ikke er afhængig af framerate men kun af tid.
        dvs. den bliver kørt 50 gange i sekunded, i stedet for en gang hver frame. 
        */
        fps = 50;
        lr.positionCount = Points.Count; // jeg fortæller min LineRendere component hvor mange punkter den skal bruge.
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
        {//det er i dette if statement at jeg ændre spillerens position og velocity. 
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
        {//denne kode er ansvarlig for at tegne linjen som spilleren vil følge når man spiller. 
            Points.Clear();

            D = 100000f;
            GameObject[] attractors = GameObject.FindGameObjectsWithTag("Attract");
            CurrentPos = transform.position;
            V = InitialVelocity;

            for (int i = 0; i < steps; i++)
            {/*
                her beregner jeg de forskellige kræfter som spilleren vil blive påvirket af og danner et punkt.

                funktionen køre så igen med udgangs punkt i det nye punkt. dette bliver den ved med intil man har et array af punkter som vi giver
                til line renderer. 

                */
                foreach (GameObject Attractor in attractors)
                {
                    if (Attractor != this)
                    {// dette er her hvor min implimentation af newtons tyngdekraft formel. 
                     // den hedder jo F = ((m1*m2)/r^2) * G

                        Vector2 direction = (new Vector2(Attractor.transform.position.x, Attractor.transform.position.y) - CurrentPos);
                        //jeg beregner vinklen mellem playeren og et givent objekt. 

                        float dist = direction.magnitude;
                        // finder afstanden mellem spilleren og det valgte objekt. 


                        float ForceMagnitude = (Attractor.GetComponent<Rigidbody2D>().mass - GetComponent<Rigidbody2D>().mass) / Mathf.Pow(dist, 2) * G;
                        //baseret på afstanden kan jeg beregne den kraft som de to objekter vil tiltrække hindanden med. 

                        V += ForceMagnitude * direction.normalized;
                        // her tilføjer jeg den beregnede kraft, med spillerens starthastighed. 

                        if (D >= dist)// denne funtion finder bare den mindste afstand mellem spilleren og et vilkårligt objekt. 
                        {
                            D = dist;
                        }
                    }
                }
                StepLength = (V.magnitude * (Time.fixedDeltaTime / fps)) / GetComponent<Rigidbody2D>().mass;
                CurrentPos += V.normalized * StepLength;// her opdaterer jeg den interne position som min for statement så genbruger som spillerens position
                
                Points.Add(CurrentPos);
                if (D <= 0.9f)// denne funktion stopper for statemented hvis man rammer et objekt
                {
                    break;
                }
            }
            for (int x = 0; x < Points.Count; x++)// dette for statement tildeler mine genererede punkter til min linerenderer som så tegner dem på skærmen
            {
                if(x < lr.positionCount)// hvis mine genererede punkter's index er større end 
                                        // mængden af punkter som min linerenderer kan tegne så stopper jeg med at tildele
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
