

using DG.Tweening.Plugins.Options;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using WrestleSimualtor;

public class Player : MonoBehaviour
{
    public string playerName;
    public Animator myAnimator;
    public CharacterStats playerStat;
    public GameObject playerShield;
    public LayerMask avoidHitBoxCollision;
    public bool localPlayerIsAnimating = false;
    private Rigidbody playerBody;
    public float ForceAmount;
    public float jumpForce;
    public TMP_Dropdown moveSelectionDropDown;
    private string moveToPlay;
    public GameObject localHitBox;


    #region string readonly

    private readonly int sumoStrike = Animator.StringToHash(GameConstants.sumoStrike);
    private readonly int atomicPunch = Animator.StringToHash(GameConstants.atomicPunch);
    private readonly int cosmoShot = Animator.StringToHash(GameConstants.cosmoShot);
    private readonly int jump = Animator.StringToHash(GameConstants.jump);
    private readonly int walk = Animator.StringToHash("Walk");
    private readonly string playerTag = "target";

    #endregion



    public GameObject colliderMaterial;

    [Header("Movements")]
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode jumping;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerStat = new CharacterStats(CharacterState.Idle, StrikeMoves.None);
        playerBody = GetComponent<Rigidbody>();

        colliderMaterial.GetComponent<Renderer>().sharedMaterial.color = Color.green;
        var color = colliderMaterial.GetComponent<Renderer>().sharedMaterial.color;
        color.a = 0.100f;
        colliderMaterial.GetComponent<Renderer>().sharedMaterial.color = color;


    }


    public void UpdateState(CharacterState currentState)
    {

        playerStat.myState = currentState;

        switch (playerStat.myState)

        {
            case CharacterState.Idle:
                playerStat.myStrikeorGrappleMove = StrikeMoves.None;
                break;
            case CharacterState.Striking:
                break;

            case CharacterState.Grappling:
                break;

        }

        //can perform further checks;
    }


    #region Movement



    #endregion

    private void FixedUpdate()
    {

        if (Input.GetKeyDown(moveLeft))
        {

            playerBody.AddForce(Vector3.forward * ForceAmount);
            myAnimator.SetTrigger(walk);

        }

        if (Input.GetKeyDown(moveRight))
        {
            playerBody.AddForce(- Vector3.forward  * ForceAmount);
            myAnimator.SetTrigger(walk);
        }
        if (Input.GetKeyDown(moveUp))

        {

            playerBody.AddForce(Vector3.right * ForceAmount);
            myAnimator.SetTrigger(walk);

        }

        if (Input.GetKeyDown(moveDown))
        {

            playerBody.AddForce(-Vector3.right * ForceAmount);
            myAnimator.SetTrigger(walk);

        }
        if (Input.GetKeyDown(jumping))
        {
            
         //   playerBody.AddForce(Vector3.up * jumpForce);

            myAnimator.SetTrigger(jump);

        }


    }


    // can be wrapped into 1 funciton
    #region moves



     private void TriggerMove(string move)
    {
        if (move == null) return;
        localPlayerIsAnimating = true;
        switch (move)
        {
            case GameConstants.sumoStrike:
                playerStat.myStrikeorGrappleMove = StrikeMoves.SumoSlap;
                myAnimator.SetTrigger(sumoStrike);
                break;
            case GameConstants.atomicPunch:
                playerStat.myStrikeorGrappleMove = StrikeMoves.AtomicPunch;
                myAnimator.SetTrigger(atomicPunch);
                break;
            case GameConstants.rektShot:
                break;
            case GameConstants.cosmoShot:
                playerStat.myStrikeorGrappleMove = StrikeMoves.CosmoShot;
                myAnimator.SetTrigger(cosmoShot);
                break;
            case GameConstants.dolphinDive:
                break;
            case GameConstants.GetBeatToSumo:
                playerStat.myState = CharacterState.GetBeat;
                myAnimator.SetTrigger("BeatToSumo");
                break;
            case GameConstants.GetBeatToAtomic:
                playerStat.myState = CharacterState.GetBeat;
                myAnimator.SetTrigger("BeatToAtomic");
                break;
            case GameConstants.GetBeatToCosmo:
                playerStat.myState = CharacterState.GetBeat;
                myAnimator.SetTrigger("BeatToCosmo");
                break;
        }
    }


    #endregion



    public void OnCollisionEnter(Collision collision)
    {
        OnplayerDetect();

        if (localPlayerIsAnimating && playerStat.myState == CharacterState.GetBeat) return;
        if (collision.gameObject.CompareTag(playerTag) && collision.gameObject != this.gameObject)
        {

            CharacterStats opponentPlayer = collision.gameObject.GetComponent<Player>().playerStat;
            ClashOutCome clashoutcomeResult = WrestlngSimManager.Instance.ClashOutComeDecider(playerStat, opponentPlayer);

            if (clashoutcomeResult.result) // WIN THE CLASH
            {
              
                Debug.Log("Contact count is " + collision.contactCount);
                foreach (ContactPoint contact in collision.contacts)
                {
                   
                    WrestlngSimManager.Instance.SpawnEffect (contact.point , clashoutcomeResult.mode);
                 }
               this.localPlayerIsAnimating = true;        
            }
            else //LOSE
            {   
                this.localPlayerIsAnimating = true;
                foreach (ContactPoint contact in collision.contacts)
                {
                    WrestlngSimManager.Instance.SpawnEffect(contact.point , clashoutcomeResult.mode);
                }
                if (clashoutcomeResult.mode == PowerMode.Same) return;
                


                //get beat animation -> lose
                switch (opponentPlayer.myStrikeorGrappleMove) {

                    case StrikeMoves.SumoSlap:                    
                        TriggerMove(GameConstants.GetBeatToSumo);
                        break;
                    case StrikeMoves.AtomicPunch:                      
                        TriggerMove(GameConstants.GetBeatToAtomic);
                        break;
                    case StrikeMoves.CosmoShot:                      
                        TriggerMove(GameConstants.GetBeatToCosmo);
                        break;
                }
            }
        }

    }


    private void OnplayerDetect() {

        colliderMaterial.GetComponent<Renderer>().sharedMaterial.color = Color.red;
        var color = colliderMaterial.GetComponent<Renderer>().sharedMaterial.color;
        color.a = 0.100f;
        colliderMaterial.GetComponent<Renderer>().sharedMaterial.color = color;
    }


    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag) && collision.gameObject != this.gameObject)
        {
            colliderMaterial.GetComponent<Renderer>().sharedMaterial.color = Color.green;

            var color = colliderMaterial.GetComponent<Renderer>().sharedMaterial.color;
            color.a = 0.100f;
            colliderMaterial.GetComponent<Renderer>().sharedMaterial.color = color;
        }
    }


    public void ValueChangedDropDown()
    {


        switch (moveSelectionDropDown.value)
        {   

            case 0:
                moveToPlay = "SumoStrike";
                break;
            case 1:
                moveToPlay = "AtomicPunch";
                break;
            case 2:
                moveToPlay = "RektShot";
                break;
            case 3:
                moveToPlay = "CosmoShot";
                break;
            case 4:
                moveToPlay = "DolphinDive";
                break;

        }


       
    }


    //---------------------------------
    //main UI move player
    public void PlayMove()
        {

    
        switch (moveToPlay) 
            {

            case "SumoStrike":
                TriggerMove(GameConstants.sumoStrike);
                break;
            case "AtomicPunch":
                TriggerMove(GameConstants.atomicPunch);
                break;
            case "RektShot":
                TriggerMove(GameConstants.rektShot);
                break;
            case "CosmoShot":
                TriggerMove(GameConstants.cosmoShot);
                break;
            case "DolphinDive":
                TriggerMove(GameConstants.dolphinDive);
                break;

            default:
                TriggerMove(GameConstants.sumoStrike);
                break;
              }

        }




}




[Serializable]
public class CharacterStats
{
    public CharacterState myState;  // diff for every character
    public StrikeMoves myStrikeorGrappleMove;



    public CharacterStats(CharacterState state , StrikeMoves move ) {
        myState = state;
        myStrikeorGrappleMove = move;

        }

}
