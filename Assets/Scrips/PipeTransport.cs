using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena
using System.Collections;

public class PipeTransport : MonoBehaviour
{
    public Transform pipeEntry; // Punto de entrada de la tubería
    public float transportSpeed = 5f; // Velocidad de movimiento dentro de la tubería
    public AudioClip pipeSound; // Sonido de la tubería
    public string nextSceneName; // Nombre de la siguiente escena

    private AudioSource audioSource;
    private bool isTransporting = false;
    private Transform player;

    private void Awake()
    {
        // Inicializa el componente de audio
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = pipeSound;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTransporting)
        {
            // Comienza el proceso de transporte
            player = collision.transform;
            StartCoroutine(TransportPlayer());
        }
    }

    private IEnumerator TransportPlayer()
    {
        isTransporting = true;

        // Reproducir sonido de tubería
        audioSource.Play();

        // Mover al jugador hacia la entrada de la tubería
        while (Vector2.Distance(player.position, pipeEntry.position) > 0.1f)
        {
            player.position = Vector2.MoveTowards(player.position, pipeEntry.position, transportSpeed * Time.deltaTime);
            yield return null;
        }

        // Esperar a que termine el sonido de la tubería
        yield return new WaitForSeconds(audioSource.clip.length);

        // Cambiar a la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
