using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers instance => _instance; 
    private static Managers _instance;
    
    
    
    private void Awake()
    {
        _instance = this;
    }
}
