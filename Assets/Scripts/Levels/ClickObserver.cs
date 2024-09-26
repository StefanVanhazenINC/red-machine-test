using System;
using Connection;
using Events;
using Player.ActionHandlers;
using UnityEngine;

namespace Levels
{
    public class ClickObserver : MonoBehaviour
    {
        [SerializeField] private ColorConnectionManager colorConnectionManager;
        
        private ClickHandler _clickHandler;

        private void Awake()
        {
            _clickHandler = ClickHandler.Instance;


            
            _clickHandler.SetDragEventHandlers(colorConnectionManager.OnDragStart, colorConnectionManager.OnDragEnd, colorConnectionManager.OnDrag);

            _clickHandler.PointerDownEvent += OnPointerDown;
            _clickHandler.PointerUpEvent += OnPointerUp;


            _clickHandler.DragEvent += CameraMover.Instance.SetDirectionCamera;
            CameraMover.Instance.RefreshCamera();


        }

        private void OnDestroy()
        {

           
            _clickHandler.PointerDownEvent -= OnPointerDown;
            _clickHandler.PointerUpEvent -= OnPointerUp;
            _clickHandler.ClearEvents();

        }
        
        private void OnPointerDown(Vector3 position)
        {
            colorConnectionManager.TryGetColorNodeInPosition(position, out var node);

            if (node != null)
                EventsController.Fire(new EventModels.Game.NodeTapped());
            else
                CameraMover.Instance.StartMove(position);
        }
        private void OnPointerUp(Vector3 position)
        {
            EventsController.Fire(new EventModels.Game.PlayerFingerRemoved());
            CameraMover.Instance.EndMove();
        }
    }
}