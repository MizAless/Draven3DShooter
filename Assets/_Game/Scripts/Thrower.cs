using System;
using _Game.Scripts;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private Axe _axePerfab;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _upAngle;
    [SerializeField] private Camera _camera;

    private bool _isAttacking = false;
    private Mover _mover;
    public event Action Throwed;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_isAttacking)
                return;

            _isAttacking = true;
            Throwed?.Invoke();
        }
    }

    public void OnThrow()
    {
        Vector3 direction;

        if (TryGetViewTarget(out Vector3 point))
            direction = (point - _throwPoint.position).normalized;
        else
            direction = GetViewDirection();

        Debug.DrawRay(_throwPoint.position, direction, Color.green, 2f);
        
        var axe = Instantiate(_axePerfab, _throwPoint.position, transform.rotation);
        axe.Init(_mover);
        axe.Launch(Quaternion.Euler(-_upAngle, 0, 0) * direction, _throwForce);
        axe.Catched += AxeOnCatched;
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

    private void AxeOnCatched()
    {
        print("Axe Catched");
    }

    public void OnEndAttack()
    {
        _isAttacking = false;
    }
}