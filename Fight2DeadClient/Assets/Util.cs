using UnityEngine.SceneManagement;

public class Util
{
	public static void toNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}  
}