using UnityEngine;

namespace SQLite4Unity3d.Example
{
    public class Class1
    {
        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }
        public string String1 { get; set; }
        public float Float1 { get; set; }
        // array to binary
        public float[] Floats1 { get; set; }
        public int[] Ints1 { get; set; }
        public bool[] Boolean1 { get; set; }
        
        public Vector2[] Vec2s { get; set; }
        
        public Vector3[] Vec3s { get; set; }
        // array to string
        [ToString]
        public string[] Strings1 { get; set; }
        [ToString]
        public float[] Floats2 { get; set; }
        [ToString]
        public int[] Ints2 { get; set; }
        [ToString]
        public bool[] Boolean2 { get; set; }
        // class or struct to string
        [ToString]
        public Class2 Class2 { get; set; }
    }
}