package com.sp.utils;

import java.io.File;
import java.io.FileOutputStream;
import java.io.ObjectOutputStream;
import java.security.KeyPair;
import java.security.KeyPairGenerator;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.security.PrivateKey;
import java.security.PublicKey;

import javax.crypto.Cipher;

public class CryptoUtil {
	  static final String ALGORITHM = "RSA";
	  static final String PRIVATE_KEY_FILE = "pKey.key";
	  static final String PUBLIC_KEY_FILE = "";
    
    public static String hash256(String data) throws NoSuchAlgorithmException {
        
        String phrase = "";  // this is the password
         phrase = data ;//Saltk + phrase;
        MessageDigest md = MessageDigest.getInstance("SHA-256");    //SHA-256"
        md.update(phrase.getBytes());
 
        byte byteData[] = md.digest();
 
        StringBuffer hexString = new StringBuffer();
    	for (int i=0;i<byteData.length;i++) {
    		String hex=Integer.toHexString(0xff & byteData[i]);
   	     	if(hex.length()==1) hexString.append('0');
   	     	hexString.append(hex);
    	}
        return hexString.toString();
    	//System.out.println("Hex format : " + hexString.toString());
        
    }
      
    public static void generateKey() {
        try {
            final KeyPairGenerator keyGen = KeyPairGenerator.getInstance(ALGORITHM);
            keyGen.initialize(1024);
            final KeyPair key = keyGen.generateKeyPair();

            File privateKeyFile = new File(PRIVATE_KEY_FILE);
            File publicKeyFile = new File(PUBLIC_KEY_FILE);

            // Create files to store public and private key
            if (privateKeyFile.getParentFile() != null) {
                privateKeyFile.getParentFile().mkdirs();
            }
            privateKeyFile.createNewFile();

            if (publicKeyFile.getParentFile() != null) {
                publicKeyFile.getParentFile().mkdirs();
            }
            publicKeyFile.createNewFile();

//            // Saving the Public key in a file
//            ObjectOutputStream publicKeyOS = new ObjectOutputStream(
//                    new FileOutputStream(publicKeyFile));
//            publicKeyOS.writeObject(key.getPublic());
//            publicKeyOS.close();
//
//            // Saving the Private key in a file
//            ObjectOutputStream privateKeyOS = new ObjectOutputStream(
//                    new FileOutputStream(privateKeyFile));
//            privateKeyOS.writeObject(key.getPrivate());
//            privateKeyOS.close();
        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    public static boolean areKeysPresent() {

        File privateKey = new File(PRIVATE_KEY_FILE);
        File publicKey = new File(PUBLIC_KEY_FILE);

        if (privateKey.exists() && publicKey.exists()) {
            return true;
        }
        return false;
    }

    public static byte[] encrypt(String text, PublicKey key) {
        byte[] cipherText = null;
        try {
            // get an RSA cipher object and print the provider
            final Cipher cipher = Cipher.getInstance(ALGORITHM);
            // encrypt the plain text using the public key
            cipher.init(Cipher.ENCRYPT_MODE, key);
            cipherText = cipher.doFinal(text.getBytes());
        } catch (Exception e) {
            e.printStackTrace();
        }
        return cipherText;
    }

    public static String decrypt(byte[] text, PrivateKey key) {
        byte[] dectyptedText = null;
        try {
            // get an RSA cipher object and print the provider
            final Cipher cipher = Cipher.getInstance(ALGORITHM);

            // decrypt the text using the private key
            cipher.init(Cipher.DECRYPT_MODE, key);
            dectyptedText = cipher.doFinal(text);

        } catch (Exception ex) {
            ex.printStackTrace();
        }

        return new String(dectyptedText);
    }
    
      private static String hex(String sPub) {
        sPub.toString();
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }
    
}
