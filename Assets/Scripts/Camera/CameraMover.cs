using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.MonoBehaviourUtils;
using Utils.Singleton;

public class CameraMover : DontDestroyMonoBehaviourSingleton<CameraMover>
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 sizeBoundPos;//пределы по X 
    [SerializeField] private Vector2 sizeBoundNeg;// пределы по Y
    [SerializeField] private float saveIntertion = 0.3f;
    [SerializeField] private float threshold = 5;//пороговое значение между точками, 

    private Vector3 _startPosition;
    private Vector3 _direction;
    private Vector3 _targetPosition;
    private float _distance;
    private bool _isMove;
    private bool _blockMove = true;

    private Coroutine _inertionRoutine;
  
    public void SetBound(Vector2 positive,Vector2 negative) 
    {
        sizeBoundPos = positive;
        sizeBoundNeg = negative;
        Debug.Log(Vector2.Distance(sizeBoundPos, sizeBoundNeg));
        if (Vector2.Distance(sizeBoundPos, sizeBoundNeg) > threshold)
        {
            _blockMove = false;
        }
        else 
        {
            _blockMove = true;
        }
    }
    private void CheckBounds() 
    {
        float clampX = Mathf.Clamp(transform.position.x, sizeBoundNeg.x, sizeBoundPos.x);
        float clampY = Mathf.Clamp(transform.position.y, sizeBoundNeg.y, sizeBoundPos.y);
        transform.position= new Vector3(clampX, clampY, transform.position.z);
    }
    public void RefreshCamera() 
    {
        
        transform.position.Set(0, 0, transform.position.z);
    }
    private void Update()
    {
        if (_isMove && !_blockMove) 
        {
            MoveCamera();
            CheckBounds();
        }
      
    }
    public void MoveCamera() 
    {
       
        transform.position = Vector3.Lerp(transform.position, _targetPosition, moveSpeed * Time.deltaTime) ;
    }
    public void SetDirectionCamera(Vector3 direction) 
    {
        _distance = Vector2.Distance(_startPosition, direction);
        _direction = direction;
        _targetPosition = transform.position+((_startPosition - _direction).normalized) * _distance;
        _targetPosition.z = transform.position.z;
    }
    public void StartMove(Vector3 startPosition) 
    {
       
        if (_inertionRoutine != null)
        {
            Coroutines.Stop(_inertionRoutine);
        }
        _direction = Vector3.zero;
        _isMove = true;
        _startPosition = startPosition;
    }
    public void EndMove() 
    {
        _inertionRoutine = Coroutines.WaitForSeconds(saveIntertion, EndInertion);
    }
    private void EndInertion() 
    {
        _isMove = false;
    }
 

}
