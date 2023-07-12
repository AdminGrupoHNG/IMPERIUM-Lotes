using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DA_GestionLotes
{
    public class daEncrypta
    {
        private string _key; //= "b14ca5898a4e4133bbce2ea2315a1916";// "HNG-Soft"; // crear llave de 32 caracteres y registrarlo en la db, cambiar llave cada version
        public daEncrypta(string key)
        {
            _key = key;
        }
        public string Encrypta(string valor)
        {
            byte[] iv = new byte[16];
            byte[] array;

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(_key);
                    aes.IV = iv;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                            {
                                streamWriter.Write(valor);
                            }

                            array = memoryStream.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }

            return Convert.ToBase64String(array);
        }
        public string Desencrypta(string valor)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(valor);

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(_key);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        /*  public string Encrypta(string valor)
          {
              try
              {
                  byte[] plainContent = File.ReadAllBytes(valor);
                  using (var DES = new DESCryptoServiceProvider())
                  {
                      DES.IV = Encoding.UTF8.GetBytes(key);
                      DES.Key = Encoding.UTF8.GetBytes(key);
                      DES.Mode = CipherMode.CBC;
                      DES.Padding = PaddingMode.PKCS7;

                      using (var memStream = new MemoryStream())
                      {
                          CryptoStream cryptoStream = new CryptoStream(memStream, DES.CreateEncryptor(),
                              CryptoStreamMode.Write);

                          cryptoStream.Write(plainContent, 0, plainContent.Length);
                          cryptoStream.FlushFinalBlock();
                          File.WriteAllBytes(filePath, memStream.ToArray());
                          // Console.WriteLine("Encrypted succesfully " + filePath);
                      }
                  }
                  return "";
              }
              catch (System.Exception ex)
              {
                  throw new System.Exception(ex.Message);
              }
          }*/
        /* public string Desencrypta(string valor)
         {
             try
             {
                 byte[] encrypted = File.ReadAllBytes(filePath);
                 using (var DES = new DESCryptoServiceProvider())
                 {
                     DES.IV = Encoding.UTF8.GetBytes(key);
                     DES.Key = Encoding.UTF8.GetBytes(key);
                     DES.Mode = CipherMode.CBC;
                     DES.Padding = PaddingMode.PKCS7;


                     using (var memStream = new MemoryStream())
                     {
                         CryptoStream cryptoStream = new CryptoStream(memStream, DES.CreateDecryptor(),
                             CryptoStreamMode.Write);

                         cryptoStream.Write(encrypted, 0, encrypted.Length);
                         cryptoStream.FlushFinalBlock();
                         File.WriteAllBytes(filePath, memStream.ToArray());
                         //  Console.WriteLine("Decrypted succesfully " + filePath);
                     }
                 }
                 return "";
             }
             catch (System.Exception ex)
             {
                 throw new System.Exception(ex.Message);
             }
         }
        */

        /*  
          //string provider = "DataProtectionConfigurationProvider";
          UnicodeEncoding ByteConverter = new UnicodeEncoding();

          public string Encrypta(string valor)
          {
              string result = "";
              //byte[] codigos = Encoding.ASCII.GetBytes(valor);
              //Byte[] nuevosCodigos = new Byte[codigos.Length];

              //for (int i = 0; i < codigos.Length; i++)
              //{
              //    nuevosCodigos[i] = Convert.ToByte(Convert.ToInt32(codigos[i]) - 3);
              //}
              //result = Encoding.ASCII.GetString(nuevosCodigos);

              result = valor;
              return result;
          }

          public string Desencrypta(string valor)
          {
              string result = "";
              //byte[] codigos = Encoding.ASCII.GetBytes(valor);
              //Byte[] nuevosCodigos = new Byte[codigos.Length];

              //for (int i = 0; i < codigos.Length; i++)
              //{
              //    nuevosCodigos[i] = Convert.ToByte(Convert.ToInt32(codigos[i]) + 3);
              //}
              //result = Encoding.ASCII.GetString(nuevosCodigos);

              result = valor;
              return result;
          }
          */
    }
}
