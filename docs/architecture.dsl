workspace my-budget {

    model {
        user = person "User"
        softwareSystem = softwareSystem "myBudget" {
            description "Allows useers to manage their personal budgets"
            myBudgetFrontend = container "my-budget.frontend" {
                technology "react"
                description "Provides all of functionality to users via their web browser."
                user -> this "Uses"
            }
            myBudgetApi = container "my-budget.api" {
                technology .net
                description "Provides all functionality via a JSON/HTTP API."
                myBudgetFrontend -> this "Makes API calls to"
            }
            
            myBudgetIdentity = container "my-budget.identity" {
                technology .net
                description "Provides authentication functionality"
                user -> this "Uses"
                myBudgetApi -> this "validate tokens with"
                myBudgetFrontend -> this "authorize with"
            }
            
            database = container "database" {
                tags db
                myBudgetApi -> this "Reads from and writes to"
                myBudgetIdentity -> this "Reads from and writes to"
            }
        }
        
        
        production = deploymentEnvironment "Production" {
        
            deploymentNode "Azure" {
                tags "Microsoft Azure"
                deploymentNode "Azure App Services" {
                    tags "Microsoft Azure - App Services"
                    
                    deploymentNode "docker container: my-budget.frontend" {
                        tags "docker"
                        containerInstance myBudgetFrontend
                    }
                    
                    deploymentNode "docker container: my-budget.api" {
                        tags "docker"
                        containerInstance myBudgetApi
                    }
                    
                    deploymentNode "docker container: my-budget.identity" {
                        tags "docker"
                        containerInstance myBudgetIdentity
                    }
                }
            
                deploymentNode "MsSql" {
                    tags "Microsoft Azure - SQL Server"
                    containerInstance database
                }
            }
        }
    }

    views {
        systemContext softwareSystem {
            include *
            autolayout lr
        }

        container softwareSystem {
            include *
             autolayout lr
        }
        
        deployment * production {
            include *
            autolayout lr
        }

        themes https://static.structurizr.com/themes/microsoft-azure-2023.01.24/theme.json default
        
        styles {
            element db {
                shape cylinder
            }
        }
    }

}