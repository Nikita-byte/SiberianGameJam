using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public sealed class UI : MonoBehaviour
{
    [SerializeField] private EnemyUI _enemyUI;
    [SerializeField] private CardPanelUI _cardsPanel;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private HoodPanel _rightPanel;
    [SerializeField] private HoodPanel _leftPanel;
    [SerializeField] private CardHood _rightCard;
    [SerializeField] private CardHood _leftCard;
    [SerializeField] private Text _discription;
    public DeadPanel deadPanel;

    private Vector2 _offPos = new Vector2(0,200);
    private Vector2 _onPos = new Vector2(0, 336);
    private bool _tempSide;
    private Sprite _tempSprite;
    public Text Discription { get { return _discription; } }
    public CardPanelUI CardPanel { get { return _cardsPanel; } }

    public void OpenEnemyUI(EnemyType enemyType)
    {
        _enemyUI.gameObject.SetActive(true);
        _enemyUI.OpenUI(enemyType);
    }

    public void CloseEnemyUI()
    {
        _enemyUI.gameObject.SetActive(false);
    }

    public EnemyUI GetEnemyUI()
    {
        return _enemyUI; 
    }

    public void Text(string text, Vector3 pos, bool isGreen)
    {
        if (!isGreen)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Text"), _canvas.transform);
            go.GetComponent<TextMessage>().SetText(text, pos + Vector3.up * 10);
        }
        else
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Text"), _canvas.transform);
            go.GetComponent<Text>().color = Color.green;
            go.GetComponent<TextMessage>().SetText(text, pos + Vector3.up * 10);
        }
    }

    public void TurnOnCardPanel()
    {
        _cardsPanel.GetComponent<RectTransform>().DOAnchorPos(_onPos, 1);
    }

    public void TurnOffCardPanel()
    {
        _cardsPanel.GetComponent<RectTransform>().DOAnchorPos(_offPos, 1);
    }

    public void TurnOnHoodPanel(bool side, string enemyHP,string enemyMaxHP, string playerHP, string playerMaxHP, Sprite enemySprite, Sprite playerSprite)
    {
        if (side)
        {
            _rightPanel.OnPanel(enemySprite, enemyMaxHP, enemyHP);
            _leftPanel.OnPanel(playerSprite,playerMaxHP, playerHP);
        }
        else
        {
            _leftPanel.OnPanel(enemySprite, enemyMaxHP, enemyHP);
            _rightPanel.OnPanel(playerSprite, playerMaxHP, playerHP);
        }

        TurnOnCardPanel();
    }

    public void OpenActiveCard(bool side, Sprite sprite)
    {
        _tempSide = side;
        _tempSprite = sprite;

        if (!side)
        {
            _rightCard.OnPanel(sprite);
        }
        else
        {
            _leftCard.OnPanel(sprite);
        }
    }

    public void CloseActiveCard()
    {
        _leftCard.OffPanel();
        _rightCard.OffPanel();
    }

    public void TurnOffHoodPanel()
    {
        _rightPanel.OffPanel();
        _leftPanel.OffPanel();
        TurnOffCardPanel();
    }

    public void SwitchSideCard()
    {
        if (_tempSide)
        {
            _rightCard.OnPanel(_tempSprite);
            _leftCard.OffPanel();
        }
        else
        {
            _leftCard.OnPanel(_tempSprite);
            _rightCard.OffPanel();
        }
    }
}
