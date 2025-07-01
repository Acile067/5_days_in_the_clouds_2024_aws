resource aws_db_subnet_group mssql_subnet_group {
  name       = "mssql-subnet-group"
  subnet_ids = [aws_subnet.backend.id, aws_subnet.database.id]
}