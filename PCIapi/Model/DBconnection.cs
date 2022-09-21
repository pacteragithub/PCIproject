﻿using System;
using System.Data;
using System.Data.SqlClient;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace PCIapi.Model
{
    public class DBconnection
    {
        private string connectionString;
        public DBconnection()
        {
            string kvURL = "https://pcidbconnection.vault.azure.net/";
            string tenantId = "9b415834-803a-4da0-afdc-fe6b1d52d649";
            string clientId = "14d7f64e-9171-48ac-8aed-372db04b8c69";
            string ClientSecret = "fVW8Q~zLTy_KRf4NfrF5I6eVPw75HbosP6NXbcIl";

            var credential = new ClientSecretCredential(tenantId, clientId, ClientSecret);
            var client = new SecretClient(new Uri(kvURL), credential);
            string strsecretkey = client.GetSecret("ConnectionStrings--PCIDBconnection").Value.Value;
            connectionString = @strsecretkey;
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }
    }

    public class Settings
    {
        public string AppName { get; set; }
        public double Version { get; set; }
        public long RefreshRate { get; set; }
        public long FontSize { get; set; }
        public string Language { get; set; }
        public string Messages { get; set; }
        public string BackgroundColor { get; set; }
    }

}
