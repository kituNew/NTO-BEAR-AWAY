using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EducationController : MonoBehaviour
{
    [SerializeField] GameObject EducationProgramming;
    [SerializeField] GameObject EducationProgrammingUI;

    [SerializeField] GameObject EducationDoor;
    [SerializeField] GameObject EducationDoorUI;

    [SerializeField] GameObject EducationTest;
    [SerializeField] GameObject EducationTestUI;

    [SerializeField] GameObject robot1;
    [SerializeField] GameObject robot2;

    [SerializeField] GameObject startEducationText;
    Color col = Color.white;

    void Update()
    {
        if(startEducationText != null)
        {
            if(col.a > 0f)
            {
                col.a -= Time.deltaTime * 0.25f;
                startEducationText.GetComponent<Image>().color = col;
            }
            else
            {
                Destroy(startEducationText);
            }
        }
    }

    public void EducationProgrammingFunction()
    {
        if(robot1 != null)
        {
            robot1.GetComponent<Enemy_controller>().Bam();
        }
        if(robot2 != null)
        {
            robot2.SetActive(true);
            robot2.GetComponent<Enemy_controller>().Bam();
        }


        EducationProgramming.SetActive(false);
        EducationProgrammingUI.SetActive(false);
        EducationDoor.SetActive(true);
        EducationDoorUI.SetActive(true);
    }

    public void EducationDoorFunction()
    {
        EducationDoor.SetActive(false);
        EducationDoorUI.SetActive(false);
        EducationTest.SetActive(true);
        EducationTestUI.SetActive(true);
    }

    public void EducationTestFunction()
    {
        SceneManager.LoadScene(2);
    }
}
