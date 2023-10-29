namespace SQLite4Unity3d.Example
{
    public class Class2
    {
        public int a;
        public float b;
        public string c;

        public Class2()
        {
            a = 999;
            b = 3.1416f;
            c = "hello world";
        }

        public Class2(string s)
        {
            var strs = s.Split("_");
            a = int.Parse(strs[0]);
            b = float.Parse(strs[1]);
            c = strs[2];
        }

        public override string ToString()
        {
            return a + "_" + b + "_" + c;
        }
    }
}