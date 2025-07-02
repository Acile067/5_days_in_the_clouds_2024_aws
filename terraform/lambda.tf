resource aws_lambda_function match_storage {
  function_name = "MatchStorageFunction"
  handler       = "MatchStorage::MatchStorage.Function::FunctionHandler"
  runtime       = "dotnet8"
  role          = aws_iam_role.lambda_exec_role.arn
  timeout       = 10
  memory_size   = 256
  publish = false
  environment {
    variables = {
      BUCKET_NAME = aws_s3_bucket.match_storage.bucket
    }
  }
  lifecycle {
    ignore_changes = [filename, source_code_hash]
  }
}