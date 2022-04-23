using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class LogService
    {
        private readonly LogDataAccess? _logDataAccess;
        
        public LogService()
        {
            _logDataAccess = new LogDataAccess();
        }

        public bool CreateLog(LogEntryModel logEntryModel)
        {
            var dataStoreLog = new DataStoreLog()
            {
                _userId = logEntryModel._userId,
                _username = logEntryModel._username,
                _levelName = logEntryModel._levelName,
                _categoryName = logEntryModel._categoryName,
                _description = logEntryModel._logDescription,

            };
            return _logDataAccess!.InsertNewLogEntity(dataStoreLog);
        }
    }
}
