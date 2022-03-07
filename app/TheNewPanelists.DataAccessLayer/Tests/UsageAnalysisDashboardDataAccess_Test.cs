using Xunit;
using System;
using TheNewPanelists.DataAccessLayer;
using MySql.Data.MySqlClient;

public class UsageAnalysisDashboardDataAccess_Test
{
    [Fact]
    public void IsMariaDBConnected()
    {
        // Given
        IDataAccess dataAccess = new UsageAnalysisDashboardDataAccess();
        // When
        bool result = dataAccess.EstablishMariaDBConnection();
        // Then
        Assert.True(result, "Connection not opened");
    }

    [Fact]
    public void IsSelectIndicatorDataReturned()
    {
        // Given
        string[] tables = { "ViewAnalytics", "AdmissionAnalytics", "CommunityBoardAnalytics", "EventListAnalytics" };
        //IDictionary<string, string[]> tables = { "ViewAnalytics", "AdmissionAnalytics", "CommunityBoardAnalytics", "EventListAnalytics" };
        //string[] indicators = { "displayTotal", "durationAvg", "loginTotal", "registrationTotal", "feedPostTotal", "eventRegistrationTotal" };
        string query;
        IDataAccess dataAccess;
        IList<MySqlDataReader?> readers = new List<MySqlDataReader?>();
        foreach (string table in tables)
        {
            //if (table.Equals("ViewAnalytics"))
            query = $"SELECT * FROM {table};";
            // Query is formed in the Service Layer, test only if DataReader is not null
            //query = $"SELECT * FROM {table} ORDER BY {} DESC LIMIT 5;";
            //query = $"SELECT displayTotal FROM {table} WHERE viewTitle LIKE \"View2\";";
            dataAccess = new UsageAnalysisDashboardDataAccess(query);
            MySqlDataReader? result = ((UsageAnalysisDashboardDataAccess)dataAccess).SelectIndicatorData();
            readers.Add(result);
        }
        // When
        // Then
        Assert.DoesNotContain(null, readers);
    }

    [Fact]
    public void IsSelectIndicatorUpdated()
    {
        // Given
        string query = $"UPDATE AdmissionAnalytics SET registrationTotal = registrationTotal + 1;";
        // Query is formed in the Service Layer, test only if rows were affected
        // When
        IDataAccess dataAccess = new UsageAnalysisDashboardDataAccess(query);
        bool result = ((UsageAnalysisDashboardDataAccess)dataAccess).UpdateIndicatorData();
        // Then
        Assert.True(result, "No rows were affected");
    }
}