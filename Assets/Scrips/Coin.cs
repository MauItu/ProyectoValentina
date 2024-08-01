using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int valor = 10; 
    public GameManager gameManager;
    public AudioClip coinSound; // Clip de sonido para la moneda

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.SumarPuntos(valor);

            // Reproduce el sonido de la moneda y destruye la moneda
            PlayCoinSound();
            Destroy(this.gameObject);
        }
    }

    private void PlayCoinSound()
    {
        // Crea un objeto temporal para el sonido
        GameObject soundObject = new GameObject("CoinSound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();

        // Configura el AudioSource
        audioSource.clip = coinSound;
        audioSource.playOnAwake = false;
        audioSource.Play();

        // Destruye el objeto de sonido después de que termine el clip
        Destroy(soundObject, coinSound.length);
    }
}
