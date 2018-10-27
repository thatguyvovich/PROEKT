
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberPushkin.dao.ado.connectors;
using CyberPushkin.config;
using CyberPushkin.dao.ado;

namespace CyberPushkin.dao.factories
{
    public class ADODAOFactory
    {
        private ADOSQLiteConnector connector;



        static ADODAOFactory getDaoFactory(Config config)
        {
            ADODAOFactory daoFactory = null;

            // create DAO factory depends on config values
            //

            try
            {
                if (config.getDaoType().Equals("ado"))
                {
                    // create connector depends on config value
                    //

                    ADOSQLiteConnector Connector = null;

                    if (config.getDBMSType().Equals("sqlite"))
                    {
                        Connector = new ADOSQLiteConnector(config.getDbPath() + "/" + config.getSQLiteDbFileName());
                    }

                    // create factory
                    daoFactory = new ADODAOFactory(Connector);
                }
            }
            catch (ArgumentException e)
            {
                return null;
            }

            return daoFactory;
        }

        ADODAOFactory(ADOSQLiteConnector connector)
        {
            if (connector == null)
            {
                throw new ArgumentException();
            }

            this.connector = connector;
        }
        public VerseDAO verseDAO()
        {
            return new ADOVerseDAO(connector);
        }


        public PositionDAO positionDAO()
        {
            return new ADOPositionDAO(connector);
        }


        public VocabularyWordDAO vocabularyWordDAO()
        {
            return new ADOVocabularyWordDAO(connector);
        }


        public StorageCreator storageCreator()
        {
            return new ADOBaseCreator(connector);
        }
    }
}
