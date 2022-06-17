using System.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private ActionController actionController;
    [SerializeField] private RocketManager rocketManager;
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private PlayerSO playerSO;

    private Animator _animator;
    private CharacterController _characterController;
    [SerializeField] private SoliderPool soldierPool;
    private float distToTarget;
    private Bot target;
    private float distToEnemy;

    [Header("Move Setup")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float turnSpeed = 0.1f;
    private float _turnSmoothVelocity;

    [Header("Shooting Setup")]
    [SerializeField] private ParticleSystem PsShooting;
    [SerializeField] private ParticleSystem PsShooting2;
    [SerializeField] private ParticleSystem PsShooting3;

    [Header("Rocket launcher")]
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private GameObject firePoint;
    private bool isShooting = false;
    private bool isTarget = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        InvokeRepeating("SelectTarget", 0f, 0.1f);
        AutoAim();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        if (!_characterController.isGrounded)
        {
            _characterController.Move(Vector3.down);
            if (transform.position.y < -3f) gameSetupSo.IsPlay = false;
        }

        var direction = new Vector3(actionController.Move.x, 0, actionController.Move.y);

        _animator.SetFloat("Move", direction.magnitude);

        if (direction.magnitude >= 0.1f)
        {
            _characterController.Move(direction * moveSpeed);
        }
    }

    private void AutoAim()
    {
        StartCoroutine(aiming());

        IEnumerator aiming()
        {
            while (true)
            {
                while (gameSetupSo.IsPause) yield return new WaitForSeconds(1f);
                if (isTarget)
                {
                    transform.LookAt(target.transform);

                    if (playerSO.Rockets > 0)
                    {
                        if (!isShooting) LaunchRocket();
                    }
                    else if (playerSO.Bullets > 0)
                    {
                        if (!isShooting) ShootRifle();
                    }
                }
                else
                {
                    isShooting = false;
                    PsShooting.Stop(true);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private void SelectTarget()
    {
        for (int i = 0; i < soldierPool.SoldierPool.Count; i++)
        {

            if (soldierPool.SoldierPool[i].IsAlive)
            {
                distToEnemy = Vector3.Distance(transform.position, soldierPool.SoldierPool[i].transform.position);
                if (distToEnemy < 6f)
                {
                    distToTarget = distToEnemy;
                    target = soldierPool.SoldierPool[i];
                    isTarget = true;
                    break;
                }
            }
        }
    }

    private void LaunchRocket()
    {
        isShooting = true;
        StartCoroutine(Shooting());

        IEnumerator Shooting()
        {

            while (isShooting)
            {
                playerSO.Rockets--;
                rocketManager.SpawnRocket(firePoint.transform.position, firePoint.transform.rotation);

                yield return new WaitForSeconds(.2f);

                if (playerSO.Rockets < 1)
                {
                    isShooting = false;
                }
            }
        }
    }

    private void ShootRifle()
    {
        PsShooting.Play();
        PsShooting2.Play();
        PsShooting3.Play();

        StartCoroutine(Shooting());

        IEnumerator Shooting()
        {
            isShooting = true;
            while (isShooting)
            {
                //playerSO.Bullets--;
                SoundManager.instance.PlayRifleShot();

                if (playerSO.Bullets < 1)
                {
                    PsShooting.Stop(true);
                    isShooting = false;
                }
                if (isTarget)
                {
                    if (!target.IsAlive)
                    {
                        isTarget = false;
                        PsShooting.Stop(true);
                        isShooting = false;
                    }
                }

                yield return new WaitForSeconds(0.09f);
            }
        }
    }
}


