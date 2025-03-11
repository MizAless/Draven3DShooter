using System;
using _Game.Scripts;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private Axe _axePerfab;
    [SerializeField] private Transform _throwPointL;
    [SerializeField] private Transform _throwPointR;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _upAngle;
    [SerializeField] private Camera _camera;

    private bool _isAttacking = false;
    private Mover _mover;
    private AxeAmmunition _axeAmmunition;
    private ThrowerSound _throwerSound;
    
    public event Action Throwed;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _axeAmmunition = GetComponent<AxeAmmunition>();
        _throwerSound = GetComponent<ThrowerSound>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_isAttacking)
                return;

            if (!_axeAmmunition.TryGet())
                return;

            _isAttacking = true;
            Throwed?.Invoke();
        }
    }

    public void OnThrow()
    {
        Vector3 direction;

        Transform throwPoint = _axeAmmunition.CurrentCount == 1 ? _throwPointR : _throwPointL;

        if (TryGetViewTarget(out Vector3 point))
            direction = (point - throwPoint.position).normalized;
        else
            direction = GetViewDirection();

        _throwerSound.PlayThrowAxeSound();
        
        var axe = Instantiate(_axePerfab, throwPoint.position, transform.rotation);
        axe.Init(_mover);
        axe.Launch(Quaternion.Euler(-_upAngle, 0, 0) * direction, _throwForce);
    }

    private bool TryGetViewTarget(out Vector3 point)
    {
        var ray = _camera.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));

        point = Vector3.zero;

        if (!Physics.Raycast(ray, out RaycastHit hit))
            return false;

        point = hit.point;
        return true;
    }

    private Vector3 GetViewDirection()
    {
        var ray = _camera.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
        return ray.direction.normalized;
    }

    public void OnEndAttack()
    {
        _isAttacking = false;
    }
}