resource aws_apigatewayv2_api main {
  name          = "mainapplicationgateway"
  protocol_type = "HTTP"
}

resource aws_apigatewayv2_stage main {
  api_id      = aws_apigatewayv2_api.main.id
  name        = "mainstage"
  auto_deploy = true
}

resource "aws_apigatewayv2_integration" "main" {
  api_id          = aws_apigatewayv2_api.main.id
  integration_type = "HTTP_PROXY"
  integration_uri  = "http://${aws_instance.backend.public_ip}:5000/{proxy}"
  integration_method = "ANY"
  connection_type = "INTERNET"
}

resource "aws_apigatewayv2_route" "main" {
  api_id    = aws_apigatewayv2_api.main.id
  route_key = "ANY /{proxy+}"
  target    = "integrations/${aws_apigatewayv2_integration.main.id}"
}