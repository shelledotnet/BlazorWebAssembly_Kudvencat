using LandReal.Client.Services;
using LandReal.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LandReal.Client
{
    public class Program
    {
       
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDg4NTI0QDMxMzkyZTMyMmUzME5kYUlJa0g2RVpMV2tuNnZQNy9GK2FTOWh2Lys0aVRrb3U3Z3hDdmxoRWc9;NDg4NTI1QDMxMzkyZTMyMmUzMGJmZkFNbnh4ZTRNRENzR0dUQ1VwWXVlcG0vYk9LVHVXM1R5Sjg1R0ZKNHc9;NDg4NTI2QDMxMzkyZTMyMmUzMGZPU1lwcmVqaGlHWTZzekl4R3hSQlJTVlZhT2xCMXFINEZpcFdJTUtQMGs9;NDg4NTI3QDMxMzkyZTMyMmUzMFlRbFZObXVZS3lneWtzTlVjM1Nya3hPYm1lSVVpMXo2c1dMQ2dZZFdZYTQ9;NDg4NTI4QDMxMzkyZTMyMmUzMG8yYlNMbTV0VFVmZGJQUW14UXhqbUhLN3RCWGZpWFk3RTQ2QnlvdG40T1k9;NDg4NTI5QDMxMzkyZTMyMmUzMEpDSXhkalNwRnpVV3R1eVNJbGVXdHY1ckFJSFFqRWVsKzRxSDNsR0hIbms9;NDg4NTMwQDMxMzkyZTMyMmUzMGsrR1RQQjY2Mml4cHFIaFlRQ0JESjJNa2ovRnljVjNabzNKNkJyZmN1WTQ9;NDg4NTMxQDMxMzkyZTMyMmUzMFJyUXk4ZFhrV1hsY1NyN1hNUlVrRm9JYjRralJvaWFTTm1PMlorT0prRW89;NDg4NTMyQDMxMzkyZTMyMmUzMFhNbW5WaE1MTUN1dzdDbEhEcmlYdVh6Qm5tdWZXNExvY3IvQ3hqYnRsbE09;NDg4NTMzQDMxMzkyZTMyMmUzME9lTnJSYlJSTTg4TjduaW1aN2pQeSt2NjMwTWx1dzhCRFNyQ1l4aVdrbms9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped<EmployeeAdaptor>();
            builder.Services.AddHttpClient<IEmployeeService, EmployeeService>(client=>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            
            });
            builder.Services.AddHttpClient<IDepartmentService, DepartmentService>(client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);

            });
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddMudServices();
            await builder.Build().RunAsync();
           
        }
    }
}
