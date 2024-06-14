using System.Collections;
using UnityEngine;
namespace TowerOfDefence.Level
{
    public class LevelGenarator : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] tilesList;
        private int[,] levelTilesArray =
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,0,1,0,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1},
            {0,0,0,1,0,1,0,1,1,1,0,1,1,1,1,0,0,0,0,0,0},
            {1,1,1,1,0,1,0,0,0,1,0,1,1,1,1,0,1,1,1,1,1},
            {1,1,1,1,0,1,1,1,0,1,0,1,1,1,1,0,1,1,1,1,1},
            {1,1,1,1,0,0,0,0,0,1,0,0,0,0,0,0,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}

        };

        //private int[,] levelTilesArray =
        //{
        //    {1,1,1,1,1},
        //    {0,0,0,1,1},
        //    {1,1,0,0,1},
        //    {1,1,1,0,1},
        //};
        private void Start()
        {
            print("Start ");
        }

        public void DoCreateLevel()
        {
            StartCoroutine(WaitForCreateLevel());
        }

        IEnumerator WaitForCreateLevel()
        {
            ClearLevel();
            while (transform.childCount > 0)
            {
                yield return null;
            }
            CreateLevel();
        }



        public void CreateLevel()
        {
            var initYPos = levelTilesArray.GetLength(0) / 2.0f;
            var initXPos = levelTilesArray.GetLength(1) / 2.0f;
            Vector3 tilePos = new Vector3(-(initXPos - 0.5f), (initYPos - 0.5f), 0);
            //print("initYPos " + initYPos + " initXPos " + initXPos);
            for (int i = 0; i < levelTilesArray.GetLength(0); i++)
            {
                for (int j = 0; j < levelTilesArray.GetLength(1); j++)
                {
                    //print("i " + i + " j " + j + " = " +levelTilesArray[i, j]);
                    var tileValue = levelTilesArray[i, j] - 1;
                    if (tileValue >= 0)
                    {
                        GameObject tile = Instantiate(tilesList[tileValue], tilePos, Quaternion.identity) as GameObject;
                        tile.transform.SetParent(this.transform);
                    }
                    tilePos.x += 1;
                }
                tilePos.x = -(initXPos - 0.5f);
                tilePos.y -= 1;
            }
        }

        public void ClearLevel()
        {
            while (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
    }
}
