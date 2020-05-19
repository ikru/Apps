# export https_proxy=
# tfpath="$PWD/terraform"; terraform() { "$tfpath" "$@"; }
# az login 
# terraform plan
# terraform apply
# az aks install-cli
# az aks get-credentials --resource-group AKS --name aks
# kubectl get nodes
provider "azuread" {
  version = "~> 0.8"

  tenant_id = var.tenant_id
  subscription_id = var.subscription_id
  }
provider "azurerm" {
  version = "=1.44.0"

  tenant_id = var.tenant_id
  subscription_id = var.subscription_id
}
resource "azuread_application" "app" {
  name                       = "aksapp"
  homepage                   = "https://aksapp"
  identifier_uris            = ["https://aksapp"]
  reply_urls                 = ["https://aksapp"]
  available_to_other_tenants = false
  oauth2_allow_implicit_flow = true
}

resource "azuread_service_principal" "sp" {
  application_id = azuread_application.app.application_id
}

resource "azuread_service_principal_password" "spp" {
  service_principal_id = azuread_service_principal.sp.id
  value                = "VT=uSgbTanZhyz@%nL9Hpd+Tfay_MRV#"
  end_date             = "2099-01-01T01:02:03Z"
}

resource "azurerm_resource_group" "AKS" {
  name     = "AKS"
  location = "West US"
}

resource "azurerm_kubernetes_cluster" "aks" {
  name                = "aks"
  location            = azurerm_resource_group.AKS.location
  resource_group_name = azurerm_resource_group.AKS.name
  dns_prefix          = "ivan"

  default_node_pool {
    name       = "default"
    node_count = 1
    vm_size    = "Standard_D2_v3"
  }

  service_principal {
        client_id     = azuread_service_principal.sp.id
        client_secret = azuread_service_principal_password.spp.value
  }

  identity {
    type = "SystemAssigned"
  }

  tags = {
    Environment = "Development"
  }
}

output "client_certificate" {
  value = azurerm_kubernetes_cluster.aks.kube_config.0.client_certificate
}

output "kube_config" {
  value = azurerm_kubernetes_cluster.aks.kube_config_raw
}