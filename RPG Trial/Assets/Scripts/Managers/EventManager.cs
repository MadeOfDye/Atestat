using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    //This is put inside your class, but outside of Start or Update
[SerializeField] private GameObject playerPrefab = null;
    [SerializeField] private Camera camGrill = null;

    public Canvas cannedFish;
    
    private void Awake()
    {
        InstantiateMainObjects();
        cannedFish.enabled = false;
        activated = false;
    }
    public void InstantiateMainObjects()
    {
        GameObject playerInstance = Instantiate(playerPrefab);
       playerInstance.transform.position =new Vector3(-9.2f,16f,-9.4f); // just an example, set player position to 0,0,0
        Camera cameraInstance = Instantiate(camGrill);
        cameraInstance.transform.position = playerInstance.transform.position;
    }

    
    public void RandomizeEnemiesWithStats()
    {

    }
    bool activated;
    public void CanvasActivation()
    {
          if (Input.GetKeyDown(KeyCode.R))
            {
                if (activated == false)
                {
                cannedFish.enabled = true;
                    activated = true;
                Cursor.lockState = CursorLockMode.Confined;
                }
                else
                {
                cannedFish.enabled = false;
                    activated = false;
                Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    public void Update()
    {
        CanvasActivation();
    }
}
