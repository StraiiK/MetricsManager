using MetricsManagerUi.Requests;
using MetricsManagerUi.Responses;

namespace MetricsManagerUi.Client
{
    public interface IMetricsAgentClient
    {
        AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request);
        //AllDotNetMetricsApiResponse GetDotNetMetrics(GetAllDotNetMetricsApiRequest request);
        //AllNetworkMetricsApiResponse GetNetworkMetrics(GetAllNetworkMetricsApiRequest request);
        //AllRamMetricsApiResponse GetRamMetrics(GetAllRamMetricsApiRequest request);
        //AllRomMetricsApiResponse GetRomMetrics(GetAllRomMetricsApiRequest request);
    }
}
