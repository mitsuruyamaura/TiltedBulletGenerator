using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_C : MonoBehaviour
{
    class TestClass
    {
        public string name;
        public int age;

        public TestClass(string name, int age) {
            this.name = name;
            this.age = age;
        }
    }

    // private TestClass[] testClass = new TestClass[5];
    //
    // void Start() {
    //     testClass[0] = new TestClass("鈴木", 37);
    //     testClass[1] = new TestClass("榊原", 14);
    //     testClass[2] = new TestClass("森田", 51);
    //     testClass[3] = new TestClass("山田", 19);
    //     testClass[4] = new TestClass("藤井", 23);
    // }
    
    // private TestClass[] testClass = 
    // {
    //     new TestClass("鈴木", 37),
    //     new TestClass("榊原", 14),
    //     new TestClass("森田", 51),
    //     new TestClass("山田", 19),
    //     new TestClass("藤井", 23)
    // };
    //
    
    
    // Target typed new 表現
    private TestClass[] testClass =
    { 
        new ("鈴木", 37),
        new ("榊原", 14),
        new ("森田", 51),
        new ("山田", 19),
        new ("藤井", 23)
    };

    // int[] values = { 0, 1 };

    private void Start() {
        foreach (var item in testClass)
        {
            Debug.Log($"Name : {item.name}, Age : {item.age}");
        }
    }


    private Vector3 positions = new(0, 0, 0);
    private List<TestClass> testList = new();


}
