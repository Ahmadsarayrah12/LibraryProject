using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Business
{
    internal class clsPerson
    {

        enum enMode { AddNew=1, Update=2}

        public int PersonID {  get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone {  get; set; }
        public string Email { get; set; }

        enMode _Mode = enMode.AddNew;

        clsPerson()
        {

            this.PersonID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Phone = "";
            this.Email = "";
            this._Mode =enMode.AddNew;
        }

        private clsPerson(int PersonID,string FirstName,string LastName,string Phone,string Email)
        {

            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Phone = Phone;
            this.Email = Email;
            this._Mode = enMode.Update;

        }


        public bool FindUser(int PersonID)
        {


        } 

    }
}
