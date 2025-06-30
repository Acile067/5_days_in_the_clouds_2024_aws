terraform {
  required_providers {
    tls = {
      source = "hashicorp/tls"
      version = "4.1.0"
    }
    aws = {
      source  = "hashicorp/aws"
      version = "6.0.0"
    }
  }

  backend "s3" {

    encrypt = true
  }
}

provider "tls" {
  # Configuration options
}
provider "aws" {
  region = "eu-central-1"
}
