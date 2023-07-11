using System;
using System.Text;
using System.Security.Cryptography;
using fPeerLending.Entities;
using fPeerLending.Business;
using fanikiwaGL.Entities;

namespace ffwebAdminUI
{
    public class HashHelper
    {
        /// <summary>
        /// Create random salt
        /// </summary>
        /// <returns></returns>
        public static string CreateRandomSalt()
        {
            Byte[] saltBytes = new Byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Compute salted password hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string ComputeSaltedHash(string password, string salt)
        {
            // Create Byte array of password string
            UnicodeEncoding encoder = new UnicodeEncoding();
            Byte[] secretBytes = encoder.GetBytes(password);

            // Create a new salt
            Byte[] saltBytes = Convert.FromBase64String(salt);

            // append the two arrays
            Byte[] toHash = new Byte[secretBytes.Length + saltBytes.Length];
            Array.Copy(secretBytes, 0, toHash, 0, secretBytes.Length);
            Array.Copy(saltBytes, 0, toHash, secretBytes.Length, saltBytes.Length);

            SHA512 sha512 = SHA512.Create();
            Byte[] computedHash = sha512.ComputeHash(toHash);

            return Convert.ToBase64String(computedHash);
        }
    }

    public class AccountSelectEventArgs : System.EventArgs
    {

        // add local member variables to hold text        
        private Account _account;

        // class constructor
        public AccountSelectEventArgs(Account account)
        {

            this._account = account;
        }

        // Properties - Viewable by each listener         
        public Account _Account
        {
            get
            {
                return _account;
            }
        }
    }
     
    public class CustomerSelectEventArgs : System.EventArgs
    {

        // add local member variables to hold text
        private Customer _customers;

        // class constructor
        public CustomerSelectEventArgs(Customer customer)
        {

            this._customers = customer;
        }

        // Properties - Viewable by each listener
        public Customer _Customer
        {
            get
            {
                return _customers;
            }
        }
    }



}