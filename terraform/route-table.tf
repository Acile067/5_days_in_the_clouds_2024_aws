resource aws_route_table main {
  vpc_id = aws_vpc.main.id

  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.main.id
  }
}

resource aws_route_table_association rta1 {
  subnet_id      = aws_subnet.backend.id
  route_table_id = aws_route_table.main.id
}

resource aws_route_table_association rta2 {
  subnet_id      = aws_subnet.database.id
  route_table_id = aws_route_table.main.id
}