using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [Header("Bot stats")]
    [SerializeField] private int _speed;
    [SerializeField] private int _speed_damage;
    [SerializeField] private int _damage;
    [SerializeField] private int _radius;
    [SerializeField] private int _hp;
    [SerializeField] private int _price;

    private List<Vector3> _path;
    private int _index;

    public void Init(List<Vector3> path)
    {
        _path = path;
        _index = 0;
        transform.position = _path[0];
    }

    void Update()
    {
        if (_path == null || _index >= _path.Count)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            _path[_index],
            _speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, _path[_index]) < 0.05f)
        {
            _index++;
        }
    }
}
