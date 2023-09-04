using MetricsManager.Requests;
using MetricsManager.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        Task<AllCpuMetricsApiResponse> GetCpuMetricsAsync(GetAllCpuMetricsApiRequest request, CancellationToken cancellationToken = default);
        Task<AllDotNetMetricsApiResponse> GetDotNetMetricsAsync(GetAllDotNetMetricsApiRequest request, CancellationToken cancellationToken = default);
        Task<AllNetworkMetricsApiResponse> GetNetworkMetricsAsync(GetAllNetworkMetricsApiRequest request, CancellationToken cancellationToken = default);
        Task<AllRamMetricsApiResponse> GetRamMetricsAsync(GetAllRamMetricsApiRequest request, CancellationToken cancellationToken = default);
        Task<AllRomMetricsApiResponse> GetRomMetricsAsync(GetAllRomMetricsApiRequest request, CancellationToken cancellationToken = default);
    }
}
