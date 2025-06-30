resource aws_secretsmanager_secret main {
  name = "main-ssh-private-key"
}

resource aws_secretsmanager_secret_version main {
  secret_id     = aws_secretsmanager_secret.main.id
  secret_string = tls_private_key.main.private_key_pem
}