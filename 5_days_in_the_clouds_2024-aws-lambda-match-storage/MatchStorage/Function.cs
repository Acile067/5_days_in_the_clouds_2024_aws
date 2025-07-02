using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Model;
using System.Net;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MatchStorage;

public class Function
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public Function()
    {
        _s3Client = new AmazonS3Client(); // koristi podrazumevane kredencijale i region
        _bucketName = Environment.GetEnvironmentVariable("BUCKET_NAME") ?? throw new Exception("Missing BUCKET_NAME env variable");
    }

    // Handler koji prima direktno objekat Match
    public async Task FunctionHandler(Match match, ILambdaContext context)
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match), "Match object is null.");
        }

        try
        {
            var json = JsonSerializer.Serialize(match);

            var key = $"matches/{match.Id}_{DateTime.UtcNow:yyyyMMddHHmmss}.json";

            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
                ContentBody = json,
                ContentType = "application/json"
            };

            await _s3Client.PutObjectAsync(putRequest);

            context.Logger.LogInformation($"Match saved to S3 as {key}");
        }
        catch (Exception ex)
        {
            context.Logger.LogError($"Error saving match: {ex.Message}");
            throw;
        }
    }
}
