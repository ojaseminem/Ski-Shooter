using UnityEngine;

namespace Gameplay.Managers
{
    public class SwipeManager : MonoBehaviour
    {
        public static bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
        private bool _isDragging = false;
        private Vector2 _startTouch, _swipeDelta;

        private void Update()
        {
            tap = swipeDown = swipeUp = swipeLeft = swipeRight = false;
            
            #region Standalone Inputs
            if (Input.GetMouseButtonDown(0))
            {
                tap = true;
                _isDragging = true;
                _startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                Reset();
            }
            #endregion

            #region Mobile Input
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    tap = true;
                    _isDragging = true;
                    _startTouch = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    _isDragging = false;
                    Reset();
                }
            }
            #endregion

            //Calculate the distance
            _swipeDelta = Vector2.zero;
            if (_isDragging)
            {
                if (Input.touches.Length < 0)
                    _swipeDelta = Input.touches[0].position - _startTouch;
                else if (Input.GetMouseButton(0))
                    _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
            }

            if (_swipeDelta.magnitude > 100)
            {
                float x = _swipeDelta.x;
                float y = _swipeDelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    //Left or Right
                    if (x < 0)
                        swipeLeft = true;
                    else
                        swipeRight = true;
                }
                else
                {
                    //Up or Down
                    if (y < 0)
                        swipeDown = true;
                    else
                        swipeUp = true;
                }
                Reset();
            }
        }

        private void Reset()
        {
            _startTouch = _swipeDelta = Vector2.zero;
            _isDragging = false;
        }
    }
}
