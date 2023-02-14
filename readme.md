# Azure Functions - Custom Binding test project

This is a sample project that is used to test the custom Function binding that is [available on GitHub](https://github.com/sjwaight/azure-functions-acs-email-extension). The assemlby that contains the custom binding can be found in the dlls folder and is the output result of the linked repository. It can be manually updated if required (in a production environment you'd package the DLL into a nuget package.. but here we're just testing things out).

Here is a sample local.settings.json file. The `AzureWebJobsAcsEmailConnectionString` is only required if you don't specify the application setting to use in the Binding (see below).


```
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet",
        "AzureWebJobsAcsEmailConnectionString": "endpoint=https://{your_acs_account}.communication.azure.com/;accesskey={your_acs_account_access_key}"
    }
}
```

Use this default setting by defining your binding like this:

```csharp
[CommunicationServiceEmail()] IAsyncCollector<EmailMessage> emailClient
```

If you prefer to use a custom connection string app setting reference in the binding in your code you can do as this, where `YourAppSettingValue` is the key in your local.settings.json file or Azure Portal app settings. 

```csharp
[CommunicationServiceEmail(ConnectionString = "YourAppSettingValue")] IAsyncCollector<EmailMessage> emailClient
```

There are a few other properties that can be bound at runtime as well: SenderAddress, RecipientAddress and SubjectLine.