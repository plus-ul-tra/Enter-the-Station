using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
public class RightWire : MonoBehaviour
{
    public EWireColor WireColor { get; private set; }
    public bool isConnected { get; private set; }
    [SerializeField]
    private List<Image> wireImages;
    [SerializeField]
    private Image lightImage;

    private List<LeftWire> connectedWires = new List<LeftWire>();
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

        foreach (var image in wireImages)
        {
            image.color = color;
        }
    }
    public void ConnectWire(LeftWire leftwire)
    {
        if (connectedWires.Contains(leftwire)){
            return;
        }
        connectedWires.Add(leftwire);
        if(connectedWires.Count ==1 && leftwire.WireColor == WireColor)
        {
            lightImage.color = Color.yellow;
            isConnected = true;
        }
        else
        {
            lightImage.color = Color.gray;
            isConnected = false;
        }
    }
    public void DisconnectWire(LeftWire leftwire)
    {
        connectedWires.Remove(leftwire); 
        if(connectedWires.Count == 1 && connectedWires[0].WireColor == WireColor)
        {
            lightImage.color = Color.yellow;
            isConnected = true;
        }
        else
        {
            lightImage.color = Color.gray;
            isConnected = false;
        }
    }
}
