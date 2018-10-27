using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace CyberPushkin.config
{

    class Config
    {
        private static Config instance;
        //private  Properties properties = new Properties();
        private ArrayList arrPropriates = new ArrayList();

        private Config()
        {

            try
            {
                StreamReader objReader = new StreamReader("./ config / config.ini");
                string sLine = "";
                

                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        arrPropriates.Add(sLine);
                }
                objReader.Close();
            }
            catch { }
        }

        static Config getInstance()
        {
            if (instance == null)
            {
                instance = new Config();
            }

            return instance;
        }

        bool isLoaded()
        {
            return arrPropriates.Count > 0;
        }

        public String getDbPath()
        {
            return getProperty("db_path");
        }

        public String getDaoType()
        {
            return getProperty("dao_type");
        }

        public String getDBMSType()
        {
            return getProperty("dbms_type");
        }

        public String getSQLiteDbFileName()
        {
            return getProperty("sqlite_db_filename");
        }

        public String getNGramStrategy()
        {
            return getProperty("ngram_strategy");
        }
        private String getProperty(String propert)
        {
            foreach(String p in arrPropriates)
            {
                if (p.Contains(propert))
                {
                    return p.Substring(propert.Length+1);
                }
            }
            return "";
        }


    }
}
