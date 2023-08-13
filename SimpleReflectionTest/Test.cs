using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleReflectionTest
{
    public class TestAction
    {
        public void Execute() { }
        public void Execute2() { }
        public void Execute2(string strArgs) { }
        public static void ExecuteStatic() { }
        public static void ExecuteStatic2() { }
        public static void ExecuteStatic2(string strArgs) { }
    }

    public class TestPrivateAction
    {
        private void Execute() { }
        private void Execute2() { }
        private void Execute2(string strArgs) { }
        private static void ExecuteStatic() { }
        private static void ExecuteStatic2() { }
        private static void ExecuteStatic2(string strArgs) { }
    }

    public class TestFunction
    {
        public int Execute() { return 1; }
        public int Execute2() { return 2; }
        public int Execute2(string strArgs) { return 2; }
        public static int ExecuteStatic() { return 1; }
        public static int ExecuteStatic2() { return 2; }
        public static int ExecuteStatic2(string strArgs) { return 2; }
    }

    public class TestPrivateFunction
    {
        private int Execute() { return 1; }
        private int Execute2() { return 2; }
        private int Execute2(string strArgs) { return 2; }
        private static int ExecuteStatic() { return 1; }
        private static int ExecuteStatic2() { return 2; }
        private static int ExecuteStatic2(string strArgs) { return 2; }
    }

}
