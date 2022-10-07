using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{
    public class PlatformManager : MonoBehaviour
    {

        public static PlatformManager instance;

        private Camera _camera; 
        public Vector3 defaultTilePosition;
        [SerializeField] private Vector3 offsetPoistionForTile;
        [SerializeField] private GameObject platformPrefab;
        [SerializeField] private List<Material> materials;
        [SerializeField] private GameObject TileSpawnList;


        public float platformDelaySpeed;

        [Range(0,3)]
        public int countC;

        public float tileSpeed =  30f; 

        private void Awake()
        {
            instance = this; 
        }

        private void Start()
        {   
            
            _camera = Camera.main;
            SpawnTile();
        }

        [ContextMenu("Spawn tile")]
        public void SpawnTile()
        {
            ++countC;
            if (countC > 2)
            {
               
                NewSpawnChain();
                return;
            }
            GameObject spawnedTile = Instantiate(platformPrefab, transform.position, Quaternion.identity, TileSpawnList.transform);
            spawnedTile.transform.position = defaultTilePosition + offsetPoistionForTile;
         //  spawnedTile.GetComponent<Renderer>().sharedMaterial = materials[Random.Range(0, 2)];
           

        }

        public Camera GetCamera => _camera; 


        int CheckTileCount() {
            
            return TileSpawnList.transform.childCount ;
        }



        void NewSpawnChain()
        {
            foreach (Transform item in TileSpawnList.transform)
                if (!item.gameObject.activeInHierarchy)
                {
                    item.transform.position += offsetPoistionForTile;    
                    item.gameObject.SetActive(true);
                    break;
                
                }    
        
        }

     




    }

}