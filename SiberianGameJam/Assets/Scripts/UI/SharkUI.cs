using UnityEngine.UI;
using UnityEngine;


public class SharkUI : MonoBehaviour
{
    [SerializeField] private Button _head;
    [SerializeField] private Button _body;
    [SerializeField] private Button _arms;

    public Button GetHead()
    {
        return _head;
    }

    public Button Getbody()
    {
        return _body;
    }

    public Button GetArm()
    {
        return _arms;
    }
}
