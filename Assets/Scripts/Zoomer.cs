using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour {
	/* The scene camera. */
	public Camera cam; 
	/* Transform of Pan Button, used to bring pan in at half zoom */
	private PanButtonController pan_ctrlr;
	private RectTransform panTransform;
	/* Amount to shift pan transform when making it visible/invisible */
	private Vector3 panPosInitial;
	private Vector3 panPosFinal;
	/* The size of a full-zoomed camera. */
	private float ZOOM_AMNT_FULL;
	/* The size of a zoomed camera at half-zoom. */
	private float ZOOM_AMNT_HALF;
	/* The size of a zoomed camera at no-zoom. */
	private float ZOOM_AMNT_NONE;
	/* The increase to the camera size that produces half-zoom. */
	public float zAmount_half;
	/* The increase to the camera size that produces no-zoom. */
	public float zAmount_none;
	/* How close the camera gets to the zoomed/non-zoomed thresholds. */
	private const float ZOOM_CLOSENESS = 0.0005f;
	/* The speed of the camera zoom. */
	private float ZOOM_SPEED = 0.5f;

	/* The background, which needs to get scaled. */
	public GameObject background;
	public GameObject background2;
	private float origBgScaleX;
	private float origBgScaleY;

	public float bgScaleAmnt;
	public float timer = 0;

	/* The current state of the zooming. */
	private enum ZoomState {
		ZOOMING_OUT,
		ZOOMING_IN,
		ZOOMED_IN,
		ZOOMED_OUT
	};
	private ZoomState zState;
	private float lerp_timer;

	void Start () {
		ZOOM_AMNT_FULL = cam.orthographicSize;
		ZOOM_AMNT_HALF = cam.orthographicSize + zAmount_half;
		ZOOM_AMNT_NONE = cam.orthographicSize + zAmount_none;
		zState = ZoomState.ZOOMED_IN;
		lerp_timer = 0;
		pan_ctrlr = GameObject.Find("Pan Butt").GetComponent<PanButtonController>();
		if(pan_ctrlr != null) panTransform = pan_ctrlr.GetComponent<RectTransform>();
		panPosInitial = new Vector3(-385, 40, 0);
		panPosFinal = new Vector3(-385, -40, 0);
	}

	/* Player toggled camera, update the state. */
	public void toggleZoom() {
		lerp_timer = 0;
		switch(zState) {
			case ZoomState.ZOOMING_OUT:
				zState = ZoomState.ZOOMING_IN;
				break;
			case ZoomState.ZOOMING_IN:
				zState = ZoomState.ZOOMING_OUT;
				break;
			case ZoomState.ZOOMED_IN:
				zState = ZoomState.ZOOMING_OUT;
				break;
			case ZoomState.ZOOMED_OUT:
				zState = ZoomState.ZOOMING_IN;
				break;
		}
	}

	/* Lerp the camera from [start] to [end]. */
	void moveCam(float start, float end) {
		cam.orthographicSize = Mathf.Lerp(start, end, lerp_timer);
		float scaleProp = 1 / cam.orthographicSize;
		background.GetComponent<ScaleBackground>().Scale2(scaleProp);
		background2.GetComponent<ScaleBackground>().Scale2(scaleProp);
	}

	/* The camera is close enough to its destination, so we can
	 * stop moving it. (It's within [ZOOM_CLOSENESS] of [end]). */
	bool camMoveDone(float start, float end) {
		return (Mathf.Abs(end - start) <= ZOOM_CLOSENESS);
	}

	/* Update the camera position and zoom state. */
	void resolveZoom() {
		switch(zState) {
			case ZoomState.ZOOMING_OUT:
				lerp_timer += ZOOM_SPEED * Time.deltaTime;
				moveCam(cam.orthographicSize, ZOOM_AMNT_HALF);
				if (camMoveDone(cam.orthographicSize, ZOOM_AMNT_HALF)) {
					zState = ZoomState.ZOOMED_OUT;
				}
				// Bring pan button down
				if(panTransform != null)
					panTransform.anchoredPosition = Vector3.Lerp(panTransform.anchoredPosition, panPosFinal, lerp_timer);
				break;
			case ZoomState.ZOOMING_IN:
				lerp_timer += ZOOM_SPEED * Time.deltaTime;
				moveCam(cam.orthographicSize, ZOOM_AMNT_FULL);
				if (camMoveDone(cam.orthographicSize, ZOOM_AMNT_FULL)) {
					zState = ZoomState.ZOOMED_IN;
				}
				// Bring pan button back up, disable panning if it was on
				if(panTransform != null)
					pan_ctrlr.setIsPanning(false);
					panTransform.anchoredPosition = Vector3.Lerp(panTransform.anchoredPosition, panPosInitial, lerp_timer);
				break;	
			case ZoomState.ZOOMED_IN:
				lerp_timer = 0;
				break;
			case ZoomState.ZOOMED_OUT:
				lerp_timer = 0;
				break;
		};
	}

	void Update() {
		resolveZoom();
	}
}
