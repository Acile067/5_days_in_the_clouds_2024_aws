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
        _s3Client = new AmazonS3Client(); // default credentials + region
        _bucketName = Environment.GetEnvironmentVariable("BUCKET_NAME") ?? throw new Exception("Missing BUCKET_NAME env variable");
    }

    public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        try
        {
            // Deserialize Match object from body
            var match = JsonSerializer.Deserialize<Match>(request.Body);

            if (match == null)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid Match data."
                };
            }

            // Convert to JSON string
            var json = JsonSerializer.Serialize(match);

            // Generate a unique file name
            var key = $"matches/{match.Id}_{DateTime.UtcNow:yyyyMMddHHmmss}.json";

            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
                ContentBody = json,
                ContentType = "application/json"
            };

            await _s3Client.PutObjectAsync(putRequest);

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = $"Match saved to S3 as {key}"
            };
        }
        catch (Exception ex)
        {
            context.Logger.LogError($"Error saving match: {ex.Message}");
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Body = "Error saving match."
            };
        }
    }
}
