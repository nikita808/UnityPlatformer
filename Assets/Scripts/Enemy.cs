#region

using UnityEngine;

#endregion

public class Enemy : MonoBehaviour
{
    private static readonly int DeathID = Animator.StringToHash("Death");
    protected Animator Anim;

    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
    }

    public void JumpedOn()
    {
        Anim.SetTrigger(DeathID);
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}