using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleMan : Enemy
{
    enum State
    {
        Walk,
        Hide,
    }

    [SerializeField] float WalkTime = 1f,HideTime = 5f;
    float _walkTimer, _hideTimer;
    State _state = State.Walk;

    [SerializeField] float HideSpeed;
    BoxCollider2D _boxCollider2D;

    void Awake() {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _walkTimer = WalkTime;
        _hideTimer = HideTime;
    }

    public override void Update() {
        base.Update();
        switch (_state)
        {
            case State.Walk: {
                if(_walkTimer >= 0){
                    _walkTimer -= Time.deltaTime;
                }
                else{
                    Hide();
                }
            };
            break;
            case State.Hide: {
                if(_hideTimer >= 0f){
                    _hideTimer -= Time.deltaTime;
                }
                else{
                    Walk();
                }
            };
            break;
        }
    }

    void Hide() {
        _boxCollider2D.enabled = false;

        _state = State.Hide;

        _walkTimer = WalkTime;

        SetSpeed(HideSpeed);
        animator.SetBool("isRunning", false);
    }

    void Walk() {
        _boxCollider2D.enabled = true;

        _state = State.Walk;
        _hideTimer = HideTime;

        SetSpeed(MaxSpeed);
        animator.SetBool("isRunning", true);

        //TODO(eren): animasyonlar
    }

    public override void OnStunOver() {
        base.OnStunOver();
        Walk();
    }

    public override void OnStun() {
        base.OnStun();
        Hide();
    }

    public override void OnSlowDown() {
        if(_state == State.Walk)
            base.OnSlowDown();
        else
            Hide();
    }

    public override void OnSlowDownOver() {
        if(_state == State.Walk)
            base.OnSlowDownOver();
        else
            Hide();
    }
}
