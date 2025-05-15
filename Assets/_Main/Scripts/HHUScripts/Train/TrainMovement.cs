using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public float trainSpeed = 0.0f;

    private void Update()
    {
        transform.position += Vector3.left * trainSpeed * Time.deltaTime;
        if(transform.position.x < -6)
        {
            transform.position = new Vector3 (60.0f, -12.070f, 0);
        }
    }
}
