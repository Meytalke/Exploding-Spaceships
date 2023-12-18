using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down - based upon player input")][SerializeField] float controlSpeed = 35f;
    [Tooltip("How far player moves horizontally")][SerializeField] float xRange = 30f;
    [Tooltip("How far player moves vertically")][SerializeField] float yRange = 15f;

    [Header("Lasers gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]

    [SerializeField] float positionPicthFactor = -2f;
    [SerializeField] float positionyawFactor = 5f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPicthFactor = -15f;
    [SerializeField] float controlRollFactor = -20f;

    // [SerializeField] InputAction fire;

    // void OnEnable()
    // {
    //     fire.Enable;
    // }

    // void OnDisable()
    // {
    //     fire.Disable;
    // }


    float xlThrow,yThrow;   

    // [SerializeField] InputAction movement;
    // void OnEnable()
    // {
    //     movement.Enable();
    // }

    // void OnDisable()
    // {
    //     movement.Disable();
    // }

    // Update is called once per frame
    void Update()
    {  
        processTranslation();
        processRotation();
        processFiring();
    }
    
    void processRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPicthFactor;
        float pitchDueToControlThrow = yThrow * controlPicthFactor;
        float pitch =  pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.y * positionyawFactor;
        float roll = yThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    void processTranslation()
    {
        // float xlThrow =movement.ReadValue<Vector2>().x;
        // float yThrow =movement.ReadValue<Vector2>().y;
        //Debug.Log(xlThrow);
        //Debug.Log(yThrow);
        xlThrow = Input.GetAxis("Horizontal");   
        yThrow = Input.GetAxis("Vertical");
        
        float xOffset = xlThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos,-xRange,xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos,-yRange,yRange);

        transform.localPosition = new Vector3(clampedXPos,clampedYPos,transform.localPosition.z);
    }

    void processFiring()
    {
        if (Input.GetButton("Fire1"))
        {
           setLasersActive(true);
        }
        else
        {
            setLasersActive(false);
        }
    }
    void setLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            //laser.SetActive(true);
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }

    }
}
