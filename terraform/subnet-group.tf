resource aws_db_subnet_group mssql_subnet_group {
  name       = "mssql-subnet-group"
  subnet_ids = [
    aws_subnet.database.id,
    aws_subnet.database_az2.id
  ]
}