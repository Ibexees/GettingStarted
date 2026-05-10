using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class Sign : MonoBehaviour
{

    [SerializeField] private GameObject diaglogBox;
    [SerializeField] private LocalizedString signOneText;
    private InputAction interactAction;
    private bool canInteract = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.interactAction = InputSystem.actions.FindAction("Attack");
        this.interactAction.performed += ToggleDialogBox;

        this.diaglogBox.SetActive(false);
        this.canInteract = false;
    }

    private void ToggleDialogBox(InputAction.CallbackContext cxt)
    {
        if (this.canInteract)
        {
            if (this.diaglogBox.activeInHierarchy)
            {
                this.diaglogBox.SetActive(false);
            }
            else
            { 
                this.diaglogBox.SetActive(!this.diaglogBox.activeInHierarchy);
                var uiDocument = this.diaglogBox.GetComponent<UIDocument>();
                var label = uiDocument.rootVisualElement.Q<Label>();
                label.text = this.signOneText.GetLocalizedString();
            
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        this.canInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {

        if (!other.CompareTag("Player")) return;
        this.canInteract = false;
        this.diaglogBox.SetActive(false);
    }
}
