using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;


public class CardsUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform _toPosition;
    [SerializeField] private RectTransform _OfPos;
    [SerializeField] private UI _ui;

    public AudioSource audioSource;
    public string _discription;
    private float _duration = 0.5f;
    private Vector2 _position;
    public bool IsActive;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _position = gameObject.GetComponent<RectTransform>().anchoredPosition;
        IsActive = true;
    }

    public void OffCard()
    {
        gameObject.GetComponent<RectTransform>().DOAnchorPos(_OfPos.anchoredPosition, _duration);
        IsActive = false;
    }

    public void RefreshCard()
    {
        gameObject.GetComponent<RectTransform>().DOAnchorPos(_position, _duration);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _ui.Discription.text = _discription;
        audioSource.Play();

        if (IsActive)
        {
            gameObject.GetComponent<RectTransform>().DOAnchorPos(_toPosition.anchoredPosition, _duration);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _ui.Discription.text = "";

        if (IsActive)
        {
            gameObject.GetComponent<RectTransform>().DOAnchorPos(_position, _duration);
        }
    }
}
