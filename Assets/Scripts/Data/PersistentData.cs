using System.Collections.Generic;

namespace Data
{
    public class PersistentData
    {
        // For simplicity I have put all types of data here, in real project the data should be grouped in different classes
        
        public List<UnitInfo> PlayerUnits;
        public int Level;
        public string CurrentLanguage = "US";
        public int MoneyCount;
    }
}