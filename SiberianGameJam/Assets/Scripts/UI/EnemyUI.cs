using UnityEngine.UI;
using UnityEngine;


public class EnemyUI : MonoBehaviour
{
    [SerializeField] private SharkUI _sharkUI;

    public void OpenUI(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Shark:
                _sharkUI.gameObject.SetActive(true);
                break;
        }
    }

    public SharkUI GetShark()
    {
        return _sharkUI;
    }
}
