using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDIrAtk : StateMachineBehaviour
{
   
    Boss boss;
    float cooldown = 3f;
    float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        /*boss.FlipSprite();*/
        boss.DirectAttack();
        if (timer > cooldown)
        {
            boss.GetComponent<Animator>().SetTrigger("DirTImer");
            timer = 0;
        }
    }
}
