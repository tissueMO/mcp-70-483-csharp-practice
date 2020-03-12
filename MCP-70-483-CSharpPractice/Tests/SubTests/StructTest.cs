using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpractice.Tests.SubTests {

    // 構造体にはインスタンス初期化子を付けることができないコンパイルエラー
    //public struct StructTest {
    //    public double x = 0;
    //    public double y = 0;
    //    public double z = 0;

    //    public StructTest(double x, double y, double z) {
    //        this.x = x;
    //        this.y = y;
    //        this.z = z;
    //    }
    //}

    // すべてのフィールド (_x, _y, _z) が初期化されるまで this は参照できないコンパイルエラー
    //public struct StructTest {
    //    public double x {
    //        get {
    //            return this._x;
    //        }
    //        private set {
    //            this._x = value;
    //        }
    //    }
    //    private double _x;

    //    public double y {
    //        get {
    //            return this._y;
    //        }
    //        private set {
    //            this._y = value;
    //        }
    //    }
    //    private double _y;

    //    public double z {
    //        get {
    //            return this._z;
    //        }
    //        private set {
    //            this._z = value;
    //        }
    //    }
    //    private double _z;

    //    public StructTest(double x, double y, double z) {
    //        this.x = x;
    //        this.y = y;
    //        this.z = z;
    //    }
    //}

    // C# 6.0 からは以下でも動くが、それ以前は自動実装プロパティは使えなかったらしい
    // https://ufcpp.net/study/csharp/resource/rm_struct/
    //public struct StructTest {
    //    public double x {
    //        get; private set;
    //    }
    //    public double y {
    //        get; private set;
    //    }
    //    public double z {
    //        get; private set;
    //    }

    //    public StructTest(double x, double y, double z) {
    //        this.x = x;
    //        this.y = y;
    //        this.z = z;
    //    }
    //}

    // 以下はフィールドの初期化が不完全なのでコンパイルエラー
    //public struct StructTest {
    //    public double x;
    //    public double y;
    //    public double z;

    //    public StructTest(double x, double y, double z) {
    //        this.x = x;
    //        this.y = y;
    //    }
    //}

    // 以下は正しく動く
    public struct StructTest {
        public double x;
        public double y;
        public double z;

        public StructTest(double x, double y, double z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
