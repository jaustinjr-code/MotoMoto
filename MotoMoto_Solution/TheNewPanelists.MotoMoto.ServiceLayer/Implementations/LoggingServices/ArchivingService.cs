using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data;
using System;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class ArchivingService
    {
        private readonly ArchivingDataAccess _archivingDataAccess;
        private readonly string _directoryString;

        public ArchivingService(string directoryString)
        {
            _archivingDataAccess = new ArchivingDataAccess();
            _directoryString = directoryString;
        }

        public void GenerateArchivingProcess()
        {
            DateTime dateTime = DateTime.Now;
            _archivingDataAccess.BuildArchiveTable(dateTime);
            _archivingDataAccess.LoadCSVDataIntoThirtyDayOldArchiveTable(_directoryString, dateTime);
        }
    }
}
