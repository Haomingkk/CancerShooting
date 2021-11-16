using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float waitAfterDying = 2f;

    private void Awake()
	{
        instance = this;
	}

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDied()
    {
        StartCoroutine(PlayerDiedCo());
    }

    public IEnumerator PlayerDiedCo()
    {
        yield return new WaitForSeconds(waitAfterDying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //public void Reset()
	//{
    //    SceneManager.LoadScene("Level1");
	//}
    //public void Reset2()
	//{
    //    SceneManager.LoadScene("Level2");
	//}
    //public void Reset3()
	//{
    //    SceneManager.LoadScene("Level3");
	//}
    //public void GoScene2()
    //{
    //    SceneManager.LoadScene("Level2");
    //}
    //
    //public void GoScene3()
	//{

    //    SceneManager.LoadScene("Level3");
	//}
    //
    //public void GoBack()
    //{
    //    SceneManager.LoadScene("MainMenuScene");
    //}
    //
    
    public void Exit()
    {
        Application.Quit();
    }
}
