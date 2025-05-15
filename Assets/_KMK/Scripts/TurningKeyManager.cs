using UnityEngine;
using UnityEngine.Events;

public class TurningKeyManager : Task
{
    public UnityEvent Initial;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        InitGame();
        Initial.Invoke();
    }
    public override void InitGame()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
