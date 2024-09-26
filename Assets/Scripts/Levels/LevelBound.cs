using Connection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBound : MonoBehaviour
{
    private enum TypeSetBound 
    {
        Auto,
        Manual
    }
    [SerializeField] private TypeSetBound typeSetBound;
    [SerializeField,Range(0,2)] private float scaler = 1f;
    [SerializeField] private Vector2 sizeBoundPos;//пределы по X 
    [SerializeField] private Vector2 sizeBoundNeg;// пределы по Y
    [SerializeField] private GameObject nodesContainer;

    public Vector2 SizeBoundPos { get => sizeBoundPos;  }
    public Vector2 SizeBoundNeg { get => sizeBoundNeg; }
    public void SetBounds() 
    {
        switch (typeSetBound)
        {
            case TypeSetBound.Auto:
                SetAutoClamp();
                break;
        }
    }
    
    private void SetAutoClamp() 
    {
        ColorNode[] _nodes = nodesContainer.GetComponentsInChildren<ColorNode>();
        float minX=float.MaxValue,maxX = float.MinValue,minY = float.MaxValue,maxY = float.MinValue;    
        for (int i = 0; i < _nodes.Length; i++)
        {
            if (_nodes[i].transform.position.x > maxX)
            {
                maxX = _nodes[i].transform.position.x;
            }
            if (_nodes[i].transform.position.x  < minX) 
            {
                minX = _nodes[i].transform.position.x;
            }
            if (_nodes[i].transform.position.y  > maxY)
            {
                maxY = _nodes[i].transform.position.y;
            }
            if (_nodes[i].transform.position.y < minY)
            {
                minY = _nodes[i].transform.position.y;
            }
        }

        sizeBoundPos.Set(maxX, maxY);
        sizeBoundPos *= scaler;
        sizeBoundNeg.Set(minX, minY);
        sizeBoundNeg *= scaler;
    }
  
}
