

using Unity.VisualScripting;
using UnityEngine;
using WrestleSimualtor;

public class FighterBehaviour : StateMachineBehaviour
{
   
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {    

       AssignState(animator, stateInfo);
    }



    void AssignState(Animator animator, AnimatorStateInfo stateInfo)
    {
        Player player = animator.gameObject.GetComponent<Player>();

       //note :-> //should be wrapped into 1 function -> better than 100 if else      
        if (stateInfo.IsName(GameConstants.idle))
        {

            player.UpdateState(CharacterState.Idle);

        }
        else if (stateInfo.IsName(GameConstants.atomicPunch))
        {
            Debug.Log("Atomic Punch");
            player.UpdateState(CharacterState.Striking);

        }
        else if (stateInfo.IsName(GameConstants.sumoStrike))
        {
             player.UpdateState(CharacterState.Striking);

        }    else if (stateInfo.IsName(GameConstants.cosmoShot))
        {
            Debug.Log("cosmo shot");
                player.UpdateState(CharacterState.Striking);
        }
        else if (stateInfo.IsName(GameConstants.grapple))
        {

            player.UpdateState(CharacterState.Grappling);
        }
        else if (stateInfo.IsName(GameConstants.block))
        {
            player.UpdateState(CharacterState.Blocking);
        }
        else if (stateInfo.IsName(GameConstants.GetBeatToSumo))
        {

            player.UpdateState(CharacterState.GetBeat);
        }
        else if (stateInfo.IsName(GameConstants.GetBeatToAtomic))
        {

            player.UpdateState(CharacterState.GetBeat);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
 
      Player player = animator.gameObject.GetComponent<Player>();
      //  if (stateInfo.IsName(GameConstants.idle)) player.localPlayerIsAnimating = false;
      
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        Player player = animator.gameObject.GetComponent<Player>();
        if (player.playerStat.myState == CharacterState.Idle) player.localPlayerIsAnimating = false;
        if (stateInfo.IsName(GameConstants.jump)) animator.ResetTrigger(GameConstants.jump);
        if (stateInfo.IsName(GameConstants.GetBeatToSumo) || stateInfo.IsName(GameConstants.GetBeatToAtomic) || stateInfo.IsName(GameConstants.GetBeatToCosmo))
                  player.localPlayerIsAnimating = false;


      

    }
    

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
