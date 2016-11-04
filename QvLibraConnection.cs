using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using QlikView.Qvx.QvxLibrary;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;



namespace QvEventLogConnectorSimple
{
    public class QvLibraConnection : QvxConnection
    {
        public override void Init()
        {

            var addressFields = new QvxField[]
            {
                new QvxField("Address_Id", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Address_Name", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Address", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Address_Asset", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Address_Balance", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL),
                new QvxField("Address_Updated_At", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.DATE),
                new QvxField("Address_Active", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII)
            };

            var transactionFields = new QvxField[]
            {
                new QvxField("Transaction_Id", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Transaction_Asset", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Transaction_Amount", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL),
                new QvxField("Transaction_Fair_Value", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL),
                new QvxField("Transaction_Spot_Value", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL),
                new QvxField("Transaction_Total_Value", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL),
                new QvxField("Transaction_Note", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Transaction_Destination", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Datetime", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.DATE),
                new QvxField("Transaction_Created_At", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.DATE),
                new QvxField("Address_Id", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Wallet_Id", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Transaction_Tax_Type", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII)
            };

            var walletFields = new QvxField[]
            {
                new QvxField("Wallet_Id", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Wallet_Name", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Wallet_Provider_Id", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Wallet_Provider_Name", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Wallet_Asset", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Wallet_Balance", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL),
                new QvxField("Wallet_Updated_At", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.DATE)
            };

            var tradeFields = new QvxField[]
            {
                new QvxField("Trade_Id", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Trade_Asset1", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Trade_Amount1", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL),
                new QvxField("Trade_Asset2", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Trade_Amount2", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL),
                new QvxField("Trade_Rate", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL),
                new QvxField("Trade_Total_Value", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL),
                new QvxField("Trade_Note", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Trade_Direction", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Datetime", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.DATE),
                new QvxField("Trade_Created_At", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.DATE),
                new QvxField("Trade_Provider", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Trade_Provider_Id", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII)
            };

            var lotFields = new QvxField[]
            {
                new QvxField("Match_Id", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Match_Calculation_Method", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Match_Asset", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Match_User_Id", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Datetime", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Match_Updated_At", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Match_Calculation_Started_At", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Match_Unsold_Lots", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Match_Invalidated_At", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII)
                //new QvxField("Lot_Date", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Remaining", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Total_Value", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Fee", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Fair_Value", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Id", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Gain_Type", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Amount", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Total_Sale_Value", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Value_Per_Coin", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Amount_Remaining", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Tax_Type", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Net_Gain", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Amount_Sold", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Spot_Value", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Cost_Basis_Per_Coin", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Asset", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Date_Sold", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Sale_Value_Per_Coin", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Sale_Id", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                //new QvxField("Lot_Total_Cost_Basis", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII)
            };

        
            var calculationFields = new QvxField[]
            {
                new QvxField("Calculation_Method", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Calculation_Asset", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Calculation_Period", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Calculation_Status_Ready", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.ASCII),
                new QvxField("Calculation_Total_Gain_Long_Term", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL),
                new QvxField("Calculation_Total_Gain_Total", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL),
                new QvxField("Calculation_Total_Gain_Unrealized", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL),
                new QvxField("Calculation_Taggable_Totals_Drinks", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL),
                new QvxField("Calculation_Categories_Totals_Accounts_Payable", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL),
                new QvxField("Calculation_Categories_Totals_Accounts_Receivable", QvxFieldType.QVX_TEXT, QvxNullRepresentation.QVX_NULL_FLAG_SUPPRESS_DATA, FieldAttrType.REAL)

            };


            MTables = new List<QvxTable>
            {
                new QvxTable
                {
                    TableName = "Address",
                    GetRows = GetAddressData,
                    Fields = addressFields
                },

                new QvxTable
                {
                    TableName = "Transaction",
                    GetRows = GetTransactionData,
                    Fields = transactionFields
                },

                new QvxTable
                {
                    TableName = "Wallet",
                    GetRows = GetWalletData,
                    Fields = walletFields
                },

                new QvxTable
                {
                    TableName = "Trade",
                    GetRows = GetTradeData,
                    Fields = tradeFields
                },

                new QvxTable
                {
                    TableName = "Lot",
                    GetRows = GetLotData,
                    Fields = lotFields
                },

                new QvxTable
                {
                    TableName = "Calculation",
                    GetRows = GetCalculationData,
                    Fields = calculationFields
                }
            };
        }





        public static string fullPath = System.Reflection.Assembly.GetAssembly(typeof(QvLibraConnection)).Location;
        public static string theDirectory = Path.GetDirectoryName(fullPath);
        public static string filePath = String.Format(@"{0}\_Token.txt", theDirectory); public static string token = File.ReadAllText(filePath);
        public static int num_row = 1000000;
        




        static async Task<string> GetLibraAddressAsync()
        {
            // ... Target page.
            string url = String.Format("https://api.libratax.com/v1/account/addresses?token={0}&page=1&per_page={1}", token, num_row);

            // ... Use HttpClient.
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();
            return result;

        }

        public static List<Address> GetJsonAddressList()
        {
            Task<string> t = GetLibraAddressAsync();
            t.Wait();
            string rawData = t.Result;
            RootObjectA jsonData = JsonConvert.DeserializeObject<RootObjectA>(rawData);
            List<Address> jsonAddressList = jsonData.addresses;
            return jsonAddressList;
        }

        private IEnumerable<QvxDataRow> GetAddressData()
        {
            List<Address> jsonAddressList = GetJsonAddressList();

            foreach (var address in jsonAddressList)
            {
                yield return MakeEntryA(address as Address, FindTable("Address", MTables));
            }
        }

        private QvxDataRow MakeEntryA(Address address, QvxTable table)
        {
            var row = new QvxDataRow();
            row[table.Fields[0]] = address.id;
            row[table.Fields[1]] = address.name;
            row[table.Fields[2]] = address.address;
            row[table.Fields[3]] = address.asset;
            row[table.Fields[4]] = address.balance;
            row[table.Fields[5]] = address.updated_at;
            row[table.Fields[6]] = address.active;
            return row;
        }

        public class Address
        {
            public string id { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public string asset { get; set; }
            public string balance { get; set; }
            public string updated_at { get; set; }
            public string active { get; set; }
        }

        public class RootObjectA
        {
            public List<Address> addresses { get; set; }
            public int total_pages { get; set; }
            public int page { get; set; }
            public int per_page { get; set; }
        }










        static async Task<string> GetLibraTransactionAsync()
        {
            // ... Target page.
            string url = String.Format("https://api.libratax.com/v1/account/transactions?token={0}&page=1&per_page={1}", token, num_row);

            // ... Use HttpClient.
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();
            return result;

        }

        public static List<Transaction> GetJsonTransactionList()
        {
            Task<string> t = GetLibraTransactionAsync();
            t.Wait();
            string rawData = t.Result;
            RootObjectT jsonData = JsonConvert.DeserializeObject<RootObjectT>(rawData);
            List<Transaction> jsonTransactionList = jsonData.transactions;
            return jsonTransactionList;
        }

        private IEnumerable<QvxDataRow> GetTransactionData()
        {
            List<Transaction> jsonTransactionList = GetJsonTransactionList();

            foreach (var transaction in jsonTransactionList)
            {
                yield return MakeEntryT(transaction as Transaction, FindTable("Transaction", MTables));
            }
        }

        private QvxDataRow MakeEntryT(Transaction transaction, QvxTable table)
        {
            var row = new QvxDataRow();
            row[table.Fields[0]] = transaction.id;
            row[table.Fields[1]] = transaction.asset;
            row[table.Fields[2]] = transaction.amount;
            row[table.Fields[3]] = transaction.fair_value;
            row[table.Fields[4]] = transaction.spot_value;
            row[table.Fields[5]] = transaction.total_value;
            row[table.Fields[6]] = transaction.note;
            row[table.Fields[7]] = transaction.destination;
            row[table.Fields[8]] = transaction.date;
            row[table.Fields[9]] = transaction.created_at;
            row[table.Fields[10]] = transaction.address_id;
            row[table.Fields[11]] = transaction.wallet_id;
            row[table.Fields[12]] = transaction.tax_type;

            return row;
        }

        public class Transaction
        {
            public string id { get; set; }
            public string asset { get; set; }
            public string amount { get; set; }
            public string fair_value { get; set; }
            public dynamic spot_value { get; set; }
            public string total_value { get; set; }
            public dynamic note { get; set; }
            public string destination { get; set; }
            public string date { get; set; }
            public string created_at { get; set; }
            public string address_id { get; set; }
            public dynamic wallet_id { get; set; }
            public string tax_type { get; set; }
        }

        public class RootObjectT
        {
            public List<Transaction> transactions { get; set; }
            public int total_pages { get; set; }
            public int page { get; set; }
            public int per_page { get; set; }
        }









        static async Task<string> GetLibraWalletAsync()
        {
            // ... Target page.
            string url = String.Format("https://api.libratax.com/v1/account/wallets?token={0}&page=1&per_page={1}", token, num_row);

            // ... Use HttpClient.
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();
            return result;

        }

        public static List<Wallet> GetJsonWalletList()
        {
            Task<string> t = GetLibraWalletAsync();
            t.Wait();
            string rawData = t.Result;
            RootObjectW jsonData = JsonConvert.DeserializeObject<RootObjectW>(rawData);
            List<Wallet> jsonWalletList = jsonData.wallets;
            return jsonWalletList;
        }

        private IEnumerable<QvxDataRow> GetWalletData()
        {
            List<Wallet> jsonWalletList = GetJsonWalletList();

            foreach (var wallet in jsonWalletList)
            {
                yield return MakeEntryW(wallet as Wallet, FindTable("Wallet", MTables));
            }
        }

        private QvxDataRow MakeEntryW(Wallet wallet, QvxTable table)
        {
            var row = new QvxDataRow();
            row[table.Fields[0]] = wallet.id;
            row[table.Fields[1]] = wallet.name;
            row[table.Fields[2]] = wallet.provider.id;
            row[table.Fields[3]] = wallet.provider.name;
            row[table.Fields[4]] = wallet.asset;
            row[table.Fields[5]] = wallet.balance;

            return row;
        }

        public class Provider
        {
            public dynamic id { get; set; }
            public dynamic name { get; set; }
        }

        public class Wallet
        {
            public dynamic id { get; set; }
            public dynamic name { get; set; }
            public Provider provider { get; set; }
            public dynamic asset { get; set; }
            public dynamic balance { get; set; }
            public dynamic updated_at { get; set; }
        }

        public class RootObjectW
        {
            public List<Wallet> wallets { get; set; }
            public int total_pages { get; set; }
            public int page { get; set; }
            public int per_page { get; set; }
        }










        static async Task<string> GetLibraTradeAsync()
        {
            // ... Target page.
            string url = String.Format("https://api.libratax.com/v1/account/trades?token={0}&page=1&per_page={1}", token, num_row);

            // ... Use HttpClient.
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();
            return result;

        }

        public static List<Trade> GetJsonTradeList()
        {
            Task<string> t = GetLibraTradeAsync();
            t.Wait();
            string rawData = t.Result;
            RootObjectD jsonData = JsonConvert.DeserializeObject<RootObjectD>(rawData);
            List<Trade> jsonTradeList = jsonData.trades;
            return jsonTradeList;
        }

        private IEnumerable<QvxDataRow> GetTradeData()
        {
            List<Trade> jsonTradeList = GetJsonTradeList();

            foreach (var trade in jsonTradeList)
            {
                yield return MakeEntryD(trade as Trade, FindTable("Trade", MTables));
            }
        }

        private QvxDataRow MakeEntryD(Trade trade, QvxTable table)
        {
            var row = new QvxDataRow();
            row[table.Fields[0]] = trade.id;
            row[table.Fields[1]] = trade.asset1;
            row[table.Fields[2]] = trade.amount1;
            row[table.Fields[3]] = trade.asset2;
            row[table.Fields[4]] = trade.amount2;
            row[table.Fields[5]] = trade.rate;
            row[table.Fields[6]] = trade.total_value;
            row[table.Fields[7]] = trade.note;
            row[table.Fields[8]] = trade.direction;
            row[table.Fields[9]] = trade.datetime;
            row[table.Fields[10]] = trade.created_at;
            row[table.Fields[11]] = trade.provider;
            row[table.Fields[12]] = trade.provider_id;

            return row;
        }

        public class Trade
        {
            public dynamic id { get; set; }
            public dynamic asset1 { get; set; }
            public dynamic amount1 { get; set; }
            public dynamic asset2 { get; set; }
            public dynamic amount2 { get; set; }
            public dynamic rate { get; set; }
            public dynamic total_value { get; set; }
            public dynamic note { get; set; }
            public dynamic direction { get; set; }
            public dynamic datetime { get; set; }
            public dynamic created_at { get; set; }
            public dynamic provider { get; set; }
            public dynamic provider_id { get; set; }
        }

        public class RootObjectD
        {
            public List<Trade> trades { get; set; }
            public int total_pages { get; set; }
            public int page { get; set; }
            public int per_page { get; set; }
        }


















        static async Task<string> GetLibraLotAsync()
        {
            // ... Target page.
            string url = String.Format("https://api.libratax.com/v1/account/matched_lots?token={0}&page=1&per_page={1}", token, num_row);

            // ... Use HttpClient.
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();
            return result;

        }

        public static List<Matched> GetJsonLotList()
        {
            Task<string> t = GetLibraLotAsync();
            t.Wait();
            string rawData = t.Result;
            string Data = "{\"Matched\": " + rawData + "}";
            RootObjectL jsonData = JsonConvert.DeserializeObject<RootObjectL>(Data);
            List<Matched> jsonLotList = jsonData.Matched;
            return jsonLotList;
        }

        private IEnumerable<QvxDataRow> GetLotData()
        {
            List<Matched> jsonLotList = GetJsonLotList();

            foreach (var match in jsonLotList)
            {
                yield return MakeEntryL(match as Matched, FindTable("Lot", MTables));

            }

        }

        private QvxDataRow MakeEntryL(Matched match, QvxTable table)
        {
            var row = new QvxDataRow();
            row[table.Fields[0]] = match.id;
            row[table.Fields[1]] = match.calculation_method;
            row[table.Fields[2]] = match.asset;
            row[table.Fields[3]] = match.user_id;
            row[table.Fields[4]] = match.created_at;
            row[table.Fields[5]] = match.updated_at;
            row[table.Fields[6]] = match.calculation_started_at;
            //row[table.Fields[7]] = match.unsold_lots;
            //row[table.Fields[8]] = match.invalidated_at;
            //row[table.Fields[0]] = lot.date;
            //row[table.Fields[1]] = lot.lot_remaining;
            //row[table.Fields[2]] = lot.total_value;
            //row[table.Fields[3]] = lot.fee;
            //row[table.Fields[4]] = lot.fair_value;
            //row[table.Fields[5]] = lot.id;
            //row[table.Fields[6]] = lot.gain_type;
            //row[table.Fields[7]] = lot.amount;
            //row[table.Fields[8]] = lot.total_sale_value;
            //row[table.Fields[9]] = lot.value_per_coin;
            //row[table.Fields[10]] = lot.amount_remaining;
            //row[table.Fields[11]] = lot.tax_type;
            //row[table.Fields[12]] = lot.net_gain;
            //row[table.Fields[13]] = lot.amount_sold;
            //row[table.Fields[14]] = lot.spot_value;
            //row[table.Fields[15]] = lot.cost_basis_per_coin;
            //row[table.Fields[16]] = lot.asset;
            //row[table.Fields[17]] = lot.date_sold;
            //row[table.Fields[18]] = lot.sale_value_per_coin;
            //row[table.Fields[19]] = lot.sale_id;
            //row[table.Fields[20]] = lot.total_cost_basis;

            return row;
        }

        public class Lot
        {
            public dynamic sale_value_per_coin { get; set; }
            public dynamic date { get; set; }
            public dynamic sale_id { get; set; }
            public dynamic total_cost_basis { get; set; }
            public dynamic spot_value { get; set; }
            public dynamic cost_basis_per_coin { get; set; }
            public dynamic fair_value { get; set; }
            public dynamic value_per_coin { get; set; }
            public dynamic fee { get; set; }
            public dynamic amount { get; set; }
            public dynamic asset { get; set; }
            public dynamic amount_remaining { get; set; }
            public dynamic lot_remaining { get; set; }
            public dynamic total_sale_value { get; set; }
            public dynamic total_value { get; set; }
            public dynamic net_gain { get; set; }
            public dynamic tax_type { get; set; }
            public dynamic date_sold { get; set; }
            public dynamic amount_sold { get; set; }
            public dynamic id { get; set; }
            public dynamic gain_type { get; set; }
        }

        public class UnsoldLot
        {
            public dynamic sale_value_per_coin { get; set; }
            public dynamic date { get; set; }
            public dynamic sale_id { get; set; }
            public dynamic total_cost_basis { get; set; }
            public dynamic spot_value { get; set; }
            public dynamic cost_basis_per_coin { get; set; }
            public dynamic fair_value { get; set; }
            public dynamic value_per_coin { get; set; }
            public dynamic fee { get; set; }
            public dynamic amount { get; set; }
            public dynamic asset { get; set; }
            public dynamic amount_remaining { get; set; }
            public dynamic lot_remaining { get; set; }
            public dynamic total_sale_value { get; set; }
            public dynamic total_value { get; set; }
            public dynamic net_gain { get; set; }
            public dynamic tax_type { get; set; }
            public dynamic date_sold { get; set; }
            public dynamic amount_sold { get; set; }
            public dynamic id { get; set; }
            public dynamic gain_type { get; set; }
        }

        public class Matched
        {
            public dynamic asset { get; set; }
            public List<Lot> lots { get; set; }
            public string updated_at { get; set; }
            public dynamic user_id { get; set; }
            public string created_at { get; set; }
            public dynamic calculation_method { get; set; }
            public List<UnsoldLot> unsold_lots { get; set; }
            public dynamic invalidated_at { get; set; }
            public dynamic id { get; set; }
            public string calculation_started_at { get; set; }
        }

        public class RootObjectL
        {
            public List<Matched> Matched { get; set; }
        }






        static async Task<string> GetLibraCalculationAsync()
        {
            // ... Target page.
            string url = String.Format("https://api.libratax.com/v1/account/calculations?token={0}", token);

            // ... Use HttpClient.
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();
            return result;

        }

        public static RootObjectC GetJsonCalculation()
        {
            Task<string> t = GetLibraCalculationAsync();
            t.Wait();
            string rawData = t.Result;
            RootObjectC jsonData = JsonConvert.DeserializeObject<RootObjectC>(rawData);
            return jsonData;
        }

        private IEnumerable<QvxDataRow> GetCalculationData()
        {
            RootObjectC jsonCalculation = GetJsonCalculation();
            yield return MakeEntryC(jsonCalculation, FindTable("Calculation", MTables));
        }

        private QvxDataRow MakeEntryC(RootObjectC cal, QvxTable table)
        {
            var row = new QvxDataRow();
            row[table.Fields[0]] = cal.method;
            row[table.Fields[1]] = cal.asset;
            row[table.Fields[2]] = cal.period;
            row[table.Fields[3]] = cal.status.ready;
            row[table.Fields[4]] = cal.total_gains.long_term;
            row[table.Fields[5]] = cal.total_gains.total;
            row[table.Fields[6]] = cal.total_gains.unrealized;
            row[table.Fields[7]] = cal.taggable_totals.drinks;
            //row[table.Fields[8]] = cal.categories_totals.accounts_payable;
            //row[table.Fields[9]] = cal.categories_totals.accounts_receivable;

            return row;
        }

        public class Status
        {
            public dynamic ready { get; set; }
        }

        public class TotalGains
        {
            public dynamic long_term { get; set; }
            public dynamic total { get; set; }
            public dynamic unrealized { get; set; }
        }

        public class TaggableTotals
        {
            public dynamic drinks { get; set; }
        }

        public class CategoriesTotals
        {
            public dynamic accounts_payable { get; set; }
            public dynamic accounts_receivable { get; set; }
        }

        public class RootObjectC
        {
            public dynamic method { get; set; }
            public dynamic asset { get; set; }
            public dynamic period { get; set; }
            public Status status { get; set; }
            public TotalGains total_gains { get; set; }
            public TaggableTotals taggable_totals { get; set; }
            public CategoriesTotals categories_totals { get; set; }
        }









    }
}
