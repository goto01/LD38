﻿using Assets.Scripts.Core.PropertyAttributes;
using Assets.Scripts.Core.Staff;
using UnityEngine;

namespace Assets.Scripts.Components.Movement
{
    public abstract class BaseMovement : BindingMonoBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] [Binding(true)] protected Rigidbody2D _rigidbody2D;
        protected float _trueSpeed;
        protected int _delta = 1;

        protected abstract Vector2 Direction { get; }

        protected Vector2 Offset { get { return Direction * _speed  * Time.deltaTime; } }

        protected virtual void Awake()
        {
            _trueSpeed = _speed;
        }

        protected virtual void Update()
        {
            Translate();
        }

        protected virtual void Translate()
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + Offset * _delta);
        }
    }
}
