using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public int weaponAmmo;
    public float fireRate;
    public int health;
    public bool isFood;

    public GameObject button;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            button.SetActive(false);
        }
    }
}
