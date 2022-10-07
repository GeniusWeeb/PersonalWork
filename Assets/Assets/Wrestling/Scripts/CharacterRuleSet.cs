
using System;
using System.Collections.Generic;
using UnityEngine;
namespace WrestleSimualtor
{
      //plug into the character -> reference the sim manager

    [CreateAssetMenu(fileName = "RuleDataBase")]
    public class CharacterRuleSet : ScriptableObject
    {
        StrikeDecider tempStrikeHolder1;
        StrikeDecider tempStrikeHolder2;
        public List<CharacterMode> modes;
        public List <StrikeDecider> strikeReferenceChecker;


        public ClashOutCome ReturnWinnerStrike (StrikeMoves strike1, StrikeMoves strike2)
        {


            foreach (var strike in strikeReferenceChecker)
            {
                if (strike1 == strike.moves)
                 {
                    tempStrikeHolder1 = new StrikeDecider(strike.StrikePriority,strike.PowerPriority); 
                 }

                if (strike2 == strike.moves)
                {
                   tempStrikeHolder2 = new StrikeDecider(strike.StrikePriority, strike.PowerPriority);
                 }       

            }
          

            if (tempStrikeHolder1.StrikePriority == tempStrikeHolder2.StrikePriority)
            {
                Debug.Log("Strike priority is same");
                 if (tempStrikeHolder1.PowerPriority == tempStrikeHolder2.PowerPriority) return new ClashOutCome(false, PowerMode.Same);
                  
                return new ClashOutCome((tempStrikeHolder1.PowerPriority == tempStrikeHolder2.PowerPriority ? false : tempStrikeHolder1.PowerPriority > tempStrikeHolder2.PowerPriority ? true : false), PowerMode.Power );
            }

            else
            {   
                Debug.Log("chekcing power priority");
                return new ClashOutCome ((tempStrikeHolder1.StrikePriority > tempStrikeHolder2.StrikePriority ? true : false) , PowerMode.Strike  );
            }
                           
        }


    }

    [Serializable]
    public class CharacterMode

    {
      [HideInInspector]  public string modeName; 
        public CharacterState state;
        public List<CharacterState> LoseAgainstThisState;
        public CharacterState DrawAgainstThisState;

  
    }

    [Serializable]
    public class StrikeDecider 
    {
        public string name;
        public StrikeMoves moves;
        public float StrikePriority;
        public float PowerPriority ;


        public   StrikeDecider(float StrikeP , float PowerP) {
            StrikePriority = StrikeP;
            PowerPriority = PowerP;
        
            }
        
    }



}




