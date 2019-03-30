using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Knight : MonoBehaviour {

    public bool b_IsVisible;
    public int i_SquareContent;
    public int i_Sequence;

    private Image _image;
    private Text _text;
	private Button _button;

    void Awake()
    {
        _image = GetComponent<Image>();
        _text = transform.FindChild("Sequence").GetComponent<Text>();
		_button = GetComponent<Button>();
    }

    void Start() {

		LoadKnight ();
    }

	public void LoadKnight() 
	{
		if (!b_IsVisible)
		{
			_image.enabled = false;
			_text.enabled = false;
		}
		_text.text = i_Sequence.ToString();
	}

	public void Activate () 
	{
		_image.enabled = true;
		_text.enabled = true;
		_button.enabled = true;
	}

	public void Deactivate () 
	{
		_image.enabled = false;
		_text.enabled = false;
		_button.enabled = false;
	}

    public void OnClickKnight ()
    {
        KnightMatrix.instance.AttemptMoveKnight(i_SquareContent, i_Sequence);
    }
}
