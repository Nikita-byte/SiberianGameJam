using UnityEngine;
using UnityEngine.UI;


public sealed class UI : MonoBehaviour
{
    [SerializeField] private EnemyUI _enemyUI;
    [SerializeField] private CardsUI _cardsUI;
    [SerializeField] private Canvas _canvas;

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

    public void Text(string text, Vector3 pos)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Text"), _canvas.transform);
        go.GetComponent<TextMessage>().SetText(text, pos + Vector3.up * 10);
    }
}
