using System;
using System.Collections.Generic;
public partial class Config_Test
{
    public object data = new
    {
        testobj = new { a = "1", b = 2 },
        //值为普通数据
        testint = new Dictionary<object, object>() { { 1, 2 }, { 2, 3 } },
        teststring = new Dictionary<object, object>() { { 1, "2" }, { 2, "3" } },
        //值为普通数据数组
        testlist = new Dictionary<object, object[]>() { { 1, new object[] { 1, 2, 3 } } },
        //值为自定义类
        denglu = new Dictionary<object, object>() { { 1, new { a = "123" } }, { 2, new { b = "asdf" } }, { 3, new { c = "123", b = "111" } } },
        //值为自定义类数组
        testdenglulist = new Dictionary<object, object[]>() { 
            { 1, new object[] { new { a = "123" }, new { b = "asdf" } } },
            { 2, new object[] { new { c = "123" }, new { b = "asdf" } } },
        },
    };
}
