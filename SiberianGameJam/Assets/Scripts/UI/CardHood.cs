using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class CardHood : MonoBehaviour
{
    [SerializeField] private HoodSide _hoodSide;

    private Image _image;
    private float _duration = 1f;
    private Vector2 _pos = new Vector2(420, 0);

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void OnPanel(Sprite sprite)
    {
        _image.sprite = sprite;

        GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, _duration);
    }

    public void OffPanel()
    {
        if (_hoodSide == HoodSide.Right)
        {
            GetComponent<RectTransform>().DOAnchorPos(_pos, _duration);
        }
        else
        {
            GetComponent<RectTransform>().DOAnchorPos(-_pos, _duration);
        }
    }
}