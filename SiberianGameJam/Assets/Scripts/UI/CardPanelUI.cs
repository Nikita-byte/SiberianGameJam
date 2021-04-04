using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class CardPanelUI : MonoBehaviour
{
    [SerializeField] private Button _first;
    [SerializeField] private Button _second;
    [SerializeField] private Button _third;
    [SerializeField] private Button _fourth;
    [SerializeField] private Button _fifth;

    public Button First { get { return _first; } }
    public Button Second { get { return _second; } }
    public Button Third { get { return _third; } }
    public Button Fourth { get { return _fourth; } }
    public Button Fifth { get { return _fifth; } }

}
