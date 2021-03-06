﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;


namespace TEAM4OIES.Models
{

    class AccountModels
    {
        //Artifact Name: account login model
        //DBA: Logan Stark
        //TM: Janaye Maggart
        //Date: 4/27/2015
        //approval:Paul Miller
        //approval date:4/29/15
        public String accountLogin(String username, String password)
        {
            string connectionString = "Data Source=sqlserver.cs.uh.edu,1044;Initial Catalog=TEAM4OIES;User ID=TEAM4OIES;Password=TEAM4OIES#";
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    string sql = "SELECT Surgeon.userType,Surgeon.SurgeonID FROM Surgeon WHERE Surgeon.username = @username AND Surgeon.password = @password";
                    SqlCommand getClassification = new SqlCommand(sql, cnn);
                    getClassification.Parameters.AddWithValue("@username", username);
                    getClassification.Parameters.AddWithValue("@password", password);

                    SqlDataReader reader = getClassification.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        return ""+reader[0]+reader[1];
                    }
                    cnn.Close();
                    return "0-1";
                }
                catch
                {
                    //ignored
                }
                finally
                {
                    cnn.Close();
                }
                return "0-1";
            }
        }

        //Artifact Name: account register model
        //DBA: Logan Stark
        //TM: Janaye Maggart
        //Date: 4/27/2015
        //approval: Paul Miller
        //approval date: 4/29/15
        public Boolean createAccount(int userT, String firstN, String lastN, String usern, String passw, String mail, int institution)
        {

            //Artifact Name: Registration fix
            //DB: Logan Stark
            //Date: 5/4/2015
            //approval: Paul Miller
            //apprval Date:5/4/2015
            UC9DataContext db = new UC9DataContext();
            Surgeon surg = new Surgeon
            {
                userType = userT,
                firstName = firstN,
                lastName = lastN,
                username = usern,
                password = passw,
                email = passw,
                institution_id = institution,
                online = false,
                active = false
            };
            db.Surgeons.InsertOnSubmit(surg);

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            /*
            string connectionString = "Data Source=sqlserver.cs.uh.edu,1044;Initial Catalog=TEAM4OIES;User ID=TEAM4OIES;Password=TEAM4OIES#";
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    //RegisterModel surgeon = new RegisterModel(); 
                    String sql = "INSERT INTO Surgeon(userType,firstName,lastName,username,password,email,institution_id,online,active) VALUES(@userType,@firstName,@lastName,@username,@password,@email,@institution_id,@online,@active)";
                    SqlCommand registerSurgeon = new SqlCommand(sql, cnn);
                   // var newUserType = connectionString.registerSurgeon.Single(u => u.userType == userType);
                    registerSurgeon.Parameters.AddWithValue("@userType", userType);
                    registerSurgeon.Parameters.AddWithValue("@firstName", firstName);
                    registerSurgeon.Parameters.AddWithValue("@lastName", lastName);
                    registerSurgeon.Parameters.AddWithValue("@username", username);
                    registerSurgeon.Parameters.AddWithValue("@password", password);
                    registerSurgeon.Parameters.AddWithValue("@email", email);
                    registerSurgeon.Parameters.AddWithValue("@institution_id", institution_id);
                    registerSurgeon.Parameters.AddWithValue("@online", '0');
                    registerSurgeon.Parameters.AddWithValue("@active", '0');
                    registerSurgeon.BeginExecuteNonQuery();
                    cnn.Close();
                    return true;
                }
                catch
                {
                    //ignore
                }
                finally
                {
                    cnn.Close();
                }
            }
            return false;*/
        }

    }

    //#region Contexts
    //public partial class TEAM4OIESEntities : ObjectContext
    //{
    //    #region Constructors

    //    /// <summary>
    //    /// Initializes a new bowlingdatabaseEntities object using the connection string found in the 'bowlingdatabaseEntities' section of the application configuration file.
    //    /// </summary>
    //    public TEAM4OIESEntities()
    //        : base("name=TEAM4OIESEntities", "TEAM4OIESEntities")
    //    {
    //        this.ContextOptions.LazyLoadingEnabled = true;
    //        OnContextCreated();
    //    }

    //    /// <summary>
    //    /// Initialize a new bowlingdatabaseEntities object.
    //    /// </summary>
    //    public TEAM4OIESEntities(string connectionString)
    //        : base(connectionString, "TEAM4OEISEntities")
    //    {
    //        this.ContextOptions.LazyLoadingEnabled = true;
    //        OnContextCreated();
    //    }

    //    /// <summary>
    //    /// Initialize a new bowlingdatabaseEntities object.
    //    /// </summary>
    //    public TEAM4OIESEntities(EntityConnection connection)
    //        : base(connection, "TEAM4Entities")
    //    {
    //        this.ContextOptions.LazyLoadingEnabled = true;
    //        OnContextCreated();
    //    }
    //}

    //#endregion

    #region Models
    [PropertiesMustMatch("NewPassword", "ConfirmPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Current password")]
        public string OldPassword { get; set; }

        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm new password")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }
    }

    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "The password and confirmation password do not match.")]
    public class RegisterModel
    {

        [Required]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email address")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Institution")]
        public string Institution { get; set; }


    }
    #endregion

    #region Services
    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.

    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");

            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
    }

    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
    #endregion

    #region Validation
    public static class AccountValidation
    {
        public static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                //case MembershipCreateStatus.DuplicateUserName:
                //    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class PropertiesMustMatchAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}' and '{1}' do not match.";
        private readonly object _typeId = new object();

        public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
            : base(_defaultErrorMessage)
        {
            OriginalProperty = originalProperty;
            ConfirmProperty = confirmProperty;
        }

        public string ConfirmProperty { get; private set; }
        public string OriginalProperty { get; private set; }

        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                OriginalProperty, ConfirmProperty);
        }

        public override bool IsValid(object value)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            object originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
            object confirmValue = properties.Find(ConfirmProperty, true /* ignoreCase */).GetValue(value);
            return Object.Equals(originalValue, confirmValue);
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}' must be at least {1} characters long.";
        private readonly int _minCharacters = Membership.Provider.MinRequiredPasswordLength;

        public ValidatePasswordLengthAttribute()
            : base(_defaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                name, _minCharacters);
        }

        public override bool IsValid(object value)
        {
            string valueAsString = value as string;
            return (valueAsString != null && valueAsString.Length >= _minCharacters);
        }
    }
    #endregion

}
