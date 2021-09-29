using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public GameObject firePoint;

    public GameObject attackPoint;

    public PickUpItem pickUpItem;

    // Update is called once per frame
    void Update()
    {
        if (pickUpItem.hasGun == true)
        {
            //Setting to Fire state
            attackPoint.SetActive(false);
            firePoint.SetActive(true);
        } else if (Input.GetButtonDown("Fire2"))
        {
            // Setting to Punch state
            firePoint.SetActive(false);
            attackPoint.SetActive(true);
        }
    }
}
