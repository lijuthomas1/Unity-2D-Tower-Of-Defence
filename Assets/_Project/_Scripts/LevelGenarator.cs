using UnityEngine;
namespace TowerOfDefence.Level
{
    public class LevelGenarator : MonoBehaviour
    {
        [SerializeField]
        private GameObject tile1;


        private int[,] levelTilesArray =
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };
        private void Start()
        {
            print("Start ");
            //DoCreateLevel();
        }

        public void DoCreateLevel()
        {
            var initYPos = levelTilesArray.GetLength(0) / 2.0f;
            var initXPos = levelTilesArray.GetLength(1) / 2.0f;
            Vector3 tilePos = new Vector3(-(initXPos - 0.5f), -(initYPos - 0.5f), 0);
            print("initYPos " + initYPos + " initXPos " + initXPos);
            for (int i = 0; i < levelTilesArray.GetLength(0); i++)
            {
                for (int j = 0; j < levelTilesArray.GetLength(1); j++)
                {
                    GameObject tile = Instantiate(tile1, tilePos, Quaternion.identity) as GameObject;
                    tile.transform.SetParent(this.transform);
                    tilePos.x += 1;
                }
                tilePos.x = -(initXPos - 0.5f);
                tilePos.y += 1;
            }
        }
    }
}
