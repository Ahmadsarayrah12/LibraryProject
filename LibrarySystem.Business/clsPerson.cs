using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.DataAccess;
namespace LibrarySystem.Business
{
    public class clsPerson
    {

        enum enMode { AddNew=1, Update=2}

        public int PersonID {  get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone {  get; set; }
        public string Email { get; set; }

        enMode _Mode = enMode.AddNew;

        public clsPerson()
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


        public static clsPerson FindPerson(int PersonID)
        {
            string FirstName="";
            string LastName = "";
            string Phone = "";
            string Email = "";

            bool isExist = clsDataAccessLayerPeople. GetPersonInfoByID(PersonID, ref FirstName,
            ref LastName, ref Phone, ref Email);

            if (isExist)
            {
                
                return new clsPerson(PersonID,FirstName,LastName,Phone,Email);

            }
            
            return null;
    
        } 

        private bool _AddPerson()
        {

              this.PersonID = clsDataAccessLayerPeople.AddNewPerson(this.FirstName,this.LastName,this.Phone ,this.Email);

            return  (this.PersonID >0);
        }

        private bool _Update()
        {
         return   clsDataAccessLayerPeople.UpdatePerson(PersonID,FirstName,LastName,Phone,Email);

        }

        public bool Save()
        {

            switch(this._Mode) 
            {
            
                case enMode.AddNew:

                     
                    if (_AddPerson())
                    {
                        this._Mode = enMode.Update;
                        return true;

                    }
                    return false;

                case enMode.Update:
                    return _Update();



                default:

                    return false;
            
            
            }

        }

        public static bool Delete(int personID)
        {
            return clsDataAccessLayerPeople.DeletePerson(personID);
        }

        public static DataTable GetAllPeople()
        {
            return clsDataAccessLayerPeople.GetAllPeople();
        }

        public static bool IsPersonExist(int personID)
        {
            return clsDataAccessLayerPeople.IsPersonExist(personID);
        }

    }
}
