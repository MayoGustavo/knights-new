using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;
using System;

public class FBManager : MonoBehaviour {

	public static FBManager instance;

	// Awake function from Unity's MonoBehavior
	void Awake ()
	{
		if (instance == null)
			instance = this;

		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}

	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}


	public void LogIn () 
	{
		var perms = new List<string>(){"public_profile", "email", "user_friends"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
	}

	private void AuthCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}

			ShareLink ();
		} else {
			Debug.Log("User cancelled login");
		}
	}


	public void ShareLink () 
	{
		FB.ShareLink(
			new Uri("https://play.google.com/store/apps/details?id=com.vanillaicecreamstudio.knights"),
			"Knights",
			"Knights is a fun and challenging little chess puzzle.",
			new Uri("https://lh3.googleusercontent.com/8EXOY4R6Y_JT9YjoqlJQf6D5dUatRq-5GvPxMldNaPOfQ9svno6KpwzXFou8Jog0cmA=h80"),
			callback: ShareCallback
		);
	}

	private void ShareCallback (IShareResult result) {
		if (result.Cancelled || !String.IsNullOrEmpty(result.Error)) {
			Debug.Log("ShareLink Error: "+result.Error);
		} else if (!String.IsNullOrEmpty(result.PostId)) {
			// Print post identifier of the shared content
			Debug.Log(result.PostId);
		} else {
			// Share succeeded without postID
			Debug.Log("ShareLink success!");
		}
	}

}
