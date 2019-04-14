using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Knight : MonoBehaviour {

    const string SHOW = "show";
    const string HIDE = "hide";
    const string HIGHLIGHT = "highlight";

    public bool b_IsVisible;
    public int i_SquareContent;
    public int i_Sequence;

    private Image _image;
    private Text _text;
	private Button _button;
    private Animator _tor;

    public delegate void HideAnimationCallback(int a, int b, int c, int d);
    public delegate void ShowAnimationCallback();

    struct AnimationItem
    {
        public HideAnimationCallback cb;
        public int rFrom;
        public int cFrom;
        public int rTo;
        public int cTo;
    }

    private AnimationItem mAnimItem;
    private ShowAnimationCallback showCallback;

    void Awake()
    {
        _image = GetComponent<Image>();
        _text = transform.Find("Sequence").GetComponent<Text>();
		_button = GetComponent<Button>();
        _tor = GetComponent<Animator>();
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

    public void ShowAnimation(ShowAnimationCallback callback)
    {
        showCallback = callback;
        _tor.SetTrigger(SHOW);
    }

    public void ShowAnimationFinished()
    {
        showCallback();
    }

    public void HideAnimation(HideAnimationCallback callback, int rowFrom, int colFrom, int rowTo, int colTo)
    {
        mAnimItem.cb = callback;
        mAnimItem.rFrom = rowFrom;
        mAnimItem.cFrom = colFrom;
        mAnimItem.rTo = rowTo;
        mAnimItem.cTo = colTo;

        _tor.SetTrigger(HIDE);
    }
    
    public void HideAnimationFinished()
    {
        mAnimItem.cb(mAnimItem.rFrom, mAnimItem.cFrom, mAnimItem.rTo, mAnimItem.cTo);
    }

    public void HightlightTile(bool isHighlighted)
    {
        _tor.SetBool(HIGHLIGHT, isHighlighted);
    }
}
