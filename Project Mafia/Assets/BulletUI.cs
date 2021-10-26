using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{

    [SerializeField]
    private Text bulletCount;

    private void OnEnable()
    {
        PlayerUITestScript.PewPew += ammoCount;
        PlayerUITestScript.InitializePlayer += ammoCount;
    }

    private void OnDisable()
    {
        PlayerUITestScript.PewPew -= ammoCount;
        PlayerUITestScript.InitializePlayer -= ammoCount;
    }

    void ammoCount(int currentAmmo)
    {
        bulletCount.text = currentAmmo.ToString();
    }
}
