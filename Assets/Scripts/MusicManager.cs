using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        // 这是一个单例模式，确保MusicManager只有一个实例
        if (instance == null)
        {
            // 如果这是第一个实例，将它赋值给instance
            instance = this;
            
            // 调用DontDestroyOnLoad，使这个GameObject在切换场景时不被销毁
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 如果已经存在一个MusicManager实例，就销毁这个新的（重复的）实例
            Destroy(gameObject);
        }
    }
}