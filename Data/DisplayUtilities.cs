using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESOCompanion.Utilities
{
    public class DisplayUtilities
    {
        public string GetRank(string StyleNameTextColor)
        {
            string colorBlueString = "color:#3A92FF";
            string colorPurpleString = "color:#A02EF7";
            string colorGoldString = "color:#CCAA1A";
            if (StyleNameTextColor == colorBlueString)
            {
                return "Superior";
            }
            else if (StyleNameTextColor == colorPurpleString)
            {
                return "Epic";
            }
            else if (StyleNameTextColor == colorGoldString)
            {
                return "Legendary";
            }
            else
            {
                return "";
            }
        }
        public int GetCompletedCount(StyleModel styleModel)
        {
            bool[] chapters = new bool[14] {
                styleModel.Axes,
                styleModel.Belts,
                styleModel.Boots,
                styleModel.Bows,
                styleModel.Chests,
                styleModel.Daggers,
                styleModel.Gloves,
                styleModel.Helmets,
                styleModel.Legs,
                styleModel.Maces,
                styleModel.Shields,
                styleModel.Shoulders,
                styleModel.Staves,
                styleModel.Swords };
            int trueCount = 0;
            foreach (bool chapter in chapters)
            {
                if (chapter)
                {
                    trueCount++;
                }
            }
            return trueCount;
        }
        public int CalculatePercentages(int trueCount)
        {
            int percent;
            int totalChapters = 14;
            percent = trueCount * 100 / totalChapters;
            return percent;
        }
        public bool isCompleted(StyleModel styleModel)
        {
            if (styleModel.Axes &&
            styleModel.Belts &&
            styleModel.Boots &&
            styleModel.Bows &&
            styleModel.Chests &&
            styleModel.Daggers &&
            styleModel.Gloves &&
            styleModel.Helmets &&
            styleModel.Legs &&
            styleModel.Maces &&
            styleModel.Shields &&
            styleModel.Shoulders &&
            styleModel.Staves &&
            styleModel.Swords)
            {
                //Completed = true;
                return true;
            }
            else
            {
                //Completed = false;
                return false;
            }
        }
    }
    public class CharacterDisplayUtilities
    {

    }
}
