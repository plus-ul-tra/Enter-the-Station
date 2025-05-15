using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    protected List<Node> adjacentNodes = new List<Node>(); // 인접한 노드들

    private List<Way> connectedWires = new List<Way>();

    public bool CanConnectTo(Node other)
    {
        return adjacentNodes.Contains(other);
    }

    public void RegisterAdjacentNode(Node node)
    {
        if (!adjacentNodes.Contains(node))
        {
            adjacentNodes.Add(node);
        }
    }

    //public void ConnectWire(Way wire)
    //{
    //    if (!connectedWires.Contains(wire))
    //    {
    //        connectedWires.Add(wire);
    //    }
    //}

    //public void DisconnectWire(Way wire)
    //{
    //    if (connectedWires.Contains(wire))
    //    {
    //        connectedWires.Remove(wire);
    //    }
    //}

    //public void ClearAllConnections()
    //{
    //    connectedWires.Clear();
    //}

    public List<Node> GetAdjacentNodes()
    {
        return adjacentNodes;
    }
}
