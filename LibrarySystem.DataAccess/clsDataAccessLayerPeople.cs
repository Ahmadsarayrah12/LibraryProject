using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.DataAccess
{
    internal class clsDataAccessLayerPeople
    {
////   
////    PersonID INT IDENTITY(1,1) PRIMARY KEY,
////    FirstName  NVARCHAR(50) NOT NULL,
////    LastName   NVARCHAR(50) NOT NULL,
////    Phone      NVARCHAR(20),
////    Email NVARCHAR(100)
//// 

         enum enMode  {AddNew=1,Update=2};
       public int PersonID {  get; private set; }
       public string FirstName { get;  set; }

        public string LastName { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }


        private enMode _Mode=enMode.AddNew;
       public clsDataAccessLayerPeople()
        {
            this.PersonID = -1;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
            this._Mode = enMode.AddNew;
 
        }
        public clsDataAccessLayerPeople(string FirstName,string LastName,string Phone,string Email)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Phone = Phone;
            this.Email = Email;
            this._Mode = enMode.Update;


        }

    }



}
