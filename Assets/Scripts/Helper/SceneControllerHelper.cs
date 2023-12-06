using UnityEngine.SceneManagement;

public class SceneControllerHelper
{
    /// <summary>
    /// シーン移動
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
