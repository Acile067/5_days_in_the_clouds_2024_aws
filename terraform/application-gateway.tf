resource aws_lb main {
  name               = "main_application_gateway"
  internal           = false
  load_balancer_type = "application"

  security_groups = [aws_security_group.main.id]
  subnets         = [aws_subnet.backend.id]
}

resource aws_lb_target_group main {
  name     = "main_target_group"
  port     = 5000
  protocol = "HTTP"
  vpc_id   = aws_vpc.main.id

  health_check {
    path = "/test"
    port = 5000   
  }
}

resource aws_lb_target_group_attachment backend_attach {
  target_group_arn = aws_lb_target_group.main.arn
  target_id        = aws_instance.backend.id
  port             = 5000
}

resource aws_lb_listener listener {
  load_balancer_arn = aws_lb.main.arn
  port              = 80
  protocol          = "HTTP"

  default_action {
    target_group_arn = aws_lb_target_group.main.arn
    type             = "forward"
  }
}