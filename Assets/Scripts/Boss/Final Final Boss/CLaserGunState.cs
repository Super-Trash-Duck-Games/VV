using System.Collections;
using UnityEngine;

public class CLaserGunState : State
{
    private Cientist _cientific;
    private CData _data;

    private bool _isRight;
    private ParticleSystem _bullet;
    private int _bulletCounter;

    public CLaserGunState(Cientist cientific, CData data)
    {
        _cientific = cientific;
        _data = data;
    }
    public override void OnDrawGizmos()
    {
    }

    public override void OnEnter()
    {
        _cientific._currentState = CientistStates.LaserGunShoot;

        var pos = _cientific.transform.position.x;
        if (pos < _data.rightMost.position.x && pos > _data.leftMost.position.x)
        {
            fsm.ChangeState(CientistStates.Run);
            return;
        }

        if (pos > _data.rightMost.position.x)
        {
            _cientific.transform.localScale = new Vector2(-1, 1);
            _isRight = true;
        }
        else if (pos < _data.leftMost.position.x)
        {
            _cientific.transform.localScale = new Vector2(1, 1);
            _isRight = false;
        }
        _bulletCounter = 0;

        _cientific.StartCoroutine(ShootSequence());
    }

    public override void OnExit()
    {
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnTriggerEnter(Collider2D collision)
    {
    }

    public override void OnUpdate()
    {

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
                _bulletCounter++;
            }
        }
        fsm.ChangeState(_cientific.GetNextState());
    }

    private void Shoot()
    {
                Debug.Log($"Shoot");
        _bullet = _cientific.Create(_data.laserBullet).GetComponent<ParticleSystem>();
        _bullet.transform.position = _data.shootPoint.position;
        if (_data.kkPos.position.x < _cientific.transform.position.x) _bullet.transform.up = Vector2.right;
        else _bullet.transform.up = -Vector2.right;
    }
}
