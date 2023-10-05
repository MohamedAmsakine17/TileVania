using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip _pickUpCoinSFX;
    [SerializeField] int _pointForCoinsPickUp = 100;

    bool _wasCollected = false;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"! && !_wasCollected)
        {
            _wasCollected = true;
            AudioSource.PlayClipAtPoint(_pickUpCoinSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddScore(_pointForCoinsPickUp);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
