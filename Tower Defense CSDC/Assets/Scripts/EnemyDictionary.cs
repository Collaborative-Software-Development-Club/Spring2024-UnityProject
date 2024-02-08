using UnityEngine;
using System.Collections;

public class EnemyDictionary : MonoBehaviour
{
    public GameObject Instance;
    public IDictionary dictionary;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
    }

}
