// Assets/Scripts/ChangeButtonColorAndScene.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Boton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Color hoverColor = Color.green; // Color al pasar el mouse
    private Color originalColor; // Color original del botón
    private Image buttonImage; // Referencia al componente de imagen del botón

    public string sceneToLoad; // Nombre de la escena a cargar al hacer clic
    public bool isWinner; // Indica si el botón es ganador o no
    public GameObject winnerImage; // Imagen que se muestra si es ganador
    public GameObject loserImage; // Imagen que se muestra si no es ganador

    private bool isActive; // Indica si se está mostrando una imagen

    void Start()
    {
        // Obtener el componente de imagen del botón y guardar el color original
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;

        // Asegurarse de que las imágenes no se muestren al iniciar
        if (winnerImage != null) winnerImage.SetActive(false);
        if (loserImage != null) loserImage.SetActive(false);

        isActive = false;
    }

    // Método al pasar el mouse sobre el botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.color = hoverColor;
    }

    // Método al salir el mouse del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.color = originalColor;
    }

    // Método al hacer clic en el botón
    public void OnPointerClick(PointerEventData eventData)
    {
        // Mostrar la imagen correspondiente
        if (isWinner)
        {
            if (winnerImage != null)
            {
                winnerImage.SetActive(true);
                isActive = true;
            }
        }
        else
        {
            if (loserImage != null)
            {
                loserImage.SetActive(true);
                isActive = true;
            }
        }
    }

    void Update()
    {
        // Verificar si se debe cargar una escena o desactivar la imagen al presionar una tecla
        if (isActive && Input.anyKeyDown)
        {
            if (isWinner)
            {
                // Cargar la escena si es ganador
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                // Desactivar la imagen si no es ganador
                if (loserImage != null)
                {
                    loserImage.SetActive(false);
                }
                isActive = false;
            }
        }
    }
}
