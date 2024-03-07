using System;

using UnityEngine;


public class EventBus {
    public static event Action OnAttack;
    public static void Attack() => OnAttack?.Invoke();

    public static event Action OnJump;
    public static void Jump() => OnJump?.Invoke();

    public static event Action OnSprint;
    public static void Sprint() => OnSprint?.Invoke();

    public static event Action<Vector3> OnMove;
    public static void Move(Vector3 direction) => OnMove?.Invoke(direction);
}