using System.Collections;
using UnityEngine;
using UnityEngine.UI;
 
public class FPSDisplay : MonoBehaviour
{
    #region Fields

    [Header("Components")]
    [SerializeField] private Text m_displayText = null;

    private int m_avgFrameRate;

    #endregion



    #region Unity Lifecycle

    IEnumerator Start ()
    {
        while (true) {
            if (Time.timeScale == 1) {
                yield return new WaitForSeconds (0.1f);
                m_avgFrameRate = (int)(1 / Time.deltaTime);
                m_displayText.text = "FPS :" + (Mathf.Round (m_avgFrameRate));
            } 
            
            yield return new WaitForSeconds (0.5f);
        }
    }

    #endregion
}