using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberPushkin.dao.ado.connectors;
using System.Data.SqlClient;
using CyberPushkin.model;

namespace CyberPushkin.dao.ado
{
    public class ADOVocabularyWordDAO : VocabularyWordDAO
    {


        private ADOSQLiteConnector connector;

        public ADOVocabularyWordDAO(ADOSQLiteConnector connector)
        {
            if (connector == null)
            {
                throw new ArgumentException();
            }

            this.connector = connector;
        }

       
  public List<VocabularyWord> getAll()
        {
            List<VocabularyWord> vocabularyWords = new List<VocabularyWord>();

            try {
                SqlConnection con = connector.getConnection();
                String sqlSelect = "SELECT Id, Value FROM Vocabulary";
                SqlCommand command = new SqlCommand(sqlSelect, con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vocabularyWords.Add(new VocabularyWord(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
            } catch (SqlException ignored)
            {
            }

            return vocabularyWords;
        }

        
  public void addAll(List<VocabularyWord> vocabulary) {
    if (vocabulary == null ||
        vocabulary.Count == 0) {
      throw new EmptyRecordException("Vocabulary is null or empty");
    }

    try {
      SqlConnection con = connector.getConnection();

                //con.setAutoCommit(false);

                // prepare sql query
                //

                String sqlInsert; 

      //

      foreach (VocabularyWord vocabularyWord in vocabulary) {
        // check
        //

        if (vocabularyWord == null ||
            vocabularyWord.getValue().Equals("")) {
          throw new EmptyRecordException("Vocabulary word is null or empty");
        }       

        if (isVocabularyWordExistsInDB(con, vocabularyWord)) {
          throw new AlreadyExistsException("Vocabulary word already exists");
        }

                    // insert
                    //
                    sqlInsert = String.Format("INSERT INTO Vocabulary (Value) VALUES ('{0}')", vocabularyWord.getValue());
                    SqlCommand command = new SqlCommand(sqlInsert, con);
                    command.ExecuteNonQuery();
                }

      //con.commit();
      //con.setAutoCommit(true);
    } catch (SqlException ignored) {
    }
  }

  private bool isVocabularyWordExistsInDB(SqlConnection con, VocabularyWord vocabularyWord) 
    {
    String sqlSelect = "SELECT Id FROM Vocabulary WHERE Value = ?";
            SqlCommand command = new SqlCommand(sqlSelect, con);
            SqlDataReader reader = command.ExecuteReader();

            return reader.HasRows;
    }


    }
}
