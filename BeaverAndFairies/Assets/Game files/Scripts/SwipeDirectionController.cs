using UnityEngine;
using System.Collections;

public class SwipeDirectionController {

	Vector3 _lastAcceleration;
	Vector3 _accelerationDelta;

	private const int mMessageWidth  = 200;
	private const int mMessageHeight = 64;

	private readonly Vector2 mXAxis = new Vector2(1, 0);
	private readonly Vector2 mYAxis = new Vector2(0, 1);

	private readonly string [] mMessage = {
		"",
		"Swipe Left",
		"Swipe Right",
		"Swipe Top",
		"Swipe Bottom"
	};

	public int mMessageIndex = 0;

	// The angle range for detecting swipe
	private const float mAngleRange = 30;

	// To recognize as swipe user should at lease swipe for this many pixels
	private const float mMinSwipeDist = 5.0f;

	// To recognize as a swipe the velocity of the swipe
	// should be at least mMinVelocity
	// Reduce or increase to control the swipe speed
	private const float mMinVelocity  = 2000.0f;

	private Vector2 mStartPosition;
	private float mSwipeStartTime;

	public SwipeDirectionController() {
		_lastAcceleration = new Vector3(0, 0, 0);
	}

	public void getTouchDirection()
	{

		// Mouse button down, possible chance for a swipe
		if (Input.GetMouseButtonDown(0)) {
			// Record start time and position
			mStartPosition = new Vector2(Input.mousePosition.x,
				Input.mousePosition.y);
			mSwipeStartTime = Time.time;
		}

		// Mouse button up, possible chance for a swipe
		if (Input.GetMouseButtonUp(0)) {
			float deltaTime = Time.time - mSwipeStartTime;

			Vector2 endPosition  = new Vector2(Input.mousePosition.x,
				Input.mousePosition.y);
			Vector2 swipeVector = endPosition - mStartPosition;

			float velocity = swipeVector.magnitude/deltaTime;

			if (velocity > mMinVelocity &&
				swipeVector.magnitude > mMinSwipeDist) {
				// if the swipe has enough velocity and enough distance

				swipeVector.Normalize();

				float angleOfSwipe = Vector2.Dot(swipeVector, mXAxis);
				angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;

				// Detect left and right swipe
				if (angleOfSwipe < mAngleRange) {
					OnSwipeRight();
				} else if ((180.0f - angleOfSwipe) < mAngleRange) {
					OnSwipeLeft();
				} else {
					// Detect top and bottom swipe
					angleOfSwipe = Vector2.Dot(swipeVector, mYAxis);
					angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
					if (angleOfSwipe < mAngleRange) {
						OnSwipeTop();
					} else if ((180.0f - angleOfSwipe) < mAngleRange) {
						OnSwipeBottom();
					} else {
						mMessageIndex = 0;
					}
				}
			}
		}

	}

	private void OnSwipeLeft() {
		mMessageIndex = 1;
	}

	private void OnSwipeRight() {
		mMessageIndex = 2;
	}

	private void OnSwipeTop() {
		mMessageIndex = 3;
	}

	private void OnSwipeBottom() {
		mMessageIndex = 4;
	}
}
