#!/bin/bash
# Update sistema
dnf update -y

# Instalacija osnovnih alata
dnf install -y wget curl unzip git jq zip

# Preuzimanje .NET 9 runtime
wget https://builds.dotnet.microsoft.com/dotnet/aspnetcore/Runtime/9.0.6/aspnetcore-runtime-9.0.6-linux-x64.tar.gz

mkdir -p $HOME/dotnet && tar zxf aspnetcore-runtime-9.0.6-linux-x64.tar.gz -C $HOME/dotnet
export DOTNET_ROOT=$HOME/dotnet
export PATH=$PATH:$HOME/dotnet
dotnet --info

echo 'export DOTNET_ROOT=$HOME/dotnet' >> .bash_profile
echo 'export PATH=$PATH:$HOME/dotnet' >> .bash_profile

dnf install -y awscli
