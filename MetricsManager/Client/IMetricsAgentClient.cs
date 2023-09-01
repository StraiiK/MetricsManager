using MetricsManager.Requests;
using MetricsManager.Responses;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        Task<AllCpuMetricsApiResponse> GetCpuMetrics(GetAllCpuMetricsApiRequest request);
        AllDotNetMetricsApiResponse GetDotNetMetrics(GetAllDotNetMetricsApiRequest request);
        AllNetworkMetricsApiResponse GetNetworkMetrics(GetAllNetworkMetricsApiRequest request);
        AllRamMetricsApiResponse GetRamMetrics(GetAllRamMetricsApiRequest request);
        AllRomMetricsApiResponse GetRomMetrics(GetAllRomMetricsApiRequest request);
    }
}
