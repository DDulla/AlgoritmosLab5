using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public CustomCharacterSelectionList<string> characterList;
    public TextMeshProUGUI characterStatus;
    public Image characterImageDisplay;

    public Sprite Ryu;
    public Sprite ChunLi;
    public Sprite Ken;
    public Sprite Cammy;

    public RectTransform selectionPanel;

    // Posiciones configurables desde el Inspector
    public Vector2 startPosition;
    public Vector2 targetPosition;
    public float animationDuration = 0.5f;
    public float panelVisibleTime = 1.5f;

    private void Start()
    {
        characterList = new CustomCharacterSelectionList<string>();

        // Agregamos los personajes a la lista circular
        characterList.Add("Ryu");
        characterList.Add("Chun-Li");
        characterList.Add("Ken");
        characterList.Add("Cammy");

        characterList.InitializeSelection();

        // Aqui actualizamos la interfaz
        UpdateCharacterStatus();
        UpdateCharacterImage();

        // Establecer la posición inicial del panel de seleccion
        selectionPanel.anchoredPosition = startPosition;
    }

    // Logica del boton next
    public void OnNextButtonClicked()
    {
        characterList.SelectNext();
        UpdateCharacterStatus();
        UpdateCharacterImage();
    }

    // Logica del boton previous
    public void OnPreviousButtonClicked()
    {
        characterList.SelectPrevious();
        UpdateCharacterStatus();
        UpdateCharacterImage();
    }

    // Logica del boton de seleccion con animacion del panel
    public void OnSelectButtonClicked()
    {
        StartCoroutine(AnimateSelectionPanel());
    }

    private void UpdateCharacterStatus()
    {
        characterStatus.text = "Playing as " + characterList.GetCurrentSelection();
    }

    private void UpdateCharacterImage()
    {
        switch (characterList.GetCurrentSelection())
        {
            case "Ryu":
                characterImageDisplay.sprite = Ryu;
                break;
            case "Chun-Li":
                characterImageDisplay.sprite = ChunLi;
                break;
            case "Ken":
                characterImageDisplay.sprite = Ken;
                break;
            case "Cammy":
                characterImageDisplay.sprite = Cammy;
                break;
            default:
                break;
        }
    }

    private IEnumerator AnimateSelectionPanel()
    {
        float elapsedTime = 0;

        // Movimiento de entrada
        while (elapsedTime < animationDuration)
        {
            selectionPanel.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        selectionPanel.anchoredPosition = targetPosition;

        yield return new WaitForSeconds(panelVisibleTime); // Mantener el panel visible por el tiempo especificado

        // Movimiento de salida
        elapsedTime = 0;
        while (elapsedTime < animationDuration)
        {
            selectionPanel.anchoredPosition = Vector2.Lerp(targetPosition, startPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        selectionPanel.anchoredPosition = startPosition;
    }
}