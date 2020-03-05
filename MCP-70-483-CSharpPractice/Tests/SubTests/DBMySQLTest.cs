using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// MySQLに繋いでゴニョゴニョしてみる
    /// </summary>
    public class DBMySQLTest : IRunnable {

        public void Run() {
            // App.config にある設定値から接続文字列を読み込んで、MySQLに繋いでみる
            var connectionString = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
            using (var connection = new MySqlConnection(connectionString)) {
                connection.Open();

                // テーブル削除＆作成
                new MySqlCommand("DROP TABLE IF EXISTS m_test;", connection).ExecuteNonQuery();
                new MySqlCommand("CREATE TABLE m_test (id INT, name VARCHAR(10), comment TEXT);", connection).ExecuteNonQuery();

                // データ削除 [D]
                new MySqlCommand("DELETE FROM m_test;", connection).ExecuteNonQuery();

                // データ追加 [C]
                new MySqlCommand("INSERT INTO m_test (id, name, comment) VALUES (1, 'TEST', 'COMMENT IS NOTHING');", connection).ExecuteNonQuery();

                // データ更新 [U]
                new MySqlCommand("UPDATE m_test SET comment = 'COMMENT IS UPDATED' WHERE id = 1;", connection).ExecuteNonQuery();

                // データ読込 [R]
                using (var r = new MySqlCommand("SELECT * FROM m_test;", connection).ExecuteReader()) {
                    // 列名の取得
                    var columns = new List<(string, Type)>();
                    foreach (var i in Enumerable.Range(0, r.FieldCount)) {
                        columns.Add((r.GetName(i), r.GetFieldType(i)));
                        Debug.WriteLine($"SelectedColumn: {r.GetName(i)} (Type={r.GetFieldType(i).Name})");
                    }

                    // データの取得
                    while (r.Read()) {
                        foreach (var (columnName, columnType) in columns) {
                            var value = Convert.ChangeType(r[columnName], Type.GetType(columnType.FullName));
                            Debug.Write($"{columnName}={value} | ");
                        }
                        Debug.WriteLine("");
                    }
                }
            }
        }

    }
}
