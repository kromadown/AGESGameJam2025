using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // 在Inspector中将包含Restart和Quit按钮的Panel拖到这里
    public GameObject menuPanel;

    void Start()
    {
        // 确保游戏开始时菜单是关闭的
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }
    }

    // 这个方法用于打开和关闭菜单
    public void ToggleMenu()
    {
        if (menuPanel != null)
        {
            bool isActive = menuPanel.activeSelf;
            menuPanel.SetActive(!isActive);
        }
    }

    // 这个方法将由“Restart”按钮调用
    public void RestartLevel()
    {
        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 这个方法将由“Quit”按钮调用
    public void QuitGame()
    {
        // 在编辑器模式下，Application.Quit()不起作用，所以我们打印一条日志来确认
        Debug.Log("Quitting game...");
        
        // 在构建好的游戏中，这会关闭程序
        Application.Quit();
    }
}
