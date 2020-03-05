using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// LINQを試してみる
    /// </summary>
    public class LinqTest : IRunnable {

        public void Run() {
            this.basicLinq();
            this.linqQueryJoin();
            this.standardQueryOperators();
        }

        /// <summary>
        /// 基本的なLINQ
        /// </summary>
        private void basicLinq() {
            var scores = new List<(string, int)>() {
                ("Taro", 97),
                ("Jiro", 92),
                ("Saburo", 81),
                ("Shiro", 60),
            };

            // LINQクエリコマンドを格納するだけ
            var scoreQuery =
                from score in scores
                where 80 < score.Item2
                orderby score.Item2 ascending
                select score;

            // 一般的にLINQクエリコマンドは反復処理が始まった時点で遅延実行される
            foreach (var score in scoreQuery) {
                Debug.WriteLine($"80点を超えた人: {score.Item1}={score.Item2}");
            }

            // 集計関数は即時実行される
            Debug.WriteLine($"80点を超えた人数: {scoreQuery.Count()}");

            // リストや配列に変換: LINQクエリコマンドによって取得されるデータを丸ごとキャッシュするのに使う
            var list = scoreQuery.ToList();
            Debug.WriteLine($"Query->List: {list}");
            var array = scoreQuery.ToArray();
            Debug.WriteLine($"Query->Array: {array}");
        }

        /// <summary>
        /// LINQクエリーの結合を試してみる
        /// </summary>
        private void linqQueryJoin() {
            var students = new List<(int, string)>() {
                (1, "Taro"),
                (2, "Jiro"),
                (3, "Saburo"),
                (4, "Shiro"),
            };
            var scores = new List<(int, int)>() {
                (1, 97),
                (2, 92),
                (3, 81),
                (4, 60),
            };

            // students と scores を結合
            var scoreQuery =
                from student in students
                join score in scores on student.Item1 equals score.Item1
                select new {
                    student,
                    score
                };
            foreach (var item in scoreQuery) {
                Debug.WriteLine($"生徒の名前と点数紐づけ: {item.student.Item2}={item.score.Item2}");
            }
        }

        /// <summary>
        /// 標準クエリ演算子を使ってみる
        /// </summary>
        private void standardQueryOperators() {
            var data = Enumerable.Range(0, 10).ToArray();

            // ##### IN=多 / OUT=多 #################################
            // 条件絞り込み: フィルタリング
            this.printEnumerableObject(
                "filter/偶数だけを取り出す",
                data.Where(value => value % 2 == 0)
            );

            // すべての要素に特定の処理を噛ませる: 射影/map
            this.printEnumerableObject(
                "map/全要素を2乗",
                data.Select(value => value * value)
            );

            // 先頭n件取得
            this.printEnumerableObject(
                "take/先頭3件",
                data.Take(3)
            );

            // 先頭n件除外
            this.printEnumerableObject(
                "skip/先頭3件除外",
                data.Skip(3)
            );

            // 先頭から条件を満たさなくなるまで取得
            this.printEnumerableObject(
                "takeWhile/先頭条件付き取得",
                data.TakeWhile(value => value < 3)
            );

            // 先頭から条件を満たしている間を省略
            this.printEnumerableObject(
                "skipWhile/先頭条件付き除外",
                data.SkipWhile(value => value < 3)
            );

            // ##### IN=多 / OUT=1 #################################
            // 先頭1件取得
            this.printEnumerableObject(
                "first/先頭1件のみ",
                data.First()
            );

            // 最初に条件を満たす1件を取得
            this.printEnumerableObject(
                "first/条件付き1件のみ",
                data.First(value => value % 2 == 0)
            );

            // 条件を満たす1件が無いケース
            try {
                // 実際には InvalidOperationException が送出される
                this.printEnumerableObject(
                    "first/条件付き1件が無い",
                    data.First(value => 10 < value)
                );
            } catch (InvalidOperationException) {
                this.printEnumerableObject("first/条件付き1件が無い -> 例外", null);
            }

            // 末尾1件取得
            this.printEnumerableObject(
                "last/末尾1件のみ",
                data.Last()
            );

            // 最後に条件を満たす1件を取得
            this.printEnumerableObject(
                "last/条件付き末尾1件のみ",
                data.Last(value => value % 2 == 0)
            );

            // 結果が1件であることを保証したケース、取れなかったり、複数取れた場合は例外を吐く
            try {
                // 実際には InvalidOperationException が送出される
                this.printEnumerableObject(
                    "single/0件でも2件以上でもなく1件だけを保証 -> 0件で例外",
                    data.Single(value => 10 < value)
                );
            } catch (InvalidOperationException) {
                this.printEnumerableObject("single/0件でも2件以上でもなく1件だけを保証 -> 0件で例外", null);
            }
            try {
                // 実際には InvalidOperationException が送出される
                this.printEnumerableObject(
                    "single/0件でも2件以上でもなく1件だけを保証: 2件以上で例外",
                    data.Single(value => 0 < value)
                );
            } catch (InvalidOperationException) {
                this.printEnumerableObject("single/0件でも2件以上でもなく1件だけを保証 -> 2件以上で例外", null);
            }
            try {
                this.printEnumerableObject(
                    "single/0件でも2件以上でもなく1件だけを保証: 1件のみ",
                    data.Single(value => value == 5)
                );
            } catch (InvalidOperationException) {
                // 実際には実行されない
                this.printEnumerableObject("single/0件でも2件以上でもなく1件だけを保証 -> 1件のみ", null);
            }

            // 結果が2件以上無いことを保証したケース、複数取れた場合は例外を吐く
            try {
                // 実際には InvalidOperationException が送出される
                this.printEnumerableObject(
                    "single/2件以上でないことを保証 -> 2件以上で例外",
                    data.SingleOrDefault(value => 0 < value)
                );
            } catch (InvalidOperationException) {
                this.printEnumerableObject("single/2件以上でないことを保証 -> 2件以上で例外", null);
            }
            try {
                this.printEnumerableObject(
                    "single/2件以上でないことを保証: 1件のみ",
                    data.SingleOrDefault(value => value == 5)
                );
            } catch (InvalidOperationException) {
                // 実際には実行されない
                this.printEnumerableObject("single/2件以上でないことを保証 -> 1件のみ", null);
            }
            try {
                this.printEnumerableObject(
                    "single/2件以上でないことを保証 -> 0件",
                    data.SingleOrDefault(value => 10 < value)
                );
            } catch (InvalidOperationException) {
                // 実際には実行されない
                this.printEnumerableObject("single/2件以上でないことを保証 -> 0件", null);
            }

            // ##### IN=多 / OUT=bool #################################
            this.printEnumerableObject(
                "all/すべての要素が条件を満たすかどうか -> true",
                data.All(value => 0 <= value && value < 10)
            );
            this.printEnumerableObject(
                "all/すべての要素が条件を満たすかどうか -> false",
                data.All(value => value == 5)
            );
            this.printEnumerableObject(
                "any/いずれか1つ以上の要素が条件を満たすかどうか -> true",
                data.Any(value => value == 5)
            );
            this.printEnumerableObject(
                "any/いずれか1つ以上の要素が条件を満たすかどうか -> false",
                data.Any(value => value == -5)
            );

            // ##### IN=多 / OUT=集約結果 #################################
            this.printEnumerableObject(
                "max/最大値",
                data.Max()
            );

            this.printEnumerableObject(
                "min/最小値",
                data.Min()
            );

            this.printEnumerableObject(
                "sum/合計値",
                data.Sum()
            );

            this.printEnumerableObject(
                "average/相加平均値",
                data.Average()
            );

            // ##### IN=多＋多 / OUT=1つのシーケンス #################################
            this.printEnumerableObject(
                "union/和集合",
                data.Union(Enumerable.Range(100, 10))
            );

            this.printEnumerableObject(
                "intersect/積集合",
                data.Intersect(Enumerable.Range(5, 10))
            );

            this.printEnumerableObject(
                "except/差集合",
                data.Except(Enumerable.Range(5, 10))
            );

            this.printEnumerableObject(
                "distinct/重複排除",
                Enumerable.Repeat(100, 10).Distinct()
            );

            this.printEnumerableObject(
                "concat/シーケンス同士の単純連結",
                data.Concat(Enumerable.Range(100, 10))
            );

            this.printEnumerableObject(
                "zip/二つのシーケンスの要素同士を対応付ける",
                data.Zip(
                    Enumerable.Range(0, 10).Select(value => value * value),
                    (baseValue, squaredValue) => (baseValue, squaredValue)
                )
            );
        }

        private void printEnumerableObject(string prefix, object obj) {
            if (obj is IEnumerable) {
                foreach (var item in obj as IEnumerable) {
                    Debug.WriteLine($"LINQ [{prefix}]: {item}");
                }
            } else {
                // シーケンスではない場合は型名も添える
                Debug.WriteLine($"LINQ [{prefix}]: {obj} [{obj?.GetType()?.Name}]");
            }
        }

    }
}
