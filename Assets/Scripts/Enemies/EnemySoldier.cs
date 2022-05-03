using UnityEngine;

public class EnemySoldier : Bot
{
    private Animator _animator;
    private Rigidbody _rb;
    private CapsuleCollider _collider;
    private Rigidbody[] _rbs;
    private Collider[] _colliders;

    public bool test = false;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rbs = GetComponentsInChildren<Rigidbody>();

        _collider = GetComponent<CapsuleCollider>();
        _colliders = GetComponentsInChildren<Collider>();

    }

    private void Update()
    {
        if (test)
        {
            test = false;
            Death();
        }
    }

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        IsAlive = false;
    }

    void Init()
    {
        IsAlive = true;
        _animator.enabled = true;
        ColliderControl(true);
        RigidBodyControl(true);
    }

    private void Death()
    {

        _animator.enabled = false;
        ColliderControl(false);
        RigidBodyControl(false);

    }

    private void ColliderControl(bool state)
    {
        
        foreach (var coll in _colliders)
        {
            coll.enabled = !state;
        }
        _collider.enabled = state;
    }
    
    private void RigidBodyControl(bool state)
    {
        
        foreach (var body in _rbs)
        {
            body.isKinematic = state;
        }
        _rb.isKinematic = !state;
    }
}
