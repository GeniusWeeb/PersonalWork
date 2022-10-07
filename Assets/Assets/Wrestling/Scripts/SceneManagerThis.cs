using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace WrestleSimualtor
{
    public class SceneManagerThis : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private Player player1;
        [SerializeField] private Player player2;

        public void ReloadThisScene()
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

        public void PlayBothPlayers()
        {

            player1.PlayMove();
            player2.PlayMove();
        }



        public void ToggleHitBox(bool value) 
        {
            player1.localHitBox.GetComponent<MeshRenderer>().enabled = value;
            player2.localHitBox.GetComponent<MeshRenderer>().enabled = value;
        
        }
    }
}
