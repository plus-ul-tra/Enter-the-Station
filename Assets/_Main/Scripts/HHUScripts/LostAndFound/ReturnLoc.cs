using UnityEngine;

public class ReturnLoc : MonoBehaviour
{
    private GameObject interactUI;

    private void OnEnable()
    {
        interactUI = transform.GetChild(0).gameObject;
        interactUI.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interactUI != null)
            {
                interactUI.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interactUI != null)
            {
                interactUI.SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
