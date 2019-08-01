using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donate : MonoBehaviour
{
    [SerializeField] GameObject thanksObj;
    // Start is called before the first frame update
    void Start()
    {
        thanksObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DonateCompleted(UnityEngine.Purchasing.Product product)
    {
        Debug.Log("PURCHASE COMPLETE");
        thanksObj.SetActive(true);
    }

    public void DonateFailed(UnityEngine.Purchasing.Product product, UnityEngine.Purchasing.PurchaseFailureReason failureReason)
    {
        Debug.Log("PURCHASE FAILED");
    }

    public void ThanksClosed()
    {
        thanksObj.SetActive(false);
    }
}
