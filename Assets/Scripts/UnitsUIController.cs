using UnityEngine;
using System.Collections.Generic;

public class UnitsUIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _allowedUnitPrefabs;
    [SerializeField] private Transform _buttonsParent;
    [SerializeField] private UnitButton _buttonPrefab;

    void Start()
    {
        foreach (var unit in _allowedUnitPrefabs)
        {
            UnitButton btn = Instantiate(_buttonPrefab, _buttonsParent);
            btn.Init(unit);
        }
    }
}
