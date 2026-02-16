using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviour
{
    public GameObject[] zombies;
    public GameObject selectedZombie;
    public Vector3 selectedSize;
    private int selectedIndex;

    private InputAction next, prev, push;

    public Vector3 pushForce;

    public TMP_Text timerText;
    private float timer;
    private void Start()
    {
        next = InputSystem.actions.FindAction("NextZombie");
        prev = InputSystem.actions.FindAction("PrevZombie");
        push = InputSystem.actions.FindAction("Jump");
        SelectZombie(0);
        
    }

    void SelectZombie(int index)
    {
        if (selectedZombie != null)
        {
            selectedZombie.transform.localScale = Vector3.one;

        }
        selectedZombie = zombies[index];
        selectedZombie.transform.localScale = selectedSize;
        Debug.Log("Selected Zombie"+selectedZombie);
    }
    // Update is called once per frame
    void Update()
    {
        if (next.WasPressedThisFrame())
        {
            Debug.Log("next Pressed Zombie");
            selectedIndex++;
            if (selectedIndex >= zombies.Length)
            {
                selectedIndex = 0;
            }
            SelectZombie(selectedIndex);
        }

        if (prev.WasPressedThisFrame())
        {
            Debug.Log("prev Pressed Zombie");
            selectedIndex--;
            if (selectedIndex < 0)
            {
                selectedIndex = zombies.Length - 1;
            }
            SelectZombie(selectedIndex);
        }

        if (push.WasPressedThisFrame())
        {
            Debug.Log("Pushed Zombie");
            Rigidbody rb = selectedZombie.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(pushForce);
            }
        }
        timer += Time.deltaTime;
        timerText.text = "Time:" + timer.ToString("0.0");


    }
}
