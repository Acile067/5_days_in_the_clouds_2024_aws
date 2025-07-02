#!/bin/bash
# Update sistema
dnf update -y

# Instalacija osnovnih alata
dnf install -y wget unzip git jq zip

# Preuzimanje .NET 9 runtime
sudo wget https://builds.dotnet.microsoft.com/dotnet/aspnetcore/Runtime/9.0.6/aspnetcore-runtime-9.0.6-linux-x64.tar.gz

sudo mkdir -p $HOME/dotnet && tar zxf aspnetcore-runtime-9.0.6-linux-x64.tar.gz -C $HOME/dotnet
sudo export DOTNET_ROOT=$HOME/dotnet
sudo export PATH=$PATH:$HOME/dotnet
sudo dotnet --info

sudo echo 'export DOTNET_ROOT=$HOME/dotnet' >> .bash_profile
sudo echo 'export PATH=$PATH:$HOME/dotnet' >> .bash_profile

sudo dnf install -y libicu

dnf install -y awscli
