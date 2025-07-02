resource "aws_lambda_function" "match_storage" {
  function_name = "MatchStorageFunction"
  handler       = "MatchStorage::MatchStorage.Function::FunctionHandler"
  runtime       = "dotnet8"
  role          = aws_iam_role.lambda_exec_role.arn
  timeout       = 10
  memory_size   = 256

  filename         = "${path.module}/lambda/bin/Release/net8.0/lambda.zip"
  source_code_hash = filebase64sha256("${path.module}/lambda/bin/Release/net8.0/lambda.zip")

  environment {
    variables = {
      BUCKET_NAME = aws_s3_bucket.match_storage.bucket
    }
  }
}