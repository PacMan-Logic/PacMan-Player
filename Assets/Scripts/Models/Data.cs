using Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public static class Data { 
        public static int nowround = 0;
        public static int level = 0;
        public static bool portal = false;
        public static int pacman_score = 0;
        public static int ghost_score = 0;

        public static void Update(GameData gameData)
        {
            nowround = gameData.Round;
            level = gameData.level;
            portal = gameData.portal_available;

            pacman_score = gameData.score[0];
            ghost_score = gameData.score[1];
        }
    }

}
