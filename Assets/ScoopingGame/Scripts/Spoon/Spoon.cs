using System.Collections.Generic;
using UnityEngine;

public enum SpoonState
{
    Idle,
    Moving,
    ScoopingDown,
    ScoopingUp,
    ScoopingReturn,
    Collecting
}
public class Spoon : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private Animator animator;
    public SpoonState currentState = SpoonState.Idle;
    private bool isScooping = true;
    void Update()
    {
        if (currentState == SpoonState.ScoopingReturn)
        {
            if (isScooping)
            {
                ScoopingGameController.instance.catchZone.ActiveCatchZone();
                isScooping = false;
            }
        }
    }
    public void Scoop()
    {
        animator.enabled = true;
    }
    public void AttachItems(List<BlindBag> items)
    {
        foreach (var item in items)
        {
            item.AttachToSpoon(holdPoint);
        }
    }
}
