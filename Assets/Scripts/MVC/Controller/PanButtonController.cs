using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** NOTE: Pan Button is brought into view by Zoomer.cs */
public class PanButtonController : MonoBehaviour
{
    private const float panSpeed = 15f;
    public Image panImage;
    public Sprite panSpriteSelected;
    public Sprite panSpriteUnselected;
    public AudioSource panClickSound;
    public GameObject panBorderA;
    public GameObject panBorderB;
    public GameObject panBorderC;
    private Transform cameraTransform;
    private FollowAlpaca follow_alpaca_script;
    private bool isPanning;

    private void SetPanBorder(bool set) {
        panBorderA.SetActive(set);
        panBorderB.SetActive(set);
        panBorderC.SetActive(set);
    }

    // Start is called before the first frame update
    void Start() {
        isPanning = false;
        GameObject camera_prefab = GameObject.FindWithTag("MainCamera");
		cameraTransform = camera_prefab.GetComponent<Transform>();
        follow_alpaca_script = camera_prefab.GetComponent<FollowAlpaca>();
        SetPanBorder(false);
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
                finalPosition = cameraTransform.position + panSpeed*Time.deltaTime*Vector3.left;
                cameraTransform.position = finalPosition;
                break;
            case 1 :
                finalPosition = cameraTransform.position + panSpeed*Time.deltaTime*Vector3.forward;
                cameraTransform.position = finalPosition;
                break;
            case 2 :
                finalPosition = cameraTransform.position + panSpeed*Time.deltaTime*Vector3.right;
                cameraTransform.position = finalPosition;
                break;
            case 3 :
                finalPosition = cameraTransform.position + panSpeed*Time.deltaTime*Vector3.back;
                cameraTransform.position = finalPosition;
                break;
            default : break;
        }
    }

    /**
     * Called when pan button is clicked
     */
    public void panButtonClicked(){
        panClickSound.Play();
        this.setIsPanning(!isPanning);
    }

    public bool getIsPanning() { return isPanning; }

    /*
     * Setter method, also needs to change whether the camera is locked
     * to alpaca or not
     */
    public void setIsPanning(bool set) {
        this.isPanning = set;
        follow_alpaca_script.enabled = !isPanning;
        panImage.sprite = (isPanning) ? panSpriteSelected : panSpriteUnselected;
        SetPanBorder(set);
    }
}
