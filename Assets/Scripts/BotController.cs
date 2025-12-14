using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class BotController : MonoBehaviour
{
    [Header("Bot stats")]
    [SerializeField] private float _speed;
    [SerializeField] private float _speed_damage;
    [SerializeField] private int _damage;
    [SerializeField] private float _radius;
    [SerializeField] private int _hp;
    [SerializeField] public int _price;

    private List<Vector3> _path;
    private int _index;

    private Animator _animator;
    private bool _isAttacking;
    private Coroutine _attackCoroutine;

    public void Init(List<Vector3> path)
    {
        _path = path;
        _index = 0;
        transform.position = _path[0];
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isAttacking)
        {
            _animator.SetBool("IsMoving", false);
            return;
        }

        Move();
    }

    private void Move()
    {
        if (_path == null || _index >= _path.Count)
        {
            _animator.SetBool("IsMoving", false);
            return;
        }

        Vector3 beforeMove = transform.position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            _path[_index],
            _speed * Time.deltaTime
        );

        bool isMoving = Vector3.Distance(beforeMove, transform.position) > 0.0001f;
        _animator.SetBool("IsMoving", isMoving);

        if (Vector3.Distance(transform.position, _path[_index]) < 0.05f)
        {
            _index++;
        }
    }

    // ===== COLLISION =====

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<TilemapCollider2D>(out _))
        {
            StartAttack();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<TilemapCollider2D>(out _))
        {
            StopAttack();
        }
    }

    // ===== ATTACK =====

    private void StartAttack()
    {
        if (_isAttacking) return;

        _isAttacking = true;
        _attackCoroutine = StartCoroutine(AttackRoutine());
    }

    private void StopAttack()
    {
        if (!_isAttacking) return;

        _isAttacking = false;

        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);
    }

    private IEnumerator AttackRoutine()
    {
        while (_isAttacking)
        {
            _animator.SetTrigger("Attack");
            yield return new WaitForSeconds(_speed_damage);
        }
    }
}
