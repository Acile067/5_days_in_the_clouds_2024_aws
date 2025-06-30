data aws_secretsmanager_secret db_password {
  name = "dbpassword"
}

data aws_secretsmanager_secret_version db_password_version {
  secret_id = data.aws_secretsmanager_secret.db_password.id
}

resource aws_db_instance mssql {
  identifier           = "my-mssql-db"
  engine               = "sqlserver-ex"
  engine_version       = "15.00.4430.1.v1"
  instance_class       = "db.t3.micro"
  allocated_storage    = 20
  username             = "adminuser"
  password             = data.aws_secretsmanager_secret_version.db_password_version.secret_string
  publicly_accessible  = true
  skip_final_snapshot  = true
}
