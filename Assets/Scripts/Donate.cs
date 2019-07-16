using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DonateCompleted(UnityEngine.Purchasing.Product product)
    {
        Debug.Log("PURCHASE COMPLETE");
    }

    public void DonateFailed(UnityEngine.Purchasing.Product product, UnityEngine.Purchasing.PurchaseFailureReason failureReason)
    {
        Debug.Log("PURCHASE FAILED");
    }
}
