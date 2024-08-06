using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MouseLevel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image imageToToggle; // Arrastra y suelta la imagen en el inspector
    public string sceneToLoad;  // El nombre de la escena que quieres cargar

    private void Start()
    {
        // Asegúrate de que la imagen está desactivada al inicio
        if (imageToToggle != null)
        {
            imageToToggle.enabled = false;
        }
    }

    // Este método se llama cuando el puntero entra en el área del botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (imageToToggle != null)
        {
            imageToToggle.enabled = true; // Activa la imagen
        }
    }

    // Este método se llama cuando el puntero sale del área del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        if (imageToToggle != null)
        {
            imageToToggle.enabled = false; // Desactiva la imagen
        }
    }

    // Este método se llama cuando se hace clic en el botón
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad); // Carga la escena especificada
        }
    }
}
