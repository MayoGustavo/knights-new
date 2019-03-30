using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public static Menu instance;
    public GameObject[] go_ModalWindows;

    private bool b_OnFinalSequence;

    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }

        b_OnFinalSequence = false;
    }

	public void SwitchScene (int iScene) 
	{
		SceneManager.LoadScene (iScene);
	}

    public void RestartGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGame(bool b_Finish)
    {
		CloseModalWindow(4);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowModalWindow(int i_Index)
    {
        go_ModalWindows[i_Index].SetActive(true);
    }

    public void CloseModalWindow (int i_Index)
    {
        go_ModalWindows[i_Index].SetActive(false);
    }

    public void ExitGame ()
    {
        Application.Quit();
    }

    public void OnFinalSequence ()
    {
        b_OnFinalSequence = true;
    }

    public void OffFinalSequence()
    {
        b_OnFinalSequence = false;
        HideFinalSequence();
    }

    public void OnClickFinalSequence ()
    {
        if (b_OnFinalSequence)
        {
            ShowFinalSequence();
        }
    }

    public void OffClickFinalSequence()
    {
        if (b_OnFinalSequence)
        {
            HideFinalSequence();
        }
    }

    private void ShowFinalSequence ()
    {
        ShowModalWindow(3);
        CloseModalWindow(2);
    }

    private void HideFinalSequence()
    {
        ShowModalWindow(2);
        CloseModalWindow(3);
    }
}
