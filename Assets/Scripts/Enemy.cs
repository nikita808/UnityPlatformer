#region

using UnityEngine;

#endregion

public class Enemy : MonoBehaviour
{
    private static readonly int DeathID = Animator.StringToHash("Death");
    protected Animator Anim;
    protected Rigidbody2D Rb;

    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
    }

    public void JumpedOn()
    {
        Anim.SetTrigger(DeathID);
        Rb.velocity = Vector2.zero;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}