using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerialTroubles : MonoBehaviour
{
  public  static void LoadSceneLevel()
    {
        Debug.Log("Here");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}
