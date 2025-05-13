using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftWire : MonoBehaviour
{
    public EWireColor WireColor { get; private set; }
    public bool isConnected { get; private set; }
    [SerializeField]
    private List<Image> wireImages;
    [SerializeField]
    private Image lightImage;
    [SerializeField]
    private RightWire connectedWire;
    [SerializeField]
    private RectTransform wireBody;

    private Canvas GameCanvas;
    void Start()
    {
        GameCanvas = FindAnyObjectByType<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void SetTarget(Vector3 targetPosition, float offset)
    {
        float angle = Vector2.SignedAngle(transform.position + Vector3.right - transform.position,
                targetPosition - transform.position);
        float distance = (Vector2.Distance(wireBody.transform.position, targetPosition) - offset);
        wireBody.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle));
        wireBody.sizeDelta = new Vector2(distance * (1 / GameCanvas.transform.localScale.x), wireBody.sizeDelta.y);
    }
    public void ResetTarget()
    {
        wireBody.localRotation = Quaternion.Euler(Vector3.zero);
        wireBody.sizeDelta = new Vector2(0.0f, wireBody.sizeDelta.y);
    }
    public void SetWireColor(EWireColor wireColor)
    {
        WireColor = wireColor;
        Color color = Color.black;
        switch (WireColor)
        {
            case EWireColor.Red:
                color = Color.red;
                break;
            case EWireColor.Blue:
                color = Color.blue;
                break;
            case EWireColor.Yellow:
                color = Color.yellow;
                break;
            case EWireColor.Magenta:
                color = Color.magenta;
                break;
        }

        foreach(var image in wireImages)
        {
            image.color = color;
        }
    }
    public void ConnectWire(RightWire rightWire)
    {
        if(connectedWire != null && connectedWire != rightWire)
        {
            connectedWire.DisconnectWire(this);
            connectedWire = null;
        }

        connectedWire = rightWire; 
        if(connectedWire.WireColor == WireColor)
        {
            lightImage.color = Color.yellow;
            isConnected = true;
        }
    }
    public void DisconnectWire()
    {
        if(connectedWire != null)
        {
            connectedWire.DisconnectWire(this);
            connectedWire = null;
        }
        lightImage.color = Color.gray;
        isConnected = false;
    }
}
