using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace BaseLib {
    public class Model {
        private readonly string m_DBName;
        private readonly Logger m_log;
        private readonly Temperature_Sensor.Config m_cfg;
        public string StrConn { get; set; }

        public Model(string DBName, Temperature_Sensor.Config cfg, Logger log) {
            m_DBName = DBName;
            m_log = log;
            m_cfg = cfg;
            this.StrConn = "";
            ReadConfig();
        }

        void ReadConfig() {
            StrConn = "user id=" + m_cfg.Setting.Data.DBUser + ";";
            StrConn += "password=" + m_cfg.Setting.Data.DBPwd + ";";
            StrConn += "database=" + m_DBName + ";";
            StrConn += "data source=" + m_cfg.Setting.Data.DBIP + "," + m_cfg.Setting.Data.DBPort;
        }

        public string[] GetTableColumns(string strTable) {
            using (SqlConnection sqlConn = new SqlConnection(StrConn)) {
                try {
                    sqlConn.Open();
                    DataTable schema = sqlConn.GetSchema("Columns", new string[] { null, null, strTable });
                    schema.DefaultView.Sort = "ORDINAL_POSITION";
                    schema = schema.DefaultView.ToTable();
                    int count = schema.Rows.Count;
                    string[] columns = new string[count];
                    for (int i = 0; i < count; i++) {
                        DataRow row = schema.Rows[i];
                        foreach (DataColumn col in schema.Columns) {
                            if (col.Caption == "COLUMN_NAME") {
                                if (col.DataType.Equals(typeof(DateTime))) {
                                    columns[i] = string.Format("{0:d}", row[col]);
                                } else if (col.DataType.Equals(typeof(decimal))) {
                                    columns[i] = string.Format("{0:C}", row[col]);
                                } else {
                                    columns[i] = string.Format("{0}", row[col]);
                                }
                            }
                        }
                    }
                    return columns;
                } catch (SqlException ex) {
                    m_log.TraceError("GetTableColumns() ERROR: " + ex.Message);
                    throw new ApplicationException(ex.Message);
                } finally {
                    if (sqlConn.State != ConnectionState.Closed) {
                        sqlConn.Close();
                    }
                }
            }
        }

        public Dictionary<string, int> GetTableColumnsDic(string strTable) {
            Dictionary<string, int> colDic = new Dictionary<string, int>();
            string[] cols = GetTableColumns(strTable);
            for (int i = 0; i < cols.Length; i++) {
                colDic.Add(cols[i], i);
            }
            return colDic;
        }

        /// <summary>
        /// 执行update insert delete语句，执行失败返回-1，执行成功返回影响的行数
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string strSQL) {
            int count = -1;
            if (strSQL.Length == 0) {
                return -1;
            }
            using (SqlConnection sqlConn = new SqlConnection(StrConn)) {
                using (SqlCommand sqlCmd = new SqlCommand(strSQL, sqlConn)) {
                    try {
                        sqlConn.Open();
                        count = sqlCmd.ExecuteNonQuery();
                        sqlCmd.Parameters.Clear();
                    } catch (SqlException ex) {
                        m_log.TraceInfo("Error SQL: " + strSQL);
                        m_log.TraceError(ex.Message);
                        throw new ApplicationException(ex.Message);
                    } finally {
                        if (sqlConn.State != ConnectionState.Closed) {
                            sqlConn.Close();
                        }
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// 执行select语句，返回的查询的结果集存放在DataTable中
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="dt"></param>
        public void Query(string strSQL, DataTable dt) {
            using (SqlConnection sqlConn = new SqlConnection(StrConn)) {
                try {
                    SqlDataAdapter adapter = new SqlDataAdapter(strSQL, sqlConn);
                    sqlConn.Open();
                    adapter.Fill(dt);
                } catch (SqlException ex) {
                    m_log.TraceError("Error SQL: " + strSQL);
                    m_log.TraceError(ex.Message);
                    throw new ApplicationException(ex.Message);
                } finally {
                    if (sqlConn.State != ConnectionState.Closed) {
                        sqlConn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行select语句，返回查询结果集中的第一行第一列（object类型，使用时需要强制转换类型），
        /// 若没有查询到结果则返回null
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public object QueryOne(string strSQL) {
            using (SqlConnection sqlConn = new SqlConnection(StrConn)) {
                using (SqlCommand cmd = new SqlCommand(strSQL, sqlConn)) {
                    try {
                        sqlConn.Open();
                        object obj = cmd.ExecuteScalar();
                        if (Object.Equals(obj, null) || Object.Equals(obj, System.DBNull.Value)) {
                            return null;
                        } else {
                            return obj;
                        }
                    } catch (SqlException ex) {
                        m_log.TraceError("Error SQL: " + strSQL);
                        m_log.TraceError(ex.Message);
                        throw new ApplicationException(ex.Message);
                    } finally {
                        if (sqlConn.State != ConnectionState.Closed) {
                            sqlConn.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// DataTable参数包含欲插入的表名和数据记录，记录可以有1条或多条，
        /// 有多条记录的话，函数会依次执行插入操作，执行成功后会返回受影响的行数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int InsertRecords(DataTable dt) {
            int iRet = 0;
            for (int iRow = 0; iRow < dt.Rows.Count; iRow++) {
                string strSQL = "insert into " + dt.TableName + " (";
                for (int iCol = 0; iCol < dt.Columns.Count; iCol++) {
                    if (dt.Rows[iRow][iCol].ToString().Length != 0 && dt.Columns[iCol].ColumnName != "ID") {
                        strSQL += dt.Columns[iCol].ColumnName + ",";
                    }
                }
                strSQL = strSQL.Trim(',');
                strSQL += ") values (";
                for (int iCol = 0; iCol < dt.Columns.Count; iCol++) {
                    if (dt.Rows[iRow][iCol].ToString().Length != 0 && dt.Columns[iCol].ColumnName != "ID") {
                        strSQL += "'" + dt.Rows[iRow][iCol].ToString() + "',";
                    }
                }
                strSQL = strSQL.TrimEnd(',');
                strSQL += ")";
                iRet += ExecuteNonQuery(strSQL);
            }
            return iRet;
        }

        /// <summary>
        /// DataTable参数包含欲更新的表名和数据记录，记录只能由1条，不能有多条，whereDic存放多个where条件
        /// 执行成功后会返回受影响的行数，若有多条记录的话，函数返回-1，表示参数出错
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="whereDic"></param>
        /// <returns></returns>
        public int UpdateRecord(DataTable dt, Dictionary<string, string> whereDic) {
            int iRet;
            if (dt.Rows.Count > 1) {
                iRet = -1;
            } else {
                string strSQL = "update " + dt.TableName + " set ";
                for (int j = 0; j < dt.Columns.Count; j++) {
                    strSQL += dt.Columns[j].ColumnName + " = '" + dt.Rows[0][j].ToString() + "', ";
                }
                strSQL = strSQL.Substring(0, strSQL.Length - 2);
                strSQL += " where ";
                foreach (string key in whereDic.Keys) {
                    strSQL += key + " = '" + whereDic[key] + "' and ";
                }
                strSQL = strSQL.Substring(0, strSQL.Length - 5);
                iRet = ExecuteNonQuery(strSQL);
            }
            return iRet;
        }

        public int GetRecordsCount(string strTableName) {
            string strSQL = "SELECT sum(row_count) as ROW_COUNT FROM sys.dm_db_partition_stats ";
            strSQL += "WITH (NOLOCK) WHERE index_id < 2 and object_id = OBJECT_ID('" + strTableName + "')";
            return Convert.ToInt32(QueryOne(strSQL));
        }

    }
}
