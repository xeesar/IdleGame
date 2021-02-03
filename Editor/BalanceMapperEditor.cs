using Editor;
using Models.GD;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameBalanceMapper))] //1
public class BalanceMapperEditor : UnityEditor.Editor
{
    #region Fields
    
    private GameBalanceMapper m_gameBalanceMaper;
    
    #endregion



    #region Unity Lifecycle

    private void OnEnable()
    {
        m_gameBalanceMaper = (GameBalanceMapper)target;
    }
    
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Get From Sheets"))
        {
            m_gameBalanceMaper.UpdateConfigs(() =>
            {
                m_gameBalanceMaper.RespectConfigs.ForEach(element => element.SaveData());
                m_gameBalanceMaper.DollarsConfig.ForEach(element => element.SaveData());
            });

        }
        GUILayout.EndHorizontal();
    }

    #endregion
}
