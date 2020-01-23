using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour
{
    public class Denumiri
    {

        public static string Wall = "Wall";
        public static string Fruit = "Fruit";
        public static string Tail = "Tail";
    }


    public class Dimensiuni
    {
        public static float bucata = 0.2f;
    }

    public enum Comenzile
    {
        LEFT = 0,
        UP = 1,
        RIGHT = 2,
        DOWN = 3,
        COUNT = 4
    }
}
