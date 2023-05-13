using UnityEngine.SceneManagement;

public class Util
{
	public static void toNextScene()
	{
		toSceneWithIndex(SceneManager.GetActiveScene().buildIndex + 1);
	}  

	public static void toSceneWithIndex(int index)
	{
		SceneManager.LoadScene(index);
	}

	public static string getValueFrom(string token)
	{
		return token.Split(':')[1];
	} 
	public static string getKeyFrom(string token)
	{
		return token.Split(':')[0];
	}
}