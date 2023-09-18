

using CartoonFX;
using System.Collections;
using UnityEngine;

namespace WrestleSimualtor
{
    public class WrestlngSimManager : MonoBehaviour
    {

    //All thye public should be [SerializeField] private 
    /I am just lazy

        public CharacterRuleSet Gamerules; 
        private static WrestlngSimManager _instance;
        public static WrestlngSimManager Instance => _instance;
     //   public GameObject hitEffect;
        public GameObject clashEffect;
        public GameObject strikeEffect;
        public GameObject powerEffect;
      

        #region Unity Methods  

            private void Awake()
            {
                _instance = this;
            }


            private void OnDisable()
            {
                    _instance = null;
            }

            

        #endregion



        public ClashOutCome  ClashOutComeDecider(CharacterStats localPlayerState , CharacterStats OppPlayerState) 
        {
           
            ClashOutCome clashoutcome;
            foreach (CharacterMode rule in Gamerules.modes)
            {
                
                //check for draw cases
                if (localPlayerState.myState == rule.state )
                {

                    if (!rule.LoseAgainstThisState.Contains(OppPlayerState.myState) && !(rule.DrawAgainstThisState ==  OppPlayerState.myState) ) 
                        return new ClashOutCome(true, PowerMode.None);

                    if (localPlayerState.myState == CharacterState.Striking && OppPlayerState.myState == CharacterState.Striking)
                    {
                        
                        clashoutcome  = (Gamerules.ReturnWinnerStrike(localPlayerState.myStrikeorGrappleMove, OppPlayerState.myStrikeorGrappleMove));
                      
                      //  Debug.LogError(clashoutcome.result  +"and "+ clashoutcome.mode);
                        return (clashoutcome);                  
                    }

                    if (localPlayerState.myState == CharacterState.Grappling && OppPlayerState.myState == CharacterState.Grappling)
                    {
                       
                        return new ClashOutCome(false , PowerMode.Clash);
                    }
                  
                    //local player can perform the animation go ahead


                }
            }
                return new ClashOutCome(false , PowerMode.None);
                   
            //the return woould be who can complete their animation, the other
            ////player goes to an idle state where they are  vulnerable to the attack 
        }

     

        public void SpawnEffect(Vector3 contactPosition , PowerMode powerMode)
        {
            GameObject spawnEffect;


            switch (powerMode)
            {
                case PowerMode.Power:
                    spawnEffect = Instantiate(powerEffect, transform.position, transform.rotation);
                    spawnEffect.transform.position = contactPosition;
                    
                      StartCoroutine(Delay(spawnEffect));
                    break;
                case PowerMode.Same:
                    spawnEffect = Instantiate(clashEffect, transform.position, transform.rotation);
                    spawnEffect.transform.position = contactPosition; 
                    
                    StartCoroutine(Delay(spawnEffect));
                    break;
                case PowerMode.Strike:
                     spawnEffect = Instantiate(strikeEffect, transform.position, transform.rotation);
                     spawnEffect.transform.position = contactPosition;
                  
                      StartCoroutine(Delay(spawnEffect));
                    break;

            }
            
             

            }

        IEnumerator Delay(GameObject effect) {

            yield return new WaitForSeconds(1.5f);
            Destroy(effect.gameObject);
        }
      

    }


    public enum StrikeMoves
    {
        SumoSlap , 
        CosmoShot , 
        RektShot ,
        DolphinDive,
        AtomicPunch,
        None
    
    }

    public enum CharacterState 
    
   {
        Idle , Blocking , Grappling , Striking  , GetBeat , None

    }


    public enum PowerMode
    { 
            Strike  , Power , Unblockable , Clash , None , Same
    }

    public class ClashOutCome {

        public bool result;
        public PowerMode mode;
       

        public ClashOutCome(bool result , PowerMode mode) {

            this.result = result;
            this.mode = mode; 
        }
    
    }





}
