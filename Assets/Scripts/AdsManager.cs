using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {

	public static AdsManager instance;

	public Animator anim;

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	float f_TimePassed = 0f;

	void Update () 
	{
		f_TimePassed += Time.deltaTime;

		if (f_TimePassed > 294f) 
		{
			anim.SetBool ("Show", true);
		}

		if (f_TimePassed > 300f) {

			ShowAd ();
			f_TimePassed = 0f;
		}
	}

	public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}

		anim.SetBool ("Show", false);
	}

}
