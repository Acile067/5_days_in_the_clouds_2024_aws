using _5_days_in_the_clouds_2024.Domain.Contracts;
using _5_days_in_the_clouds_2024.Domain.Entities;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Infrastructure.Services
{
    public class MatchUploaderService : IMatchUploaderService
    {
        private readonly IAmazonLambda _lambdaClient;
        private readonly string _lambdaFunctionName;
        public MatchUploaderService(IAmazonLambda lambdaClient, IConfiguration configuration) 
        {
            _lambdaClient = lambdaClient;
            _lambdaFunctionName = configuration["Aws:LambdaFunctionName"] ?? throw new ArgumentNullException("Aws:LambdaFunctionName");
        }

        public async Task UploadMatchAsync(Match match)
        {
            var payload = JsonSerializer.Serialize(match);

            var request = new InvokeRequest
            {
                FunctionName = _lambdaFunctionName,
                Payload = payload,
                InvocationType = InvocationType.Event
            };

            var response = await _lambdaClient.InvokeAsync(request);

            if (response.StatusCode != 202 && response.StatusCode != 200)
            {
                throw new Exception($"Lambda invocation failed with status code {response.StatusCode}");
            }
        }
    }
}
