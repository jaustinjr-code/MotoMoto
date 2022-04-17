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
                LogId = _dataStoreLog.LogId,
                LevelName = _dataStoreLog.LevelName,
                CategoryName = _dataStoreLog.CategoryName,
                UserId = _dataStoreLog.UserId,
                Description = _dataStoreLog.Description,
            };
            return _logDataAccess!.InsertNewLogEntity(dataStoreLog);
        }
    }
}
