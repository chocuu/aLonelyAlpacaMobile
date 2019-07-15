using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanButtonController : MonoBehaviour
{
    public GameObject camera;
    private Transform cameraTransform;
    private Vector3 moveX;
    private Vector3 moveZ;
    private bool isPanning;
    // Start is called before the first frame update
    void Start() {
        isPanning = false;
        moveX = new Vector3(1.0f, 0, 0);
        moveZ = new Vector3(0, 0, 1.0f);
        cameraTransform = camera.GetComponent<Transform>();
    }

    /**
     * Move the camera in direction specified (called by WorldScript)

     * @param {dir} Direction to move camera, based on quadrants (below)
     *  -----------
	 * |  0  |  1  |
	 * |-----------
	 * |  3  |  2  |
	 *  -----------
     */
    public void MoveCamera(int dir) {
        Vector3 finalPosition;
        switch(dir){
            case 0 :
                finalPosition = cameraTransform.position - moveX;
                cameraTransform.position = finalPosition;
                break;
            case 1 :
                finalPosition = cameraTransform.position + moveZ;
                cameraTransform.position = finalPosition;
                break;
            case 2 :
                finalPosition = cameraTransform.position + moveX;
                cameraTransform.position = finalPosition;
                break;
            case 3 :
                finalPosition = cameraTransform.position - moveZ;
                cameraTransform.position = finalPosition;
                break;
            default : break;
        }
    }

    /**
     * Called when pan button is clicked
     */
    public void panButtonClicked(){
        this.setIsPanning(!isPanning);
    }

    public bool getIsPanning() { return isPanning; }

    /*
     * Setter method, also needs to change whether the camera is locked
     * to alpaca or not
     */
    public void setIsPanning(bool set) {
        this.isPanning = set;
        camera.GetComponent<FollowAlpaca>().enabled = !isPanning;
    }
}
