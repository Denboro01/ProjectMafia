using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{

    [SerializeField]
    private Text bulletCount;

    private void OnEnable()
    {
        PlayerController.PewPew += ammoCount;
        PlayerController.InitializePlayer += ammoCount;
    }

    private void OnDisable()
    {
        PlayerController.PewPew -= ammoCount;
        PlayerController.InitializePlayer -= ammoCount;
    }

    void ammoCount(int currentAmmo)
    {
        bulletCount.text = currentAmmo.ToString();
    }
}
