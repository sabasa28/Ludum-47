using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    Animator animator;
    int shooAmount = 0;
    bool flying = false;
    [SerializeField] int shooesToScareOff = 0;
    int sideToAnim = 1;
    Clickable clickable;
    public enum Side
    { 
        Left,
        Right
    }
    public Side side;
    void Start()
    {
        FlyManager.Get().fliesLeft++;
        clickable = GetComponent<Clickable>();
        switch (side)
        {
            case Side.Left:
                sideToAnim = 1;
                break;
            case Side.Right:
                sideToAnim = 2;
                break;
        }
        animator = GetComponent<Animator>();
    }

    private void OnMouseEnter()
    {
        if (!flying && clickable.interactable)
        {
            shooAmount++;
            if (shooAmount <= shooesToScareOff)
            {
                flying = true;
                animator.SetTrigger("Shooed"+sideToAnim);
            }
            else
            {
                animator.SetTrigger("ScaredOff"+sideToAnim);
            }
        }
    }

    public void StopFlying()
    {
        flying = false;
    }

    public void LeaveScreen()
    {
        FlyManager.Get().OnFlyDead();
        gameObject.SetActive(false);
    }
}
