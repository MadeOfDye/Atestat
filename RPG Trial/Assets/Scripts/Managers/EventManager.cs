using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //This is put inside your class, but outside of Start or Update
[SerializeField] private GameObject playerPrefab = null;
    [SerializeField] private Camera camGrill = null;
    private void Awake()
    {
        InstantiateMainObjects();
    }
    public void InstantiateMainObjects()
    {
        
        GameObject playerInstance = Instantiate(playerPrefab);
       playerInstance.transform.position =new Vector3(23.6f, 30.7f, 30.5f); // just an example, set player position to 0,0,0
        Camera cameraInstance = Instantiate(camGrill);
        cameraInstance.transform.position = playerInstance.transform.position;
    }
}
