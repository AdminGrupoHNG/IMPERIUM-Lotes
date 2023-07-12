using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DA_GestionLotes;

namespace BL_GestionLotes
{
    public class blEncrypta
    {
        readonly daEncrypta _crypt;
        public blEncrypta(string key) { _crypt = new daEncrypta(key); }

        public string Encrypta(string valor) { return _crypt.Encrypta(valor); }
        public string Desencrypta(string valor) { return _crypt.Desencrypta(valor); }


        #region Espacio para encriptado - appSettings
        public void EncryptaAppSettings()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes == null || node.Attributes.Count == 0) continue;

                        node.Attributes[0].Value = _crypt.Encrypta(node.Attributes[0].Value.ToString());
                        node.Attributes[1].Value = _crypt.Encrypta(node.Attributes[1].Value.ToString());
                    }
                }
            }
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            ConfigurationManager.RefreshSection("appSettings");
        }
        public void DesencryptaAppSettings()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes == null || node.Attributes.Count == 0) continue;

                        node.Attributes[0].Value = _crypt.Desencrypta(node.Attributes[0].Value.ToString());
                        node.Attributes[1].Value = _crypt.Desencrypta(node.Attributes[1].Value.ToString());
                    }
                }
            }
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion
    }
}
