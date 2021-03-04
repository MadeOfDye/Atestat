using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerialTroubles : MonoBehaviour 
{
   
  public  static void LoadNextScene()
    {
        //Scene obScene = SceneManager.GetSceneByName("TestLevel");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
       // SceneManager.MoveGameObjectToScene(GameObject.Find("Player"), obScene);
    }
    public static void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public static void LoadHubScene()
    {
        SceneManager.LoadScene(1);
    }
        
}
