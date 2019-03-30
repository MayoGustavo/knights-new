using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Square : MonoBehaviour {

	public bool b_IsVisible;
    public Sprite s_DefaultImage;

    private Image _image;

    void Awake ()
    {
        _image = GetComponent<Image>();
    }

	void Start () {

        if (!b_IsVisible)
            _image.enabled = false;

        _image.sprite = s_DefaultImage;
	}
	
}
