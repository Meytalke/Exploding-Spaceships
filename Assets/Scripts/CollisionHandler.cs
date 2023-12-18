using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] float loadNextLevelDelay = 2f;
    [SerializeField] ParticleSystem crashVFX;
    // void OnCollisionEnter(Collision other)
    // {
    //     Debug.Log(this.name + "--Collided with--" + other.gameObject.name);

    // }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            StartSuccessSequernce();
        }
        else
        {
        Debug.Log($"{this.name} **Triggered by** {other.gameObject.name}");
        StartCrashSequence();
        }
    }
    void StartSuccessSequernce()
    {
        GetComponent<PlayerControls>().enabled = false;   
        GetComponent<BoxCollider>().enabled = false; 
        Invoke("LoadNextLevel", loadNextLevelDelay); 
    }

    void LoadNextLevel()
    {
       int cussentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       int nextSceneIndex = cussentSceneIndex + 1;
       if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
       {
        nextSceneIndex = 0; 
       }
       SceneManager.LoadScene(nextSceneIndex);
    }

    void StartCrashSequence()
    {        
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;   
        GetComponent<BoxCollider>().enabled = false;   
        Invoke("ReloadLevel", loadDelay);  
    }

    void ReloadLevel()
    {
        int cussentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(cussentSceneIndex);
    }
    
}