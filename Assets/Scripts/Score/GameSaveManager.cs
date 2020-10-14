using Assets.Scripts.Score;
using UnityEngine;

namespace Score
{
    public class GameSaveManager : MonoBehaviour
    {
        public bool LevelIsUnlocked(int levelStageIndex)
        {
            if (levelStageIndex == 1)
            {
                return true;
            }

            var starCount = GetLevelScore(levelStageIndex - 1);
            return starCount > 0;
        }

        public void SaveLevelScore(int levelScore, int levelStageIndex)
        {
            if (!ScoreLevelMap.Map.TryGetValue(levelStageIndex, out string levelStageName))
            {
                Debug.LogError($"Index {levelStageIndex} de fase não existente");
                return;
            }

            if (GetLevelScore(levelStageIndex) < levelScore)
            {
                PlayerPrefs.SetInt(levelStageName, levelScore);
            }
        }

        public int GetLevelScore(int levelStageIndex)
        {
            if (!ScoreLevelMap.Map.TryGetValue(levelStageIndex, out string levelStageName))
            {
                Debug.LogError($"Index {levelStageIndex} de fase não existente");
                return 0;
            }

            return PlayerPrefs.GetInt(levelStageName, 0);
        }
    }

}