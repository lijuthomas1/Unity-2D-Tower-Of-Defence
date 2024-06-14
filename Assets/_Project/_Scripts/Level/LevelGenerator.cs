using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerOfDefence.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private Transform gridParent;
        [SerializeField]
        private Transform pathParent;
        [SerializeField]
        private GameObject[] tilesList;

        // 0 - empty
        // 1 - grass
        // 2 - start
        // 3 - path
        // 4 - end
        private int[,] levelTilesArray =
        {
            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,3,0,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,0,1,0,1,3,0,0,0,3,1,1,1,1,1,1,1,1,1,0},
            {2,0,3,1,0,1,0,1,1,1,0,1,1,1,1,3,0,0,0,0,4},
            {0,1,1,1,0,1,3,0,3,1,0,1,1,1,1,0,1,1,1,1,0},
            {0,1,1,1,0,1,1,1,0,1,0,1,1,1,1,0,1,1,1,1,0},
            {0,1,1,1,3,0,0,0,3,1,3,0,0,0,0,3,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0}

        };

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
            while (gridParent.childCount > 0)
            {
                yield return null;
            }
            while (pathParent.childCount > 0)
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
                        
                        if(tileValue > 1)
                        {
                            tile.transform.SetParent(pathParent.transform);
                        }
                        else
                        {
                            tile.transform.SetParent(gridParent.transform);
                        }
                    }
                    tilePos.x += 1;
                }
                tilePos.x = -(initXPos - 0.5f);
                tilePos.y -= 1;
            }
        }

        public void ClearLevel()
        {
            while (gridParent.childCount > 0)
            {
                DestroyImmediate(gridParent.GetChild(0).gameObject);
            }
            while (pathParent.childCount > 0)
            {
                DestroyImmediate(pathParent.GetChild(0).gameObject);
            }
        }
    }
}
