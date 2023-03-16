# Mango Restaurant

## Introduction

This repository demonstrates a microservice architecture simulating a restaurant ordering application. 

## Prerequisites

- [az CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)
- [Terraform](https://developer.hashicorp.com/terraform/tutorials/aws-get-started/install-cli)
- [.NET CLI](https://learn.microsoft.com/en-us/dotnet/core/tools/)
- [Entity Framework Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

## Build

1. Manually create a storage account in your Azure subscription and update the following stanza found in `Terraform/main.tf` to configure a remote backend.

        backend "azurerm" {
            resource_group_name  = "YOUR_RESOURCE_GROUP"
            storage_account_name = "YOUR_STORAGE_ACCOUNT"
            container_name       = "YOUR_CONTAINER"
            key                  = "terraform.tfstate"
        }
2. Update the values in `Terraform/variables.tf` as necessary (some resource names must be globally unique).
   - NOTE: Changes to `variables.tf` may necessitate a change to the seeded products in the `Mango.Services.ProductAPI/DbContexts/ApplicationDbContext.cs` so the correct urls are used. Those urls are configured as terraform outputs and can be recovered after running `terraform apply`.
3. Run `az login` to authenticate to your azure account.
4. Run `terraform init` to initialize remote backend and providers.
5. Run `terraform apply` to provision a storage account and upload necessary images that are fetched for the application as well as the service bus and necessary topics and subscriptions.
6. Create the necessary databases by running the following `dotnet` commands:
   - `dotnet ef database update -c ApplicationDbContext -p Mango.Services.CouponAPI -s Mango.Services.CouponAPI`
   - `dotnet ef database update -c ApplicationDbContext -p Mango.Services.Email -s Mango.Services.Email`
   - `dotnet ef database update -c ApplicationDbContext -p Mango.Services.Identity -s Mango.Services.Identity`
   - `dotnet ef database update -c ApplicationDbContext -p Mango.Services.OrderProcessor -s Mango.Services.OrderProcessor`
   - `dotnet ef database update -c ApplicationDbContext -p Mango.Services.ProductAPI -s Mango.Services.ProductAPI`
   - `dotnet ef database update -c ApplicationDbContext -p Mango.Services.ShoppingCartAPI -s Mango.Services.ShoppingCartAPI`
7. Update the following `appsettings.json` files with the Azure Service Bus connection string that can be found in the Azure portal for the newly created Azure Service Bus resource.
   - `Mango.Services.Email/appsettings.json`
   - `Mango.Services.OrderProcessor/appsettings.json`
   - `Mango.Services.PaymentProcessor/appsettings.json`
   - `Mango.Services.ShoppingCartAPI/appsettings.json`
8. Run the solution.

## Additional Information

Seeded credentials for login:
- Admin:
  - Username: Admin@example.com
  - Password: Admin#123
- Customer:
  - Username: Customer@example.com
  - Password: Customer#123