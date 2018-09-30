﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

/// <summary>
/// Summary description for clsEncryption
/// </summary>
public class clsEncryption
{
    private byte[] key = { };
    private byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

    public clsEncryption()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public string Decrypt(string stringToDecrypt, string sEncryptionKey)
    {
        stringToDecrypt = FromHex(stringToDecrypt);
        byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
        try
        {
            key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(stringToDecrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public string Encrypt(string stringToEncrypt, string SEncryptionKey)
    {
        try
        {
            key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return ToHex(Convert.ToBase64String(ms.ToArray()));
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }


    private string ToHex(String input)
    {
        String hexOutput = "";
        char[] values = input.ToCharArray();
        foreach (char letter in values)
        {
            // Get the integral value of the character.
            int value = Convert.ToInt32(letter);
            // Convert the decimal value to a hexadecimal value in string form.
            hexOutput += String.Format("{0:X}", value);
        }
        return hexOutput;

    }

    public static String FromHex(string hex)
    {
        hex = hex.Replace("-", "");
        byte[] raw = new byte[hex.Length / 2];
        for (int i = 0; i < raw.Length; i++)
        {
            raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        }
        return Encoding.ASCII.GetString(raw); ;
    }

}