using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float waitAfterDying = 2f;

    public GameObject pauseMenu;


    public static bool timeFlow = true;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (timeFlow == true)
            {
                GamePause();
            }
            else
            {
                GameContinue();
            }
        }
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

    public void GamePause()
    {
        timeFlow = false;
        UIController.instance.pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void GameContinue()
    {
        timeFlow = true;
        UIController.instance.pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
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
