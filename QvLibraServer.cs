using System;
using System.Security.Authentication;
using QlikView.Qvx.QvxLibrary;
using QvEventLogConnectorSimple;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;



namespace QvEventLogConnectorSimple
{
    public class QvLibraServer : QvxServer
    {
        public override QvxConnection CreateConnection()
        {
            return new QvLibraConnection();
        }

        public override string CreateConnectionString()
        {
            return "";
        }
        

        /**
         * QlikView classes. Handles requests from web UI.
         */

        public override string HandleJsonRequest(string method, string[] userParameters, QvxConnection connection)
        {
            QvDataContractResponse response;

            /**
             * -- How to get hold of connection details? --
             *
             * Provider, username and password are always available in
             * connection.MParameters if they exist in the connection
             * stored in the QlikView Repository Service (QRS).
             *
             * If there are any other user/connector defined parameters in the
             * connection string they can be retrieved in the same way as seen
             * below
             */

            string provider, host, username, password;
            connection.MParameters.TryGetValue("provider", out provider); // Set to the name of the connector by QlikView Engine
            connection.MParameters.TryGetValue("userid", out username); // Set when creating new connection or from inside the QlikView Management Console (QMC)
            connection.MParameters.TryGetValue("password", out password); // Same as for username
            connection.MParameters.TryGetValue("host", out host); // Defined when calling createNewConnection in connectdialog.js

            switch (method)
            {
                case "getInfo":
                    response = getInfo();
                    break;
                case "getDatabases":
                    response = getDatabases(username, password);
                    break;
                case "getTables":
                    response = getTables(username, password, connection, userParameters[0], userParameters[1]);
                    break;
                case "getFields":
                    response = getFields(username, password, connection, userParameters[0], userParameters[1], userParameters[2]);
                    break;
                case "testConnection":
                    response = testConnection(userParameters[0], userParameters[1]);
                    break;
                default:
                    response = new Info { qMessage = "Unknown command" };
                    break;
            }
            return ToJson(response);    // serializes response into JSON string
        }

        public bool verifyCredentials (string username, string password) {
            return (GetToken(username, password) != null);
        }

        public QvDataContractResponse getInfo()
        {
            return new Info
            {
                qMessage = "Please provide your Email & Password for Libra."
            };
        }

        public QvDataContractResponse getDatabases(string username, string password)
        {
            if (verifyCredentials(username, password))
            {
                return new QvDataContractDatabaseListResponse
                {
                    qDatabases = new Database[]
                    {
                        new Database {qName = "Libra"}
                    }
                };
            }
            return new Info { qMessage = "Credentials WRONG!" };
        }

        public QvDataContractResponse getTables(string username, string password, QvxConnection connection, string database, string owner)
        {
            return new QvDataContractTableListResponse
            {
                qTables = connection.MTables
            };
        }

        public QvDataContractResponse getFields(string username, string password, QvxConnection connection, string database, string owner, string table)
        {
            var currentTable = connection.FindTable(table, connection.MTables);

            return new QvDataContractFieldListResponse
            {
                qFields = (currentTable != null) ? currentTable.Fields : new QvxField[0]
            };
        }

        public QvDataContractResponse testConnection(string username, string password)
        {
            var message = "Credentials Invalid";
            if (verifyCredentials( username, password ) ) {
                message = "Credentials Valid";
            }
            return new Info { qMessage = message };
        }





        public static async Task<string> GetRawToken(string username, string password)
        {
            // ... Target page.
            string url = String.Format("https://api.libratax.com/v1/auth/authenticate?email={0}&password={1}", username, password);

            // ... Use HttpClient.
            HttpClient client = new HttpClient();
            HttpContent contents = null;
            HttpResponseMessage response = await client.PostAsync(url, contents);
            HttpContent content = response.Content;

            string result = await content.ReadAsStringAsync();
            return result;
        }




        public static string GetToken(string username, string password)
        {
            Task<string> t = GetRawToken(username, password);
            t.Wait();
            string rawToken = t.Result;
            RootObjectToken jsonToken = JsonConvert.DeserializeObject<RootObjectToken>(rawToken);
            string fullPath = System.Reflection.Assembly.GetAssembly(typeof(QvLibraConnection)).Location;
            string theDirectory = Path.GetDirectoryName(fullPath);
            string filePath = String.Format(@"{0}\_Token.txt", theDirectory); string data = jsonToken.token;
            File.WriteAllText(filePath, data);
            return jsonToken.token;
        }



        public class RootObjectToken
        {
            public string token { get; set; }
            public string expires_at { get; set; }
        }








    }
}
