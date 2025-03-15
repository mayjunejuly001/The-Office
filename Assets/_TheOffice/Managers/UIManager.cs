using JetBrains.Annotations;
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


    public void CharacterTookdamage(GameObject character , int damageReceived)
    {

        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damageReceived.ToString();
    }
    public void CharacterHealed(GameObject character , int healthrestored)
    {

        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = healthrestored.ToString();
    }
        
}
