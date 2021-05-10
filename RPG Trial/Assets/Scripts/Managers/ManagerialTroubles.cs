using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public  class ManagerialTroubles
{
  
  public  static void LoadNextScene()
    {
       
        SceneManager.LoadScene ("TestLevel");
        
    }
 
  /*  public static IEnumerator LoadSceneAsync()
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
  */
}
