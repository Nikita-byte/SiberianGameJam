using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class HoodPanel : MonoBehaviour
{
    [SerializeField] private HoodSide _hoodSide;
    [SerializeField] private Text _text;

    private Image _image;
    private float _duration = 1f;
    private Vector2 _pos = new Vector2(420, 0);

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void OnPanel(Sprite sprite, string MaxHP, string HP)
    {
        _image.sprite = sprite;
        _text.text = HP + " / " + MaxHP;

        GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, _duration);
    }

    public void OffPanel()
    {
        if (_hoodSide == HoodSide.Right)
        {
            GetComponent<RectTransform>().DOAnchorPos(_pos,_duration);
        }
        else
        {
            GetComponent<RectTransform>().DOAnchorPos(-_pos,_duration);
        }
    }
}
