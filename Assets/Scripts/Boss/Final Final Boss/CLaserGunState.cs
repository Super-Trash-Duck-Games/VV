using System.Collections;
using UnityEngine;

public class CLaserGunState : State
{
    private Cientist _cientist;
    private CData _data;

    private bool _isRight;
    private ParticleSystem _bullet;
    private int _bulletCounter;

    public CLaserGunState(Cientist cientific, CData data)
    {
        _cientist = cientific;
        _data = data;
    }
    public override void OnDrawGizmos()
    {
    }

    public override void OnEnter()
    {
        _cientist._currentState = CientistStates.LaserGunShoot;

        _data.targetPosition = 0;

        if (!_cientist.CheckCurrentPosition())
        {
            fsm.ChangeState(CientistStates.Run);
            return;
        }


        var pos = _cientist.transform.position.x;

        _bulletCounter = 0;

        _cientist.StartCoroutine(ShootSequence());

        _cientist.anim.SetBool("GunActive", true);
    }

    public override void OnExit()
    {
        _cientist.SelectRandomPosition();
        _cientist.anim.SetBool("GunActive", false);
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnTriggerEnter(Collider2D collision)
    {
    }

    public override void OnUpdate()
    {
        if (_data.kkPos.position.x > _cientist.transform.position.x)
            _cientist.transform.localScale = new Vector2(1, 1);
        else if (_data.kkPos.position.x < _cientist.transform.position.x)
            _cientist.transform.localScale = new Vector2(-1, 1);
    }

    private IEnumerator ShootSequence()
    {
        float timer = 0;
        while (_bulletCounter < _data.laserGunShots)
        {
            if (timer < _data.laserGunCooldown)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            else
            {
                timer = 0;
                Shoot();
                _cientist.anim.SetTrigger("ShootGun");
                _bulletCounter++;
            }
        }
        fsm.ChangeState(_cientist.GetNextState());
    }

    private void Shoot()
    {
        _bullet = _cientist.Create(_data.laserBullet).GetComponent<ParticleSystem>();
        _bullet.transform.position = _data.shootPoint.position;
        if (_data.kkPos.position.x < _cientist.transform.position.x) _bullet.transform.up = Vector2.right;
        else _bullet.transform.up = -Vector2.right;
    }
}
