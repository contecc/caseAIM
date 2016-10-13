using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using Microsoft.SharePoint.Client;
using Microsoft.Office.Client;

namespace caseAIM
{
    class MainPageData:INotifyPropertyChanged
    {

        public MainPageData()
        {
            if (!Convert.ToBoolean(SelectedCase))
            {
                LoadData();
            }
        }


   
        private void LoadData()
        { 

            
            var targetSite = new Uri("https://chsflorida.sharepoint.com/sites/cases/");
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
                List caseList = clientContext.Web.Lists.GetByTitle("Case Master");

                // This creates a CamlQuery that has a RowLimit of 100, and also specifies Scope="RecursiveAll" 
                // so that it grabs all list items, regardless of the folder they are in. 
                CamlQuery query = CamlQuery.CreateAllItemsQuery(10);
                ListItemCollection items = caseList.GetItems(query);
                clientContext.Load(items);

                //Fire the task, populate the results
                Task webTask = clientContext.ExecuteQueryAsync();
                webTask.Wait();

                //Populate the data
                foreach (ListItem listItem in items)
                {


                    /*
                    <Query>
                    <GroupBy Collapse = "TRUE" GroupLimit="30">
                       <FieldRef Name = "Case_x0020_Name" />
                    </ GroupBy >
    
                    < OrderBy >
                        <FieldRef Name="Case_x0020_ID"/>
                        <FieldRef Name = "Person_x0020_ID" />
                    </ OrderBy >

                    </ Query >
    
                    < ViewFields >
                    < FieldRef Name="Kid_x0020_Page"/>
                    < FieldRef Name = "Case_x0020_Name" />
                    < FieldRef Name="Child_x0020_Name"/>
                    < FieldRef Name = "Child_x0020_DOB" />
                    < FieldRef Name="Child_x0020_SSN"/>
                    < FieldRef Name = "Medicaid_x0020_Number" />
                    < FieldRef Name="Case_x0020_Worker"/>
                    < FieldRef Name = "Goal_x0028_Primary_x0029_" />
                    < FieldRef Name="Legal_x0020_Status_x0028_1_x0029"/>
                    < FieldRef Name = "Current_x0020_Location_x0020_Typ" />
                    < FieldRef Name="Provider_x0020_Name"/>
                    </ViewFields>
    
                    < RowLimit Paged = "TRUE" > 30 </ RowLimit >
                    < Aggregations Value="Off"/>
                    */

                    string caseName = listItem["Case_x0020_Name"].ToString();
                    string caseID = listItem["Case_x0020_ID"].ToString();      

                    string legalStatus1 = Convert.ToString(listItem["Legal_x0020_Status_x0028_1_x0029"]);
                    string currentLocationType = listItem["Current_x0020_Location_x0020_Typ"].ToString(); 
                    string providerName = listItem["Provider_x0020_Name"].ToString();
                    string personID = listItem["Person_x0020_ID"].ToString();  
                    string kidPage = listItem["Kid_x0020_Page"].ToString(); 
                    string caseNumber = listItem["Title"].ToString();
                    string childName = listItem["Child_x0020_Name"].ToString();
                    string childDOB = listItem["Child_x0020_DOB"].ToString(); 
                    string childSSN = listItem["Child_x0020_SSN"].ToString(); 

                    string medicaidNumber = ""; 
                    if (!String.IsNullOrEmpty(Convert.ToString(listItem["Medicaid_x0020_Number"])))
                    {
                        medicaidNumber = listItem["Medicaid_x0020_Number"].ToString();
                    }

                    string legalStatus = ""; 
                    if (!String.IsNullOrEmpty(Convert.ToString(listItem["Legal_x0020_Status_x0028_1_x0029"])))
                    {
                        legalStatus = listItem["Legal_x0020_Status_x0028_1_x0029"].ToString();
                    }
                     
                    string caseWorker = listItem["Case_x0020_Worker"].ToString();  // KEY Value for filtering

                    string streetAddress = listItem["Street_x0020_Address"].ToString();
                    string city = listItem["City"].ToString();
                    string state = listItem["State"].ToString();
                    string zipCode = listItem["Zip"].ToString();
                    string phoneNumber = "<phone>"; // listItem["Phone_x0020_#"].ToString();

                    string goalPrimary = "";
                    if (!String.IsNullOrEmpty(Convert.ToString(listItem["Goal_x0028_Primary_x0029_"])))
                    {
                        goalPrimary = listItem["Goal_x0028_Primary_x0029_"].ToString();
                    }
                    if (caseName != currentCaseName)
                    {
                            currentCaseName = caseName;

                            //Add kid to the kids array
                            caseAIM.Kid myKid = new Kid(childName, "<first>", streetAddress, city, state, zipCode, phoneNumber);
                            currentKids.Add(myKid);

                            List<caseAIM.Kid> theKids = new List<Kid>();
                             theKids = currentKids;

                            //logic for case
                            _cases.Add(new CaseModel(caseName, caseNumber, caseWorker, childName, theKids, goalPrimary, legalStatus, currentLocationType, providerName, caseID, childDOB, childSSN, kidPage, medicaidNumber, personID));

                            //currentKids.Clear(); 
                    }else
                    {
                        //Add kid to the kids array
                        caseAIM.Kid myKid = new Kid(childName, "<first>", streetAddress, city, state, zipCode, phoneNumber);
                        currentKids.Add(myKid);
                    }
                }
                siteGroups = items.Count().ToString();
                Greeting  = web.Title;             
            }

        }

        /*Property with shortcut value set, instead of longer set in contrstructor;*/
        private string currentCaseName;
        private List<caseAIM.Kid> currentKids = new List<caseAIM.Kid>();
        public string siteGroups { get; set; }
        public List<CaseModel> Cases {
            get { return _cases; }
        }

        private List<CaseModel> _cases = new List<CaseModel>();

        private string _greeting;
        public string Greeting
        {
            get { return _greeting; }
            set
            {
                if (value == _greeting)
                    return;

                _greeting = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Greeting)));
                

            }
        }
        private CaseModel _selectedCase;
        public event PropertyChangedEventHandler PropertyChanged;
        public CaseModel SelectedCase {

            get { return _selectedCase;  }

            set
            {
                _selectedCase = value;
                 PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCase)));
            }

        }



    } //end class
} //end nameSpace
