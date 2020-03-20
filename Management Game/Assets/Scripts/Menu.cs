using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private Button orcButton, humanButton;

    public void OnOrcButton()
    {
        SceneManager.LoadScene("OrcScene");
    }

    public void OnHumanButton()
    {
        SceneManager.LoadScene("HumanScene");
    }

    public void OnNOtYetImplementedButton()
    {
        throw new System.NotImplementedException();
    }
}
