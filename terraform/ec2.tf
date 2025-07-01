resource aws_instance backend {
  depends_on = [ aws_key_pair.main, tls_private_key.main  ]

  ami                    = "ami-0229b8f55e5178b65"
  instance_type          = var.instance_type
  vpc_security_group_ids = [aws_security_group.main.id]
  subnet_id              = aws_subnet.sub1.id
  key_name               = aws_key_pair.main.key_name
  user_data              = <<EOF
                           #!/bin/bash
                           # Update sistema
                           dnf update -y

                           # Instalacija osnovnih alata
                           dnf install -y wget unzip git jq zip

                           # Preuzimanje .NET 9 runtime
                           wget https://builds.dotnet.microsoft.com/dotnet/aspnetcore/Runtime/9.0.6/aspnetcore-runtime-9.0.6-linux-x64.tar.gz

                           mkdir -p \$HOME/dotnet && tar zxf aspnetcore-runtime-9.0.6-linux-x64.tar.gz -C \$HOME/dotnet
                           export DOTNET_ROOT=\$HOME/dotnet
                           export PATH=\$PATH:\$HOME/dotnet
                           dotnet --info

                           echo 'export DOTNET_ROOT=\$HOME/dotnet' >> /home/ec2-user/.bash_profile
                           echo 'export PATH=\$PATH:\$HOME/dotnet' >> /home/ec2-user/.bash_profile

                           dnf install -y awscli
                           EOF
}