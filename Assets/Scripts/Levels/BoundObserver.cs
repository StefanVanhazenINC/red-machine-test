using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundObserver : MonoBehaviour
{
    [SerializeField] private LevelBound _levelBound;
    //ссылка на камераMove

    private void Awake() 
    {
        _levelBound.SetBounds();
        CameraMover.Instance.SetBound(_levelBound.SizeBoundPos, _levelBound.SizeBoundNeg);
    }

}
