public interface IUserManagementManager
{
    bool IsValidRequest(string operation, Dictionary<string, string> request);
}