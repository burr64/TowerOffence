using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private BotController botPrefab;
    [SerializeField] private PathSystem pathSystem;

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        BotController bot = Instantiate(botPrefab);
        bot.Init(pathSystem.BuildPath());
    }
}
