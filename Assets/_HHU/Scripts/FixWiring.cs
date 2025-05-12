using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum EWireColor
{
    None = -1,
    Red,
    Blue,
    Yellow,
    Magenta,
}
public class FixWiring : MonoBehaviour
{
    [SerializeField]
    private List<LeftWire> leftWires;
    [SerializeField]
    private List<RightWire> rightWires;

    private LeftWire selectWire;

    private void OnEnable()
    {
        List<int> numberPool = new List<int>();
        for(int i = 0; i < 4; i++)
        {
            numberPool.Add(i);
        }
        int index = 0;
        while(numberPool.Count != 0)
        {
            var number = numberPool[Random.Range(0, numberPool.Count)];
            leftWires[index++].SetWireColor((EWireColor)number);
            numberPool.Remove(number);
        }
        for (int i = 0; i < 4; i++)
        {
            numberPool.Add(i);
        }

        index = 0;
        while (numberPool.Count != 0)
        {
            var number = numberPool[Random.Range(0, numberPool.Count)];
            rightWires[index++].SetWireColor((EWireColor)number);
            numberPool.Remove(number);
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.right, 1.0f);
            if (hit.collider != null)
            {
                //Debug.Log("clicked!");
                var left = hit.collider.GetComponentInParent<LeftWire>();
                if (left != null)
                {
                    selectWire = left;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (selectWire != null)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(Input.mousePosition, Vector2.right, 1.0f);
                foreach (var hit in hits)
                {
                    if (hit.collider != null)
                    {
                        var right = hit.collider.GetComponentInParent<RightWire>();
                        if (right != null)
                        {
                            selectWire.SetTarget(hit.transform.position, 50.0f);
                            selectWire.ConnectWire(right);
                            right.ConnectWire(selectWire);
                            selectWire = null;
                            return;
                        }
                    }
                }


                selectWire.ResetTarget();
                selectWire.DisconnectWire();
                selectWire = null;
            }
        }

        if (selectWire != null)
        {
            selectWire.SetTarget(Input.mousePosition, 15.0f);
        }
    }
}
