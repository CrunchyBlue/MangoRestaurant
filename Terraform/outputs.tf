output "image_urls" {
  value = {
    for k, stb in azurerm_storage_blob.storage_blob : k => stb.url
  }
}

output "servicebus_connection_string" {
  value     = azurerm_servicebus_namespace.servicebus_namespace.default_primary_connection_string
  sensitive = true
}