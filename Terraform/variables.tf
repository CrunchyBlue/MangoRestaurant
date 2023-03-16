variable "application" {
  type    = string
  default = "mango"
}

variable "resource_location" {
  type    = string
  default = "Central US"
}

variable "location" {
  type    = string
  default = "centralus"
}

variable "tags" {
  type = map(string)
  default = {
    environment = "production"
    application = "mango"
  }
}

variable "service_bus" {
  type = map(list(map(list(string))))
  default = {
    "checkout" = [{
      subscriptions = ["checkout"]
    }],
    "payment" = [{
      subscriptions = ["payment"]
    }],
    "order" = [{
      subscriptions = ["order", "email"]
    }]
  }
}