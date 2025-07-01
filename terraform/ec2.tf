resource aws_instance backend {
  depends_on = [ aws_key_pair.main, tls_private_key.main  ]

  ami                    = "ami-0229b8f55e5178b65"
  instance_type          = var.instance_type
  vpc_security_group_ids = [aws_security_group.main.id]
  subnet_id              = aws_subnet.sub1.id
  key_name               = aws_key_pair.main.key_name
  user_data              = base64encode(file("userdata.sh"))
}