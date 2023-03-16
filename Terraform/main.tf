terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "3.30.0"
    }
  }
  backend "azurerm" {
    resource_group_name  = "Terraform"
    storage_account_name = "crunchybluebackend"
    container_name       = "tfstate"
    key                  = "terraform.tfstate"
  }
}

provider "azurerm" {
  features {}
}

# Data and local substring blocks used to help assure uniqueness for resources that require globally unique names
data "azurerm_subscription" "current" {
}

locals {
  subscription_id = substr(data.azurerm_subscription.current.id, -5, -1)
  service_bus = merge([
    for topic, v in var.service_bus : {
      for subscription in v[0].subscriptions :
      "${topic}-${subscription}" => {
        topic        = topic
        subscription = subscription
      }
    }
  ]...)
}

resource "azurerm_resource_group" "resource_group" {
  name     = "rg-${var.application}-${var.location}"
  location = var.resource_location

  tags = var.tags
}

resource "azurerm_storage_account" "storage_account" {
  name                     = "st${var.application}${var.location}${local.subscription_id}"
  resource_group_name      = azurerm_resource_group.resource_group.name
  location                 = azurerm_resource_group.resource_group.location
  account_tier             = "Standard"
  account_replication_type = "LRS"

  tags = var.tags
}

resource "azurerm_storage_container" "storage_container" {
  name                  = var.application
  storage_account_name  = azurerm_storage_account.storage_account.name
  container_access_type = "blob"
}

resource "azurerm_storage_blob" "storage_blob" {
  for_each = fileset(path.module, "files/*")

  name                   = trim(each.key, "files/")
  storage_account_name   = azurerm_storage_account.storage_account.name
  storage_container_name = azurerm_storage_container.storage_container.name
  type                   = "Block"
  source                 = each.key
}

resource "azurerm_servicebus_namespace" "servicebus_namespace" {
  name                = "sb${var.application}${var.location}${local.subscription_id}"
  location            = azurerm_resource_group.resource_group.location
  resource_group_name = azurerm_resource_group.resource_group.name
  sku                 = "Standard"

  tags = var.tags
}

resource "azurerm_servicebus_topic" "topic" {
  for_each = var.service_bus

  name         = each.key
  namespace_id = azurerm_servicebus_namespace.servicebus_namespace.id

  max_size_in_megabytes = 1024
}

resource "azurerm_servicebus_subscription" "subscription" {
  for_each = local.service_bus

  name               = each.value.subscription
  topic_id           = azurerm_servicebus_topic.topic[each.value.topic].id
  max_delivery_count = 1
}