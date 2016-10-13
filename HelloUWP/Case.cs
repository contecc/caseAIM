using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caseAIM
{
    class CaseModel
    {
        public string CaseName { get; }
        public string CaseID { get; }
        public string CaseNumber { get; }
        public string CaseOwner { get;  }
        public string GoalPrimary { get; }
        public string LegalStatus { get; }
        public string CurrentLocationType { get; }
        public string ProviderName { get; }
        public string ChildName { get; }
        public string ChildDOB { get; }
        public string ChildSSN { get; }
        public string KidPage { get; }
        public string MedicaidNumber { get; }
        public string PersonID { get; }


        //public IEnumerable<string> Names { get; }
        public IList<caseAIM.Kid> Kids { get; }

        public CaseModel(string caseName, string caseNumber, string caseOwner, string childName, IList<caseAIM.Kid> kids, string goalPrimary, string legalStatus, string currentLocationType, string providerName, string caseID, string childDOB, string childSSN, string kidPage, string medicaidNumber, string personID)
        {
            CaseName = caseName;
            CaseID = caseID;
            CaseNumber = caseNumber;
            CaseOwner = caseOwner;
            ChildName = childName;
            GoalPrimary = goalPrimary;
            LegalStatus = legalStatus;
            CurrentLocationType = currentLocationType;
            ProviderName = providerName;
            Kids = kids;
            //Names = names;
            ChildDOB = childDOB;
            ChildSSN = ChildSSN;
            KidPage = kidPage;
            MedicaidNumber = medicaidNumber;
            PersonID = personID;

        }

        //public string KidNames => string.Join(", ", Names);
           
    }
}
