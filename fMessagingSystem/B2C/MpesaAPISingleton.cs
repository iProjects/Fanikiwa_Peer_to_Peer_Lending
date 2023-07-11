using B2C.MpesaServiceRef;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Timers;

namespace B2C
{
    public class MpesaAPISingleton
    {
        static Timer timer;
        String balanceInquiryXML = null;
        String fundsTransferXML = null;
        String miniStatementXML = null;
        String econnectresponce = "";

        public static readonly String ALGORITHM = "RSA";
        public static readonly String PRIVATE_KEY_FILE = "D:/vas\\Mwamba\\B2C_Rollout\\ApiMessageEncryptionKey.key";
        public static readonly String PUBLIC_KEY_FILE = "C:\\B2C\\B2C/bank.cer";
        public const int Sec = 30;

        public MpesaAPISingleton()
        {
            //Run();
        }

        public void Run()
        {
            try
            {
                Console.WriteLine("Sytem initializing in 3 Seconds...");
                SendB2C();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:  " + ex.Message);
            }
        }

        private void SendB2C()
        {
            //get unposted records from db
            //foreach record; PostToMpesa
        }

        private void MpesaRequest(string Amount, string MobileNo, string Stan)
        {
            // via webservice way

            DateTime dNow = DateTime.Now;

            DateTime dNow1 = DateTime.Now;

            String FinalDate = dNow1.ToString("yyyy-MM-dd HH:mm:ss");
            FinalDate = FinalDate.Substring(0, 10) + "T" + FinalDate.Substring(10, 19) + "Z";
            FinalDate = FinalDate.Replace(" ", "");


            String converted = dNow1.ToString("yyyy-MM-dd HH:mm:ss") + "T" + dNow1.ToString("HH:mm:ss.0000000") + "Z";
            String myCertPassword = "";
            String spid = "1000150";
            String Serviceid = "10001502";  //10302001
            String spPassword = "Password0!";//"Abcd123!@#";//"COOPbank@01";//"COOP@100001";
            String spPassword1 = "Password01";
            String mypassword = "";
            mypassword = spid + spPassword + dNow.ToString("yyyyMMddHHmmss");
            String pwd = "";
            String hashedpwd = hash256(mypassword);
            hashedpwd = hashedpwd.ToUpper();
            pwd = Convert.ToBase64String(Encoding.UTF8.GetBytes(hashedpwd)); //javax.xml.bind.DatatypeConverter.printBase64Binary(a.getBytes("UTF-8"));  //("UTF-8")

            try
            {
                String originalText = spPassword1;
                byte[] cipherText = encrypt(originalText);
                myCertPassword = Convert.ToBase64String(cipherText);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //do header
            RequestSOAPHeader header = new RequestSOAPHeader();
            header.serviceId = Serviceid;
            header.spId = spid;
            header.spPassword = spPassword;
            header.timeStamp = dNow.ToString("yyyy-MM-dd HH:mm:ss");

            //do message
            string msg = "<req:RequestMsg><![CDATA[<?xml version=\"1.0\" encoding=\"UTF8\"?>"
                        + "<request xmlns=\"http://api-v1.gen.mm.vodafone.com/mminterface/request\">"
                        + "<Transaction>"
                        + "<CommandID>BusinessPayment</CommandID>"
                        + "<LanguageCode></LanguageCode>"
                        + "<OriginatorConversationID>" + Stan + "</OriginatorConversationID>"
                        + "<ConversationID></ConversationID>"
                        + "<Remark>Remark0</Remark>"
                        + "<EncryptedParameters>EncryptedParameters0</EncryptedParameters>"
                        + "<Parameters>"
                        + "<Parameter>"
                        + "<Key>Amount</Key>"
                        + "<Value>" + Amount + "</Value>"
                        + "</Parameter>"
                        + "</Parameters>"
                        + "<ReferenceData>"
                        + "<ReferenceItem>"
                        + "<Key>QueueTimeoutURL</Key>"
                        + "<Value>http://IPADDRESS:8080/C2BWebService/C2B</Value>"
                        + "</ReferenceItem>"
                        + "</ReferenceData>"
                        + "<Timestamp>" + FinalDate + "</Timestamp>"
                        + "</Transaction>"
                        + "<Identity>"
                        + "<Caller>"
                        + "<CallerType>2</CallerType>"
                        + "<ThirdPartyID>ThirdPartyID0</ThirdPartyID>"
                        + "<Password>Password0</Password>"
                        + "<CheckSum>CheckSum0</CheckSum>"
                        + "<ResultURL>http://IPADDRESS:8080/C2BWebService/C2B</ResultURL>"
                        + "</Caller>"
                        + "<Initiator>"
                        + "<IdentifierType>11</IdentifierType>"
                        + "<Identifier>BANK12</Identifier>"
                        + "<SecurityCredential>" + myCertPassword + "</SecurityCredential>"
                        + "<ShortCode>1000150</ShortCode>"
                        + "</Initiator>"
                        + "<PrimaryParty>"
                        + "<IdentifierType>1</IdentifierType>"
                        + "<Identifier>BANK12</Identifier>"
                        + "<ShortCode></ShortCode>"
                        + "</PrimaryParty>"
                        + "<ReceiverParty>"
                        + "<IdentifierType>1</IdentifierType>"
                        + "<Identifier>" + MobileNo + "</Identifier>"
                        + "<ShortCode></ShortCode>"
                        + "</ReceiverParty>"
                        + "<AccessDevice>"
                        + "<IdentifierType>1</IdentifierType>"
                        + "<Identifier>Identifier3</Identifier>"
                        + "</AccessDevice>"
                        + "</Identity>"
                        + "<KeyOwner>1</KeyOwner>"
                        + "</request>]]>"
                        + "</req:RequestMsg>";

            //call this before calling the webservice
            ServicePointManager.ServerCertificateValidationCallback = TrustAllCertificateCallback;

            RequestMgrPortTypeClient client = new RequestMgrPortTypeClient();
            string response = client.GenericAPIRequest(header, msg);

        }

        private void PostToMpesa(String Amount, String MobileNo, String Stan)
        {
            DateTime dNow = DateTime.Now;

            DateTime dNow1 = DateTime.Now;

            String FinalDate = dNow1.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine("Current Date: " + FinalDate);
            FinalDate = FinalDate.Substring(0, 10) + "T" + FinalDate.Substring(10, 19) + "Z";
            Console.WriteLine("Final Date: " + FinalDate);
            FinalDate = FinalDate.Replace(" ", "");
            Console.WriteLine("Final Date: " + FinalDate);

            String converted = dNow1.ToString("yyyy-MM-dd HH:mm:ss") + "T" + dNow1.ToString("HH:mm:ss.0000000") + "Z";
            String myCertPassword = "";
            String spid = "1000150";
            String Serviceid = "10001502";  //10302001
            String spPassword = "Password0!";//"Abcd123!@#";//"COOPbank@01";//"COOP@100001";
            String spPassword1 = "Password01";
            String mypassword = "";
            mypassword = spid + spPassword + dNow.ToString("yyyyMMddHHmmss");
            String pwd = "";
            String hashedpwd = hash256(mypassword);
            hashedpwd = hashedpwd.ToUpper();
            pwd = Convert.ToBase64String(Encoding.UTF8.GetBytes(hashedpwd)); //javax.xml.bind.DatatypeConverter.printBase64Binary(a.getBytes("UTF-8"));  //("UTF-8")

            try
            {
                String originalText = spPassword1;
                byte[] cipherText = encrypt(originalText);
                myCertPassword = Convert.ToBase64String(cipherText);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            String soap = "<soap:Envelope xmlns:mrns0=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:req=\"http://api-v1.gen.mm.vodafone.com/mminterface/request\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\">"
                        + "<soap:Header>"
                        + "<tns:RequestSOAPHeader xmlns:tns=\"http://www.huawei.com/schema/osg/common/v2_1\">"
                        + "<tns:spId>" + spid + "</tns:spId>"
                        + "<tns:spPassword>" + pwd + "</tns:spPassword>"
                        + "<tns:serviceId>" + Serviceid + "</tns:serviceId>"
                        + "<tns:timeStamp>" + dNow.ToString("yyyy-MM-dd HH:mm:ss") + "</tns:timeStamp>"
                        + "</tns:RequestSOAPHeader>"
                        + "</soap:Header>"
                        + "<soap:Body>"
                        + "<req:RequestMsg><![CDATA[<?xml version=\"1.0\" encoding=\"UTF8\"?>"
                        + "<request xmlns=\"http://api-v1.gen.mm.vodafone.com/mminterface/request\">"
                        + "<Transaction>"
                        + "<CommandID>BusinessPayment</CommandID>"
                        + "<LanguageCode></LanguageCode>"
                        + "<OriginatorConversationID>" + Stan + "</OriginatorConversationID>"
                        + "<ConversationID></ConversationID>"
                        + "<Remark>Remark0</Remark>"
                        + "<EncryptedParameters>EncryptedParameters0</EncryptedParameters>"
                        + "<Parameters>"
                        + "<Parameter>"
                        + "<Key>Amount</Key>"
                        + "<Value>" + Amount + "</Value>"
                        + "</Parameter>"
                        + "</Parameters>"
                        + "<ReferenceData>"
                        + "<ReferenceItem>"
                        + "<Key>QueueTimeoutURL</Key>"
                        + "<Value>http://IPADDRESS:8080/C2BWebService/C2B</Value>"
                        + "</ReferenceItem>"
                        + "</ReferenceData>"
                        + "<Timestamp>" + FinalDate + "</Timestamp>"
                        + "</Transaction>"
                        + "<Identity>"
                        + "<Caller>"
                        + "<CallerType>2</CallerType>"
                        + "<ThirdPartyID>ThirdPartyID0</ThirdPartyID>"
                        + "<Password>Password0</Password>"
                        + "<CheckSum>CheckSum0</CheckSum>"
                        + "<ResultURL>http://IPADDRESS:8080/C2BWebService/C2B</ResultURL>"
                        + "</Caller>"
                        + "<Initiator>"
                        + "<IdentifierType>11</IdentifierType>"
                        + "<Identifier>BANK12</Identifier>"
                        + "<SecurityCredential>" + myCertPassword + "</SecurityCredential>"
                        + "<ShortCode>1000150</ShortCode>"
                        + "</Initiator>"
                        + "<PrimaryParty>"
                        + "<IdentifierType>1</IdentifierType>"
                        + "<Identifier>BANK12</Identifier>"
                        + "<ShortCode></ShortCode>"
                        + "</PrimaryParty>"
                        + "<ReceiverParty>"
                        + "<IdentifierType>1</IdentifierType>"
                        + "<Identifier>" + MobileNo + "</Identifier>"
                        + "<ShortCode></ShortCode>"
                        + "</ReceiverParty>"
                        + "<AccessDevice>"
                        + "<IdentifierType>1</IdentifierType>"
                        + "<Identifier>Identifier3</Identifier>"
                        + "</AccessDevice>"
                        + "</Identity>"
                        + "<KeyOwner>1</KeyOwner>"
                        + "</request>]]>"
                        + "</req:RequestMsg>"
                        + "</soap:Body>"
                        + "</soap:Envelope>";

            //get a reference to the site
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://IPADDRESS/mminterface/request");
            req.Headers.Add("SOAPAction", "https://IPADDRESS/mminterface/request");
            req.ContentType = "text/xml;charset=\"utf-8\"";
            req.Accept = "text/xml";
            req.Method = "POST";

            //make the call   
            using (Stream stm = req.GetRequestStream())
            {
                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(soap);
                }
            }

            //now get the response
            WebResponse response = req.GetResponse();

            String responseString;
            using (Stream rstream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(rstream, Encoding.UTF8);
                responseString = reader.ReadToEnd();
            }

            //display the response
            Console.WriteLine("request message: \n" + soap);
            Console.WriteLine("Response message: \n" + responseString);

        }


        public static bool TrustAllCertificateCallback(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
        {
            return true;

        }


        public static string hash256(string input)
        {
            var sha1 = SHA256Managed.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] outputBytes = sha1.ComputeHash(inputBytes);
            return BitConverter.ToString(outputBytes);
        }

        static string sha256(string text)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(text), 0, Encoding.UTF8.GetByteCount(text));
            foreach (byte bit in crypto)
            {
                hash += bit.ToString("x2");
            }
            return hash;
        }

        static public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.Encoding.UTF8.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        static public string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.Encoding.UTF8.GetString(encodedDataAsBytes);
            return returnValue;
        }


        #region RSA Utils
        public static void GenKey_SaveInContainer(string ContainerName)
        {
            // Create the CspParameters object and set the key container 
            // name used to store the RSA key pair.
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;

            // Create a new instance of RSACryptoServiceProvider that accesses
            // the key container MyKeyContainerName.
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024, cp);

            // Display the key information to the console.
            Console.WriteLine("Key added to container: \n  {0}", rsa.ToXmlString(true));
        }

        public static void GetKeyFromContainer(string ContainerName)
        {
            // Create the CspParameters object and set the key container 
            // name used to store the RSA key pair.
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;

            // Create a new instance of RSACryptoServiceProvider that accesses
            // the key container MyKeyContainerName.
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);

            // Display the key information to the console.
            Console.WriteLine("Key retrieved from container : \n {0}", rsa.ToXmlString(true));
        }

        public static void DeleteKeyFromContainer(string ContainerName)
        {
            // Create the CspParameters object and set the key container 
            // name used to store the RSA key pair.
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;

            // Create a new instance of RSACryptoServiceProvider that accesses
            // the key container.
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);

            // Delete the key entry in the container.
            rsa.PersistKeyInCsp = false;

            // Call Clear to release resources and delete the key from the container.
            rsa.Clear();

            Console.WriteLine("Key deleted.");
        }

        static public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider. 
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs 
                    //toinclude the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.   
                    //OAEP padding is only available on Microsoft Windows XP or 
                    //later.  
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException   
            //to the console. 
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        static public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider. 
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs 
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.   
                    //OAEP padding is only available on Microsoft Windows XP or 
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException   
            //to the console. 
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }

        #endregion





        public static bool areKeysPresent()
        {

            FileInfo privateKey = new FileInfo(PRIVATE_KEY_FILE);
            FileInfo publicKey = new FileInfo(PUBLIC_KEY_FILE);

            if (privateKey.Exists && publicKey.Exists)
            {
                return true;
            }
            return false;
        }

        public static byte[] encrypt(String text)
        {
            //Create a UnicodeEncoder to convert between byte array and string.
            UnicodeEncoding ByteConverter = new UnicodeEncoding();

            //Create byte arrays to hold original, encrypted, and decrypted data. 
            byte[] dataToEncrypt = ByteConverter.GetBytes(text);
            byte[] encryptedData;

            //Create a new instance of RSACryptoServiceProvider to generate 
            //public and private key data. 
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {

                //Pass the data to ENCRYPT, the public key information  
                //(using RSACryptoServiceProvider.ExportParameters(false), 
                //and a boolean flag specifying no OAEP padding.
                encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);
            }
            return encryptedData;

        }

        public static String decrypt(byte[] encryptedData)
        {
            //Create a UnicodeEncoder to convert between byte array and string.
            UnicodeEncoding ByteConverter = new UnicodeEncoding();

            //Create byte arrays to hold original, encrypted, and decrypted data. 
            byte[] decryptedData;

            //Create a new instance of RSACryptoServiceProvider to generate 
            //public and private key data. 
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {

                //Pass the data to DECRYPT, the private key information  
                //(using RSACryptoServiceProvider.ExportParameters(true), 
                //and a boolean flag specifying no OAEP padding.
                decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);
            }
            return ByteConverter.GetString(decryptedData);
        }


    }
}