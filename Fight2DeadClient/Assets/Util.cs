using UnityEngine.SceneManagement;

public class Util
{
	public static void toNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}  
	public static string getValueFrom(string token)
	{
		return token.Split(':')[1];
	} 
}