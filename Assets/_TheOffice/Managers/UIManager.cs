
using TMPro;
using UnityEngine;



public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    public Canvas gameCanvas;
    private void Awake()
    {
        gameCanvas = FindAnyObjectByType<Canvas>();
        
    }

    private void Update()
    {
        OnExitGame();
    }


    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookdamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookdamage;
        CharacterEvents.characterHealed  -= CharacterHealed;
    }


    public void CharacterTookdamage(GameObject character, int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        // Enable gradient mode
        tmpText.enableVertexGradient = true;
        if (damageReceived ==  10)
        {
            tmpText.text = "Jira Ticket";
            // Pure blue gradient (same blue color in all corners)
            tmpText.colorGradient = new VertexGradient(
                 new Color32(30, 30, 255, 255),   // Slightly darker blue (Top Left)
            new Color32(0, 0, 255, 255),     // Pure Blue (Top Right)
            new Color32(0, 0, 255, 255),     // Pure Blue (Bottom Left)
            new Color32(80, 80, 255, 255)    // Slightly lighter blue (Bottom Right)
            );
        }
        else
        {
            tmpText.text = "F*ck You";
            // Pure yellow gradient (same yellow color in all corners)
            tmpText.colorGradient = new VertexGradient(
                 new Color32(255, 235, 0, 255),   // Slightly darker yellow (Top Left)
            new Color32(255, 255, 0, 255),   // Pure Yellow (Top Right)
            new Color32(255, 255, 0, 255),   // Pure Yellow (Bottom Left)
            new Color32(255, 255, 80, 255)
            );
        }
    }

    public void CharacterHealed(GameObject character , int healthrestored)
    {

        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = healthrestored.ToString();
    }

    public void OnExitGame()
    {
        if (InputManager.Escape == true) // Make sure the condition properly wraps the block
        {
            Debug.Log("Escape pressed");

#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name + ": " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif

#if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
            Application.Quit();
#elif (UNITY_WEBGL)
            SceneManager.LoadScene("QuitScene");
#endif
        }
    }

}
