using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _priceText;

    private GameObject _unitPrefab;

    public void Init(GameObject prefab)
    {
        _unitPrefab = prefab;

        BotController bot = prefab.GetComponent<BotController>();

        // Цена
        _priceText.text = bot._price.ToString();

        // Картинка — берём SpriteRenderer из префаба
        SpriteRenderer sr = prefab.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            _icon.sprite = sr.sprite;
        }
        else
        {
            Debug.LogWarning($"No SpriteRenderer found in {prefab.name}");
        }
    }

    public GameObject GetPrefab()
    {
        return _unitPrefab;
    }
}
