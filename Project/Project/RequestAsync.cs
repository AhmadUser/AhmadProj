using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class RequestAsync
    {
        public static string Admin { get; set; }
        public static async Task<bool> loginRequestAsync(string adminInput, string passInput)
        {
            try
            {
                HttpClient request = new HttpClient();
                Admin = adminInput;
                string api = ApiServices.LOGIN_API;
                string arguments = "?Admin=" + adminInput + "&Password=" + passInput;
                api += arguments;
                HttpResponseMessage response = await request.GetAsync(api);
                string responseCode=response.StatusCode.ToString();
                MainWindow.writeToLogs("=============" + responseCode);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Authentication>(responseBody);
                return result.Authenticated;
            }
            catch (Exception ex)
            {
                MainWindow.writeToLogs("LOGIN EXCEPTION \n" + ex.Message);
                return false;
            }
        }
        public static async Task<bool> addCustomerAsync(Customer customer)
        {
            
            try
            {
                HttpClient client = new HttpClient();
                string request = ApiServices.ADD_CUSTOMERS_API;
                string arguments = "?Admin=" + Admin + "&FirstName=" + customer.FirstName+"&LastName="+customer.LastName+"&NickName="+customer.NickName+ "&Telephone="+customer.Telephone+ "&AddressID="+customer.AddressID;
                request += arguments;
                HttpResponseMessage response = await client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Authentication>(responseBody);
                return true;
            }
            catch (Exception ex)
            {
                MainWindow.writeToLogs("AddCustomer EXCEPTION \n" + ex.Message);
                return false;
            }

        }

        public static async Task<int> addAddressAsync(Address address)
        {
           
            try
            {
                HttpClient client = new HttpClient();
                string request = ApiServices.ADD_ADDRESS_API;
                string arguments = "?Admin=" + Admin + "&StreetName=" + address.StreetName + "&BuildingName=" + address.BuildingName + "&Floor=" + address.Floor + "&NearSomePlace=" + address.NearSomePlace + "&MoreDetails=" + address.MoreDetails;
                request += arguments;
                HttpResponseMessage response = await client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AddAddressResponse>(responseBody);
                MainWindow.writeToLogs("Resultttt===========" + result.ToString());
                return result.Id ;
            }
            catch (Exception ex)
            {
                MainWindow.writeToLogs("Add address exception  "+ex.Message);
                return 0;
            }
        }
     
        public static  async Task<bool> newItemAsync(Item item)
        {
            bool succeed = true;
            try
            {
                HttpClient client = new HttpClient();
                string request = ApiServices.ADD_ITEM_API;
                request += "?Barcode=" + item.Barcode + "&InitialPrice=" + item.InitialPrice + "&SellingPrice=" + item.SellingPrice + "&PrivateName=" + item.PrivateName + "&CompanyName=" + item.CompanyName + "&Admin=" + Admin + "&Quantity=" + item.Quantity;
                HttpResponseMessage response = await client.GetAsync(request);
            }catch(Exception ex)
            {
                MainWindow.writeToLogs("ADD NEW ITEM EXCEPTION \n"+ex.Message);
                succeed = false;
            }
            return succeed;
        }
        public static async Task<List<Item>> getItems()
        {
            List<Item> items = new List<Item>();
            try
            {
                HttpClient client = new HttpClient();
                string request = ApiServices.GET_ITEMS_API;
                request += "?Admin=" + Admin;
                HttpResponseMessage response = await client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseBody);
                foreach (var item in result)
                {
                    Item temp=new Item();
                    try
                    {
                        temp.InitialPrice = item.InitialPrice;
                    }
                    catch(Exception ex)
                    {
                        MainWindow.writeToLogs(ex.Message);
                        temp.InitialPrice = -1;
                    }
                    try
                    {
                        temp.SellingPrice = item.SellingPrice;
                    }
                    catch (Exception ex)
                    {
                        MainWindow.writeToLogs(ex.Message);
                        temp.SellingPrice = -1;
                    }
                    try
                    {
                        temp.PrivateName = item.PrivateName;
                    }
                    catch (Exception ex)
                    {
                        MainWindow.writeToLogs(ex.Message);
                        temp.PrivateName = "NA";
                    }
                    try
                    {
                        temp.Barcode = item.Barcode;
                    }
                    catch (Exception ex)
                    {
                        MainWindow.writeToLogs(ex.Message);
                        temp.Barcode = -1;
                    }
                    try
                    {
                        temp.CompanyName = item.CompanyName;
                    }
                    catch (Exception ex)
                    {
                        MainWindow.writeToLogs(ex.Message);
                        temp.CompanyName = "NA";
                    }
                    try
                    {
                        temp.Quantity = item.quantity;
                    }
                    catch (Exception ex)
                    {
                        MainWindow.writeToLogs(ex.Message);
                        temp.Quantity = "NA";
                    }
                    items.Add(temp);
                }
                return items;
            }catch(Exception ex)
            {
                MainWindow.writeToLogs("GET ALL ITEM EXCEPTION \n" + ex.Message);
                return null;
            }

        }
        public static async Task<List<Customer>> getCustomers()
        {
            List<Customer> cutomers = new List<Customer>();
            try
            {
                HttpClient client = new HttpClient();
                string request = ApiServices.GET_CUSTOMERS_API;
                request += "?Admin=" + Admin;
                HttpResponseMessage response = await client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseBody);
                foreach (var item in result)
                {
                    Customer temp = new Customer();
                    try
                    {
                        temp.FirstName = item.FirstName;
                    }
                    catch (Exception ex)
                    {
                        MainWindow.writeToLogs(ex.Message);
                        temp.FirstName = "NA";
                    }
                    try
                    {
                        temp.LastName = item.LastName;
                    }
                    catch (Exception ex)
                    {
                        MainWindow.writeToLogs(ex.Message);
                        temp.LastName = "NA";
                    }
                    try
                    {
                        temp.NickName = item.NickName;
                    }
                    catch (Exception ex)
                    {
                        MainWindow.writeToLogs(ex.Message);
                        temp.NickName = "NA";
                    }
                    try
                    {
                        temp.Telephone = item.Telephone;
                    }
                    catch (Exception ex)
                    {
                        MainWindow.writeToLogs(ex.Message);
                        temp.Telephone = ""+-1;
                    }
                    try
                    {
                        temp.AddressID = item.AddressID;
                    }
                    catch (Exception ex)
                    {
                        MainWindow.writeToLogs(ex.Message);
                        temp.AddressID = 0;
                    }

                    cutomers.Add(temp);
                }
                return cutomers;
            }
            catch (Exception ex)
            {
                MainWindow.writeToLogs("GET ALL ITEM EXCEPTION \n" + ex.Message);
                return null;
            }

        }
        public class Authentication
        {
            public bool Authenticated { get; set; }
        }
        public class AddAddressResponse
        {
            public int Id { get; set; }
        }
        public class Response
        {
            public bool Succeeds { get; set; }
        }
    }
}
