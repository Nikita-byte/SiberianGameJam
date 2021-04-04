using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;


public class TextMessage : MonoBehaviour
{
    private Text _text;
    private float _duration = 2f;

    private void Start()
    {
        Invoke("DestroyText",2.1f);
    }

    public void SetText(string text, Vector3 pos)
    {
        _text = GetComponent<Text>();
        _text.text = text;
        gameObject.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(pos);
        gameObject.GetComponent<RectTransform>().DOAnchorPosY(100, _duration, true);
        _text.DOColor(Color.clear, _duration);
    }

    public void DestroyText()
    {
        Destroy(gameObject);
    }
}
