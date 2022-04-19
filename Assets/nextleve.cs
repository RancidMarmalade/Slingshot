using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class nextleve : MonoBehaviour
{
    public GameObject spaceship;
    public TrailRenderer trail;
    //public ParticleSystem Explosion;
    public Text Lost;
    private void Start()
    {
        
        trail.emitting = true;
        spaceship.GetComponent<MeshRenderer>().enabled = true;
        Lost.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "goal" && SceneManager.GetActiveScene().buildIndex < 6)
        {
            Debug.Log("next level");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (collision.collider.tag == "goal" && SceneManager.GetActiveScene().buildIndex == 6)
        {
            Debug.Log("main menu");
            SceneManager.LoadScene(0);
        }

        if (collision.collider.tag != "goal")
        {
            
            spaceship.GetComponent<MeshRenderer>().enabled = false;
            trail.emitting = false;
            Lost.enabled = true;
           /* if (Explosion.isPlaying == false)
            {
                Debug.Log("particles wooo");
            }*/
            StartCoroutine(reload());
        }
    }
   
    IEnumerator reload()
    {
        
        yield return new WaitForSecondsRealtime(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
