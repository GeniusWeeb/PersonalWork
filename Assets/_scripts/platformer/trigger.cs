using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{
  public class trigger : MonoBehaviour
    {
        private WaitForSeconds delay;


        private void Start()
        {
            delay = new WaitForSeconds(PlatformManager.instance.platformDelaySpeed);
        }

        private string player = "Player";
        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(player))
            {
              
                StartCoroutine(SpawnDelay());

            }


        }

        private IEnumerator SpawnDelay()
        {

            yield return delay;
            PlatformManager.instance.SpawnTile();
            yield return delay;
            this.transform.parent.gameObject.SetActive(false);



        }


    }
}
