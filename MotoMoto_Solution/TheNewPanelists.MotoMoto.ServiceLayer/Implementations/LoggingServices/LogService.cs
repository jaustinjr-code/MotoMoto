using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer.Implementations
{
    public class LogService
    {
        private readonly LogDataAccess? _logDataAccess;
        
        public LogService()
        {
            _logDataAccess = new LogDataAccess();
        }

        public bool CreateLog(DataStoreLog _dataStoreLog)
        {
            var dataStoreLog = new DataStoreLog()
            {
                _logId = _dataStoreLog._logId,
                _levelName = _dataStoreLog._levelName,
                _categoryName = _dataStoreLog._categoryName,
                _userId = _dataStoreLog._userId,
                _description = _dataStoreLog._description,
            };
            return _logDataAccess!.InsertNewLogEntity(dataStoreLog);
        }
    }
}
