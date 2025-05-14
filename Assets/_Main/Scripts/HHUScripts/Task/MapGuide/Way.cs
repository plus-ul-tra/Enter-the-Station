using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Way : MonoBehaviour
{
    public bool isConnected { get; private set; }
    [SerializeField]
    private List<Image> wireImages;
    [SerializeField]
    private Node connectedNode;
    //[SerializeField]
    private RectTransform way;
    private Canvas GameCanvas;
    void Start()
    {
        //GameCanvas = FindAnyObjectByType<Canvas>();
    }
    private void OnEnable()
    {
        way = GetComponent<RectTransform>();
        GameCanvas = FindAnyObjectByType<Canvas>();
    }
   
    
    public void SetTarget(Vector3 targetPosition, float offset)
    {
        float angle = Vector2.SignedAngle(transform.position + Vector3.right - transform.position,
                targetPosition - transform.position);
        float distance = (Vector2.Distance(way.transform.position, targetPosition) - offset);
        way.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle));
        way.sizeDelta = new Vector2(distance * (1 / GameCanvas.transform.localScale.x), way.sizeDelta.y);
    }

    //public void ResetTarget()
    //{
    //    way.localRotation = Quaternion.Euler(Vector3.zero);
    //    way.sizeDelta = new Vector2(0.0f, way.sizeDelta.y);
    //}
    public void ConnectNode(Node node)
    {
        if (connectedNode != null && connectedNode != node)
        {
            connectedNode.DisconnectWire(this);
            connectedNode = null;
        }

        connectedNode = node;
    }
    //public void DisconnectNode()
    //{
    //    if (connectedNode != null)
    //    {
    //        connectedNode.DisconnectWire(this);
    //        connectedNode = null;
    //    }
    //    isConnected = false;
    //}
}
