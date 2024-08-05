using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena
using System.Collections;

public class PipeTransport : MonoBehaviour
{
    public float moveDistance = 5f; // Distancia que se moverá el jugador en el eje X
    public AudioClip moveSound;     // Clip de sonido que se reproducirá
    public string nextSceneName;    // Nombre de la escena a la que se cambiará

    private AudioSource audioSource;
    private bool isMoving = false;

    private void Awake()
    {
        // Inicializa el componente AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = moveSound;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isMoving)
        {
            // Comienza el proceso de mover al jugador y cambiar la escena
            StartCoroutine(MovePlayerAndChangeScene(collision.transform));
        }
    }

    private IEnumerator MovePlayerAndChangeScene(Transform playerTransform)
    {
        isMoving = true;

        // Reproduce el sonido
        audioSource.Play();

        // Mueve al jugador en el eje X
        Vector3 targetPosition = playerTransform.position + new Vector3(moveDistance, 0, 0);
        float elapsedTime = 0;
        float duration = 1f; // Duración del movimiento en segundos

        while (elapsedTime < duration)
        {
            playerTransform.position = Vector3.Lerp(playerTransform.position, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurar que el jugador llegue exactamente a la posición de destino
        playerTransform.position = targetPosition;

        // Espera a que el sonido termine
        yield return new WaitForSeconds(audioSource.clip.length);

        // Cambia a la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
