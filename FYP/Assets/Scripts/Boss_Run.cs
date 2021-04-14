using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    [SerializeField] float speed = 1.5f;
    [SerializeField] float attackRange = 2.4f;
    [SerializeField] float offset = 2f;
    Transform player;
    Rigidbody2D bossRB;
    Boss boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossRB = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.FlipSprite();

        PlayerPosition();

        Vector2 target = new Vector2(PlayerPosition(), bossRB.position.y);
        Vector2 newPos =  Vector2.MoveTowards(bossRB.position, target, speed * Time.fixedDeltaTime);
        bossRB.MovePosition(newPos);
        if (Vector2.Distance(player.position, bossRB.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

    public float PlayerPosition()
    {
        if (player.position.x >= boss.transform.position.x)
        {
            return player.position.x - offset;
        } 
        else
        {
            return player.position.x + offset;
        }
    }
}
