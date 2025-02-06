using UnityEngine;

public class DestroyablePlatform : MonoBehaviour
{
    public void Kill()
    {
        Destroy(gameObject);
    }
}
