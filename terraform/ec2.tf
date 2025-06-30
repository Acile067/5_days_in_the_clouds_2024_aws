resource aws_instance backend {
  ami                    = "ami-0229b8f55e5178b65"
  instance_type          = var.instance_type
  vpc_security_group_ids = [aws_security_group.main.id]
  subnet_id              = aws_subnet.sub1.id
  key_name               = aws_key_pair.main.key_name
  user_data_base64       = base64encode(file("userdata.sh"))
}