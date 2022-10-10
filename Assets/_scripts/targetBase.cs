using InterfaceApp;
using UnityEngine;

public class targetBase : MonoBehaviour , IPingableInterface , IPickable
{
    
    public virtual void IPing() { Debug.Log("I am pingAble" + this.gameObject.name);

        this.gameObject.SetActive(false);
    }
    public virtual void PickMeUp() { Debug.Log("I am pickable" + this.gameObject.name);
        this.gameObject.SetActive(false);
    }

}
