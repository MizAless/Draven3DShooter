using _Game.Scripts;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    private static readonly int IsAttacking = Animator.StringToHash(nameof(IsAttacking));

    [SerializeField] private Axe _axePerfab;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _upEngle;

    private Animator _animator;

    private bool _isAttacking = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_isAttacking)
                return;

            _isAttacking = true;
            _animator.SetTrigger(IsAttacking);
        }
    }

    public void OnThrow()
    {
        var axe = Instantiate(_axePerfab, _throwPoint.position, Quaternion.identity);
        axe.Init(this);
        axe.Launch(Quaternion.Euler(-_upEngle, 0, 0) * transform.forward, _throwForce);
        axe.Catched += AxeOnCatched;
    }

    private void AxeOnCatched()
    {
        print("Axe Catched");
    }

    public void OnEndAttack()
    {
        _isAttacking = false;
    }
}