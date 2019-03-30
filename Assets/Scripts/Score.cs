using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour {

    public static Score instance;

    public Text t_Score;
    public Text t_Timer;

    public Text t_TotalScore;
    public Text t_TotalTimer;

    private int i_CurrentScore;
    private float f_Timer;
	private bool b_StopTimer;

	void Awake () {

        if (instance == null)
            instance = this;

        i_CurrentScore = 0;
        t_Score.text = i_CurrentScore.ToString();
		b_StopTimer = false;
		f_Timer = 0f;
	}
	
    void Update ()
    {
		if (b_StopTimer) return;

		f_Timer += Time.deltaTime;

        int minutes = (int) f_Timer / 60;
        int seconds = (int) f_Timer % 60;
        int fraction = (int) (f_Timer * 100) % 100;

        t_Timer.text = String.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
    }

	public float GetTime () 
	{
		return f_Timer;
	}

	public int GetScore () 
	{
		return i_CurrentScore;
	}

	public void SetTime (float time) 
	{
		f_Timer = time;
	}

	public void SetScore (int score) 
	{
		i_CurrentScore = score;
		t_Score.text = i_CurrentScore.ToString();
	}

    public void AddMovement ()
    {
        i_CurrentScore++;
        t_Score.text = i_CurrentScore.ToString();
    }

    public void InsertFinalResults ()
    {
        t_TotalScore.text = t_Score.text;
        t_TotalTimer.text = t_Timer.text;
		b_StopTimer = true;
    }
}
