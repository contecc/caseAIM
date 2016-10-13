using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace caseAIM
{
    public class ServiceProviderViewModel
    {
        public List<ServiceProviderModel> ServiceProviders
        {
            get { return _serviceProviders; }
        }

        public List<ServiceProviderModel> _serviceProviders = new List<ServiceProviderModel>();
        /// <summary>
        /// Used to prevent NULL values (and associated crashes)
        /// </summary>
        /// <param name="myListItem"></param>
        /// <returns>Returns either a blank string or the value of the List Item</returns>
        public string RemoveNulls(object myListItem)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(myListItem)))
            {
                return myListItem.ToString();
            }
            else
            {
                return "";  //return a blank string if Null
            }
        }
        


        public ServiceProviderViewModel()
        {
            var targetSite = new Uri("https://chsflorida.sharepoint.com/sites/cases/AIM");
            var login = "chris.conte@chsfl.org";
            var password = "Password1$";

            var onlineCredentials = new SharePointOnlineCredentials(login, password);

            using (ClientContext clientContext = new ClientContext(targetSite))
            {
                clientContext.Credentials = onlineCredentials;
                Web web = clientContext.Web;
                // clientContext.Load(web, webSite => webSite.Title);
                clientContext.Load(web);


                // Assume the web has a list named "Announcements". 
                List serviceProvidersList = clientContext.Web.Lists.GetByTitle("Providers");

                // This creates a CamlQuery that has a RowLimit of 100, and also specifies Scope="RecursiveAll" 
                // so that it grabs all list items, regardless of the folder they are in. 
                CamlQuery query = CamlQuery.CreateAllItemsQuery(50);
                ListItemCollection items = serviceProvidersList.GetItems(query);
                clientContext.Load(items);

                //Fire the task, populate the results
                Task webTask = clientContext.ExecuteQueryAsync();
                webTask.Wait();


                foreach (ListItem listItem in items)
                {
                    string serviceType = RemoveNulls(listItem["Title"]);
                    string providerName = RemoveNulls(listItem["Provider_x0020_Name"]);
                    string address = RemoveNulls(listItem["Address"]);
                    string suite = RemoveNulls(listItem["Suite_x0020_"]);
                    string city = RemoveNulls(listItem["City"]);
                    string state = RemoveNulls(listItem["State"]);
                    int zip = Convert.ToInt32(listItem["Zip"]);
                    string phone = RemoveNulls(listItem["Phone"]);
                    string fax = RemoveNulls(listItem["Fax"]);
                    string director = RemoveNulls(listItem["Director"]);
                    string servicePopulation = RemoveNulls(listItem["Service_x0020_Population"]);
                    string paymentAccepted = RemoveNulls(listItem["Payment_x0020_Accepted"]);
                    string serviceDescriptions = RemoveNulls(listItem["Service_x0020_Descriptions"]);
                    string website = RemoveNulls(listItem["Website"]);
                    string division = RemoveNulls(listItem["Division"]);

                   _serviceProviders.Add(new ServiceProviderModel(serviceType, providerName, address, suite, city, state, zip, phone, fax, 
                                                                  director, servicePopulation, paymentAccepted, serviceDescriptions, website, division));
                }

            }
        }  //end constructor

    } //end class
} //end namespace
