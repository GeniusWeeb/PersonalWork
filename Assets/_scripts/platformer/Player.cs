

using UnityEngine;


namespace Platformer
{
    public class Player : MonoBehaviour
    {
        private void Update()
        {
            this.transform.LookAt(Vector3.forward);
        }

    }

}
