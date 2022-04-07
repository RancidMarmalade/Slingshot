using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class nextleve : MonoBehaviour
{
    public ParticleSystem Explosion;
    public Text Lost;
    public GameObject Player;
    private void Start()
    {
        Lost.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided");
        if(other.tag == "goal" && SceneManager.GetActiveScene().buildIndex > 6)
        {
            Debug.Log("collided with goal");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(other.tag == "goal" && SceneManager.GetActiveScene().buildIndex == 6)
        {
            Debug.Log("collided with goal");
            SceneManager.LoadScene(0);
        }

        if(other.tag != "goal")
        {
            Debug.Log("did not collide with goal");
            Destroy(Player.gameObject);
            Lost.enabled = true;
            if(Explosion.isPlaying == false) 
            {
                Explosion.Play();
            }
            StartCoroutine(reload());
        }
    }
    IEnumerator reload()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
