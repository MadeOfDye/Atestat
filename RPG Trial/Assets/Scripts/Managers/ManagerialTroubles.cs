using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
    public string _nextScene;
    public GameObject _player;
    public IEnumerator LoadSceneAsync()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_nextScene, LoadSceneMode.Additive);

        while(!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.MoveGameObjectToScene(_player, SceneManager.GetSceneByName(_nextScene));
        SceneManager.UnloadSceneAsync(currentScene);
    }

}
