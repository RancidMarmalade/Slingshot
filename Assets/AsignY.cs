using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsignY : MonoBehaviour
{
    public Slider Yv;
    public Slider Xv;
    public Text Y;
    public Text X;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Y.text = Yv.value.ToString();
        X.text = Xv.value.ToString();
    }
}
