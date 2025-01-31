using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

// using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    private Notifications notification;

    private StreetLampController streetLampController;

    void Start()
    {
        notification = FindObjectOfType<Notifications>();
        streetLampController = FindObjectOfType<StreetLampController>();
        if (notification == null)
        {
            Debug.LogError("No Notifications object found in the scene.");
        }
        else
        {
            Debug.Log(notification.name.ToString());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey) && isInRange && interactAction != null)
        {
            interactAction.Invoke();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.attachedRigidbody.name == null){
                return;
            }
            else if (collision.attachedRigidbody.name == "GameComplete"){
                PlayerManager.Instance.addScore(GameManager.score);
                GameManager.score = 0;
                SceneLoader.Instance.LoadmapScene();
            }

            isInRange = true;
            triggerNotification(collision);
        }
        catch (System.NullReferenceException)
        {
            Debug.Log("RigidBody is initializing...."); //to handle the exception due to funciton being called before initialization
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            notification.getNotificationMessage("empty");
            isInRange = false;
        } 
    }

    void triggerNotification(Collider2D collision)
    {
        try
        {
            if (collision.attachedRigidbody.name == "WindPower")
            {
                notification.getNotificationMessage("WindPower", isInRange);
            }
            else if (collision.attachedRigidbody.name == "HydroPower")
            {
                notification.getNotificationMessage("HydroPower", isInRange);
            }
            else if (collision.attachedRigidbody.name == "solar_panel")
            {
                notification.getNotificationMessage("SolarPower", isInRange);
            }
            else if (collision.attachedRigidbody.name == "biomass")
            {
                notification.getNotificationMessage("BioMassPower", isInRange);
            }
            else if (collision.attachedRigidbody.name == "lamp1" || collision.attachedRigidbody.name == "lamp2" || collision.attachedRigidbody.name == "lamp3" || collision.attachedRigidbody.name == "lamp4" || collision.attachedRigidbody.name == "lamp5")
            {
                notification.getNotificationMessage("StreetLamps", streetLampController.GetLampState());
            }
            else if (collision.attachedRigidbody.name.Contains("Bulby"))
            {
                notification.getNotificationMessage(collision.attachedRigidbody.name, isInRange);
            }
            else
            {
                // Debug.Log(collision.attachedRigidbody.name);
            }
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("Sending notification fails due to missing rigidBody name" + e);
        }
    }
}
