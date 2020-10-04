﻿using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.LowLevel;

[RequireComponent(typeof(SpriteAnimator))]
public class PlayerAnimator : SerializedMonoBehaviour
{
    private SpriteAnimator _animator = null;
    private SpriteAnimator _shadowAnimator = null;
    private Player _player = null;

    [SerializeField, OnValueChanged(nameof(AnimationChange))]
    private AnimationState currentAnimation = AnimationState.Idle;
    
    public enum AnimationState
    {
        Idle,
        WalkLeft,
        WalkRight,
        WalkUp,
        WalkDown
    }

    public AnimationState CurrentAnimation
    {
        get => currentAnimation;
        set
        {
            currentAnimation = value;
            SetAnimation(value);
        }
    }

    private void AnimationChange()
    {
        SetAnimation(CurrentAnimation);
    }

    public Dictionary<AnimationState, AnimationContainer> animations = new Dictionary<AnimationState, AnimationContainer>();

    public void SetAnimation(AnimationState state)
    {
        _animator.sprites = animations[state];
        _shadowAnimator.sprites = animations[state];
    }
    
    IEnumerator Start()
    {
        yield return new WaitUntil(() =>
        {
            _player = GetComponent<Player>();
            return _player;
        });
        
        _animator = GetComponent<SpriteAnimator>();
        if (!_player.shadow.TryGetComponent(out _shadowAnimator))
        {
            _shadowAnimator = _player.shadow.AddComponent<SpriteAnimator>();
        }

        SetAnimation(CurrentAnimation);

        Player.Controls.Player.Move.performed += context =>
        {
            var input = context.ReadValue<Vector2>();
            if (input.y > 0)
            {
                CurrentAnimation = AnimationState.WalkUp;
            }

            if (input.y < 0)
            {
                CurrentAnimation = AnimationState.WalkDown;
            }
            if (input.x < 0)
            {
                CurrentAnimation = AnimationState.WalkRight;
            }
            if (input.x > 0)
            {
                CurrentAnimation = AnimationState.WalkLeft;
            }
        };
        
        Player.Controls.Player.Move.canceled += context =>
        {
            CurrentAnimation = AnimationState.Idle;
        };
    }
}