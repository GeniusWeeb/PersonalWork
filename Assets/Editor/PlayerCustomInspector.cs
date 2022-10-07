
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player))]
public class PlayerCustomInspector : Editor
{

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        var playerManager = (Player)target;

        if (GUILayout.Button("SumoStrike"))
       //     playerManager.SumoStrike();      
        if (GUILayout.Button("Idle"))
         //   playerManager.Idle();
        if (GUILayout.RepeatButton("Block"))
        {
            playerManager.playerShield.SetActive(true);
           // playerManager.Block();

        } if (GUILayout.Button("AtomicPunch"))
        {
         //   playerManager.AtomicPunch();
        }
       

       
    }

}
