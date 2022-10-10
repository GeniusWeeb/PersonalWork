using InterfaceApp;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{



    [ContextMenu("Ping")]
    public void PingThem()
    {
      List<GameObject>thisMany =   Find<GameObject>();
        foreach (var objects in thisMany)
        {   
            
            if (objects.TryGetComponent(out targetBase tar))
            {
                tar.PickMeUp();
                tar.IPing();
            }
            
            if (objects.TryGetComponent(out ItemBase it))
            {
                it.PickMeUp();
                it.IPing();
            }



          
        
        }
        
    }


    public static List<GameObject> Find<T>()  
    {

        List<GameObject> objectss =  new List<GameObject>();
       
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject item in rootGameObjects)
        {
            if (item.TryGetComponent(out IPickable pickable))
                objectss.Add(item);
                
        }
        return objectss;
    
    }






}
