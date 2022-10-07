using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{

    public class Platform : MonoBehaviour
    {
        private Renderer rend;
        public GameObject TriggerNewTileLocation;  // serve as the trigger for new tile location / instant
        public bool flowFlag;
       
        private void Start()
        {
            rend = GetComponent<Renderer>();

            
        }

        private void OnEnable()
        {
            flowFlag = true;
        }

        private void OnDisable()
        {
            flowFlag = false;
            transform.position = PlatformManager.instance.defaultTilePosition;
        
        }

      
        private void Update()
        {
            if (flowFlag) 
           this.transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * PlatformManager.instance.tileSpeed);

        }
    

    }

                 
}